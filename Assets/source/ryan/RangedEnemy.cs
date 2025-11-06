using UnityEngine;

public class RangedEnemy : EnemyController
{
    public override void decideMove()
    {
        // Ranged enemies move in a circular pattern around the player
        if (playerInSight)
        {
            float angle = Time.time * getSpeed();
            Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * 2f;
            MoveVec = offset;
        }
    }
}