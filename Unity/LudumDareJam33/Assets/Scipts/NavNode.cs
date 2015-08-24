using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CircleCollider2D))]
public class NavNode : MonoBehaviour
{
  public enum NodeType { Road, Door, Hidespot }
  private static int idCount = 0;
  private int id;
  public List<NavNode> connections;
  public NodeType type;
  private CircleCollider2D col;

  public int Id { get { return id; } }

  void Start()
  {
    col = GetComponent<CircleCollider2D>();
    id = idCount++;
  }
  /*
  public Queue<NavNode> GetNodeDirection(NavNode node, List<NavNode> exclude, ref int hops, int maxhops, bool excludeThis)
  {

    hops++;
    List<NavNode> passed = new List<NavNode>(exclude);

    if (hops >= maxhops) return null;

    if (Id == node.Id)
    {
    
      return this;
    }
    passed.Add(this);
    NavNode currentNode = null;


    for (int i = 0; i < connections.Count; i++)
    {
      if (!passed.Contains(connections[i]))
      {
        int h = hops;
        NavNode n = connections[i].GetNodeDirection(node, passed, ref h, maxhops, false);
        if (n != null )
        {
          currentNode = n;
          maxhops =  h;
        }
      }
    }
    hops = maxhops;

    if (currentNode == null) return null;

    return (excludeThis) ? currentNode : this;
  }
  public NavNode GetNodeDirection(NavNode node, List<NavNode> exclude)
  {
    int i = 0;
    return GetNodeDirection(node, exclude, ref i,99999, true);
  }
  public NavNode GetNodeDirection(NavNode node)
  {
    return GetNodeDirection(node, new List<NavNode>());
  }
  */
  public Queue<NavNode> GetNodeDirection(NodeType type, Queue<NavNode> currentQueue, List<NavNode> exclude, ref int hops, int maxhops)
  {
    hops++;

    List<NavNode> passed = new List<NavNode>(exclude);
    Queue<NavNode> queue = new Queue<NavNode>(currentQueue);

    queue.Enqueue(this);

    if (hops >= maxhops) return null;
    if (!exclude.Contains(this))
    {
      if (this.type == type)
      {

        return queue;
      }
      passed.Add(this);
    }

    Queue<NavNode> newQueue = null;


    for (int i = 0; i < connections.Count; i++)
    {
      if (!passed.Contains(connections[i]))
      {
        int h = hops;
        Queue<NavNode> n = connections[i].GetNodeDirection(type, queue, passed, ref h, maxhops);
        if (n != null)
        {
          newQueue = n;
          maxhops = h;
        }
      }
    }

    hops = maxhops;

    return newQueue;
  }

  public Queue<NavNode> GetNodeDirection(NodeType type)
  {
    return GetNodeDirection(type, new List<NavNode>());
  }

  public Queue<NavNode> GetNodeDirection(NodeType type, List<NavNode> exclude)
  {
    int i = 0;
    return GetNodeDirection(type, new Queue<NavNode>(), exclude, ref i, 99999);
  }

  public Vector2 getRandomPosition()
  {
    return randomcircleoffset() * col.radius + col.offset + new Vector2(transform.position.x, transform.position.y);
  }

  private Vector2 randomcircleoffset()
  {
    Vector2 toReturn;
    do
    {
      toReturn = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    } while (toReturn.sqrMagnitude > 1);
    return toReturn;
  }

  public CircleCollider2D Col { get { return col; } }

}
