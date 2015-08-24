using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KidsGroup : MonoBehaviour {

  public List<KidAI> kids;


  List<NavNode> doorsVisited = new List<NavNode>();

  public Queue<NavNode> destination;
  public NavNode nextNode;

  NavNode NextNode
  {
    get
    {
      return nextNode;
    }
    set
    {
      nextNode = value;
      for (int i = 0; i < kids.Count; i++)
      {
        kids[i].NextNode = value;
      }
    }
  }

  public Queue<NavNode> Destination
  {
    get
    {
      return destination;
    }
    set
    {
      destination = value;
      for (int i = 0; i < kids.Count; i++)
      {
        kids[i].destination = value;
      }
    }
  }

  public void SetNextNode()
  {
    if ( Destination != null && Destination.Count > 0)
    {
      NextNode = destination.Dequeue();
      return;
    }
    if (NextNode.type == NavNode.NodeType.Door)  doorsVisited.Add(NextNode);
    SetNextDestination();
  }

  public void SetNextDestination()
  {
    Destination = NextNode.GetNodeDirection(NavNode.NodeType.Door, doorsVisited);
    if (Destination == null)
    {
      doorsVisited = new List<NavNode>();
      Destination = NextNode.GetNodeDirection(NavNode.NodeType.Door, doorsVisited);
    }
    SetNextNode();
  }

  public bool CanMoveOn()
  {
    bool r = true;
    for (int i = 0; i < kids.Count; i++)
    {
      r &= kids[i].ReachedNextNode;
    }
    return r;
  }

  void Start() {
    for (int i = 0; i < kids.Count; i++)
    {
      kids[i].Group = this;
    }
    Destination = destination;
    NextNode = nextNode;
  }
}
