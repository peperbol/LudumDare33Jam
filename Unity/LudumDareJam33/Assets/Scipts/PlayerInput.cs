using UnityEngine;
using System.Collections;
[RequireComponent(typeof(PlayerInteraction))]
public class PlayerInput : MonoBehaviour {
  private PlayerInteraction pi;
  // Use this for initialization
  void Start () {
    pi = GetComponent<PlayerInteraction>();
  }
	
	// Update is called once per frame
	void Update () {

    Vector2 v = Vector2.zero;
    v.x = Input.GetAxis("Horizontal");
    v.y = Input.GetAxis("Vertical");
    pi.Move(v);
  }
}
