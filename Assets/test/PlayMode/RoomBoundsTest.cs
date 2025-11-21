using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class RoomBoundsTest
{
    [UnityTest]
    public IEnumerator LoadScene()
    {
        SceneManager.LoadScene("LevelTester");
        yield return null; 
    }

    [Test]
    public void RoomBoundsTestSimplePasses()
    {
        Assert.Pass();
    }

    [UnityTest]
    public IEnumerator RoomCollisions()
    {
        if (SceneManager.GetActiveScene().name != "LevelTester")
        {
            SceneManager.LoadScene("LevelTester");
            yield return null;
        }

        GameObject root = null;
        float timeout = 10f;
        float t = 0f;

        while (root == null && t < timeout)
        {
            root = GameObject.Find("RoomRoot"); 
            yield return null;                  
            t = t + Time.deltaTime;
        }

        Assert.IsNotNull(root,
            "RoomRoot not found. Is the LevelTester scene loaded and does it create an active object named 'RoomRoot'?");

        int lastCount = -1;
        int stableFrames = 0;
        const int neededStableFrames = 5;
        t = 0f;

        while (stableFrames < neededStableFrames && t < timeout)
        {
            int current = root.transform.childCount;
            if (current == lastCount)
            {
                stableFrames = stableFrames + 1;
            }
            else
            {
                stableFrames = 0;
                lastCount = current;
            }

            yield return null;
            t = t + Time.deltaTime;
        }

        List<Transform> rooms = new List<Transform>();
        int roomCount = root.transform.childCount;
        for (int i = 0; i < roomCount; i = i + 1)
        {
            Transform child = root.transform.GetChild(i);
            if (child != null)
            {
                rooms.Add(child);
            }
        }

        Assert.Greater(rooms.Count, 0, "RoomRoot has no children.");

        int roomMask = LayerMask.GetMask("RoomCollider");
        Assert.AreNotEqual(0, roomMask,
            "LayerMask.GetMask(\"RoomCollider\") returned 0. Create a 'RoomCollider' layer and assign your room colliders to it, or change the mask name here.");

        Physics2D.SyncTransforms();
        yield return new WaitForFixedUpdate();

        bool collided = false;

        for (int i = 0; i < rooms.Count; i = i + 1)
        {
            Transform room = rooms[i];
            Vector2 center = room.position;

            Vector2 size = new Vector2(13f, 10f);
            const float angle = 0f;


            Collider2D[] hits = Physics2D.OverlapBoxAll(center, size, angle, roomMask);

            for (int j = 0; j < hits.Length; j = j + 1)
            {
                Collider2D hit = hits[j];
                if (hit == null)
                {
                    continue;
                }

                if (hit.transform.IsChildOf(room) == true)
                {
                    continue;
                }

                collided = true;

                Transform otherTop = hit.transform;
                while (otherTop.parent != null && otherTop.parent != root.transform)
                {
                    otherTop = otherTop.parent;
                }

                Assert.Fail(
                    "Rooms are colliding: '" + room.name + "' overlaps with '" + otherTop.name +
                    "' via collider '" + hit.name + "'."
                );
            }
        }
        Assert.IsFalse(collided, "Rooms are colliding (no specific pair reported).");
        yield return null;
    }
}