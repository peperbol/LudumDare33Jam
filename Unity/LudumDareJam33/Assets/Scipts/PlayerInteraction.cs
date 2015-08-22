using UnityEngine;
using System.Collections;

public class PlayerInteraction : Movement {
  private float scaring = 0;
  private float eating = 0;

  public float eatTime = 1;
  public float ScareTime = 1;

  public bool Scaring { get { return (scaring > 0); } }
  public bool Eating { get { return (eating > 0); } }

  public void Scare() {
    if (!Channeling) {
      scaring = ScareTime;
    }
  }

  public void Eat()
  {
    if (!Channeling)
    {
      eating = eatTime;
    }
  }
  public override void Move(Vector2 dir)
  {
    if (!Channeling)
    {
      base.Move(dir);
    }
  }

  protected override void Update() {
    

    if (Scaring){
      anim.SetInteger("action", 2);

      scaring -= Time.deltaTime;
    }
    else if (Eating){
      anim.SetInteger("action", 3);

      eating -= Time.deltaTime;
    }
    else if (rb.velocity.SqrMagnitude() > 0)  { anim.SetInteger("action", 1); }
    else                                      { anim.SetInteger("action", 0); }
  }
	public bool Channeling {
    get { return Scaring || Eating; }
  }
	
}
