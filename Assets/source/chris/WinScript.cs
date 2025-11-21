using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Author: Christopher Soto
/// Date: November 14, 2025
/// This file stores script that shows winzone after defeating boss
/// </summary>
public class WinScript : MonoBehaviour
{
    private BoxCollider2D Collision;
    private SpriteRenderer Sprite;
    /// <summary>
    /// enables winzone sprite and collision
    /// </summary>
    /// <param name="value"> sent from dead boss</param>
    public void enable(bool value)
    {
        Sprite.enabled = value;
        Collision.enabled = value;
    }
    /// <summary>
    /// sets necessary components and disables winzone
    /// </summary>
    void Awake()
    {
        Collision = GetComponent<BoxCollider2D>();
        Sprite = GetComponent<SpriteRenderer>();
        enable(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /// <summary>
    /// if player enters winzone sets scene back to main menu
    /// </summary>
    /// <param name="collision"> sent from target</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }
}
