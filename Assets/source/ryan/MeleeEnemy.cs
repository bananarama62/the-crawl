using UnityEngine;

public class MeleeEnemy : EnemyController
{
    public override void decideMove()
    {
        FollowPlayer();
    }
}