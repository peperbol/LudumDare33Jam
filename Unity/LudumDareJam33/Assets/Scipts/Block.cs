﻿using UnityEngine;
using System.Collections;
using System;

public class Block : MonoBehaviour  {
  private Coords coordinates;
  private BlockGenerator bg;

  public Coords Coordinates {
    get { return coordinates; }
  }

  public void Init(Coords coords, BlockGenerator blockgen) {
    bg = blockgen;
    coordinates = coords;
    transform.SetParent(blockgen.transform);
    transform.localPosition = new Vector3(coords.x * Size, -coords.y * Size, 0);
    gameObject.name = coords.x + " : " + coords.y + " " + gameObject.name;
  }

  public Block Left { get { return bg.GetBlock(Coordinates.left); } }

  public Block Right { get { return bg.GetBlock(Coordinates.right); } }

  public Block Lower { get { return bg.GetBlock(Coordinates.lower); } }

  public Block upper { get { return bg.GetBlock(Coordinates.upper); } }

  public static float Size { get{return Grid.scale * 32;}}

  public static Coords Vector3toCoords(Vector3 v) {
    return new Coords { x = Mathf.FloorToInt(v.x / 32), y = Mathf.FloorToInt(-v.y / 32) };

  }

  public struct Coords
  {
    public int x;
    public int y;
    public Coords left {
      get
      {
        return (new Func< int,int, Coords>(( X, Y) => { return new Coords { x = X -1 , y = Y }; })(x,y));
      }
    }
    public Coords right
    {
      get
      {
        return (new Func<int, int, Coords>((X, Y) => { return new Coords { x = X + 1, y = Y }; })(x, y));
      }
    }
    public Coords upper
    {
      get
      {
        return (new Func<int, int, Coords>((X, Y) => { return new Coords { x = X , y = Y - 1 }; })(x, y));
      }
    }
    public Coords lower
    {
      get
      {
        return (new Func<int, int, Coords>((X, Y) => { return new Coords { x = X , y = Y + 1 }; })(x, y));
      }
    }
  }
}
