using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Movement : MonoBehaviour {

  private int direction = 4;
  public float movementspeed = 10;
  protected Rigidbody2D rb;
  protected Animator anim;
  
  private int Direction
  {
    get
    {
      return direction;
    }
    set
    {
      direction = value;
      anim.SetInteger("direction", value);
    }
  }

   public virtual void Move(Vector2 dir) {
    rb.AddForce(dir*movementspeed * Time.deltaTime, ForceMode2D.Force);
    if (dir != Vector2.zero) {
      int newDirection;

      if (dir.x == 0) {
        dir.x += (dir.y > 0)? 0.001f : -0.001f;
      }
      float rico = dir.y / dir.x;
      bool pos = dir.x > 0;


      if (rico > 2) newDirection = 0;
      else if (rico > 0.5f) newDirection = 1;
      else if (rico > -0.5f) newDirection = 2;
      else if (rico > -2f) newDirection = 3;
      else newDirection = 4;

      if (!pos) newDirection += 4;

      newDirection %= 8;

      Direction = newDirection;
    }
  }

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
  }
  protected virtual void Update() {
      anim.SetInteger("action", (rb.velocity.SqrMagnitude() > 0)? 1:0);
  }
}
