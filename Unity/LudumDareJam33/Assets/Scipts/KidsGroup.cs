using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KidsGroup : MonoBehaviour {

  public List<KidAI> kids;


  List<NavNode> doorsVisited = new List<NavNode>();

  public NavNode destination;
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

  public NavNode Destination
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
    if (NextNode == null || NextNode.Id != Destination.Id)
    {
      NextNode = NextNode.GetNodeDirection(Destination);
      return;
    }
    if (Destination.type == NavNode.NodeType.Door) doorsVisited.Add(Destination);
    SetNextDestination();
  }

  public void SetNextDestination()
  {
    Destination = NextNode.GetNode(NavNode.NodeType.Door, doorsVisited);
    if (Destination == null)
    {
      doorsVisited = new List<NavNode>();
      Debug.Log("h");
      Destination = NextNode.GetNode(NavNode.NodeType.Door, doorsVisited);
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
