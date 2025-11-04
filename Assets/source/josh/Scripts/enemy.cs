using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : Character
{
   protected bool playerInSight = false;
   Rigidbody2D Player;
   Rigidbody2D rb;

   public Slider healthBar;
   string healthBarPath = "Canvas/HealthBar";
   Vector2 TargetPosition;
   Vector2 MoveVec;

   public void initMovement() {
      rb = GetComponent<Rigidbody2D>();

      //Path to slider
      Transform t = transform.Find(healthBarPath);
      Assert.NotNull(t);
      healthBar = t.GetComponent<Slider>();

      healthBar.maxValue = getMaxHealth();
      healthBar.minValue = 0;
      healthBar.value = getCurrentHealth();
   }

   public virtual void move(){
      rb.MovePosition(rb.position + (MoveVec * getSpeed() * Time.fixedDeltaTime));
   }


   void FollowPlayer()
   {
      MoveVec = Vector2.zero;
      if (playerInSight)
      {
         TargetPosition = Player.position - rb.position;
         MoveVec = TargetPosition.normalized;
      }
   }

   public virtual void decideMove(){
      FollowPlayer();
   }


   void OnTriggerEnter2D(Collider2D collision)
   {
      if (collision.CompareTag("Player"))
      {
         Player = collision.attachedRigidbody;
         playerInSight = true;
      }
   }

   void OnCollisionEnter2D(Collision2D collision)
   {
      PlayerController player = collision.collider.GetComponent<PlayerController>();
      if (collision.collider.CompareTag("Player"))
      {
         player.takeDamage(1);
      }
   }

   void OnCollisionStay2D(Collision2D collision)
   {
      PlayerController player = collision.collider.GetComponent<PlayerController>();
      if (collision.collider.CompareTag("Player"))
      {
         player.takeDamage(1);
      }

   }

   void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.CompareTag("Player"))
      {
         playerInSight = false;
      }
   }

   public void TakeDamage(float damage)
   {
      healthBar.value = modifyHealth((int)(-1*damage));
   }
}
