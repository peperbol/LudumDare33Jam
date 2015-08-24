using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Movement))]
public class KidAI : MonoBehaviour {

  public enum State { Normal, Scared,Distracted, Dead};

  public Queue<NavNode> destination;
  private NavNode nextNode;
  Vector2 nextNodePosition;

  KidsGroup group;

  State currentState = State.Normal;
  List<NavNode> doorsVisited = new List<NavNode>();
  public bool reachedNextNode = false;
  Movement movement;


  bool InGroup {
    get {
      return group != null;
    }
  }

  public void Scare() {

  }

  public NavNode NextNode {
    get {
      return nextNode;
    }
    set {
      nextNode = value;
      nextNodePosition = nextNode.getRandomPosition();
      reachedNextNode = false;
    }
  }

  public bool ReachedNextNode
  {
    get
    {
      return reachedNextNode;
    }
  }

  public KidsGroup Group
  {
    get
    {
      return group;
    }

    set
    {
      group = value;
    }
  }

  void SetNextNode() {
    if (InGroup) {
      group.SetNextNode();
      return;
    }
    if (destination != null && destination.Count > 0)
    {
      NextNode = destination.Dequeue() ;
      return;
    }
    if (NextNode.type == NavNode.NodeType.Door) doorsVisited.Add(NextNode);
    SetNextDestination();
  }

  void SetNextDestination() {
    if (!InGroup) {
      if (currentState == State.Normal)
      {
        destination = NextNode.GetNodeDirection(NavNode.NodeType.Door, doorsVisited);
        if (destination == null) {
          doorsVisited = new List<NavNode>();
          destination = NextNode.GetNodeDirection(NavNode.NodeType.Door, doorsVisited);
        }
        SetNextNode();
      }
      else
      {
        //todo
      }
    }
    else {
      group.SetNextDestination();
    }
  }

	void Move()
  {
    if (CanMoveOn())
    {
      SetNextNode();
    }

    Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
    movement.Move(nextNodePosition - myPos);
  }

  bool CanMoveOn() {
    if (!InGroup) {
      return reachedNextNode;
    }
    else
    {

      return group.CanMoveOn();
    }
  }

	void Update () {
    switch (currentState)
    {
      case State.Normal:
        Move();
        break;
      case State.Scared:
        break;
      case State.Distracted:
        break;
      case State.Dead:
        break;
      default:
        break;
    }


  }
  void Start() {
    movement = GetComponent<Movement>();
  }

  void OnTriggerStay2D(Collider2D c) {
    if (c == NextNode.Col) reachedNextNode = true;
  }
}
