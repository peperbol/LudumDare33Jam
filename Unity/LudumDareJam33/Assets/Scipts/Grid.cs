using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
  public GameObject[,] grid = new GameObject[32,32];
  public List<Coords> selected = new List<Coords>();
  public static float scale = 1;
  public int Width {
    get { return grid.GetLength(0); }
    set {
      GameObject[,] newGrid = new GameObject[value, Height];

      for (int x = 0; x < value && x <  Width; x++)
      {
        for (int y = 0; y < Height; y++)
        {
          newGrid[x, y] = grid[x, y];
        }
      }
      grid = newGrid;
    }
  }
  public int Height
  {
    get { return grid.GetLength(1); }
    set {
      GameObject[,] newGrid = new GameObject[Width, value];

      for (int x = 0; x < Width; x++)
      {
        for (int y = 0; y < value && y < Height; y++)
        {
          newGrid[x, y] = grid[x, y];
        }
      }
      grid = newGrid;
    }
  }
  public void SetObject(int x, int y, GameObject go) {
    if (grid[x, y] != null) {
      DestroyImmediate(grid[x, y]);
      Debug.Log(3);
    }
    grid[x, y] = Instantiate(go);
    grid[x, y].transform.SetParent(transform);
    grid[x, y].transform.localPosition = new Vector3((x+ 0.5f)*scale, -(y + 0.5f) * scale, 0);
  }

  public void SetObject(Coords c, GameObject go) {
    SetObject(c.x, c.y, go);
  }
  public float MaxX {
    get { return Width * scale; } 
  }
  public float MaxY
  {
    get { return Height * scale; }
  }
  public void UnSelect() {
    selected = new List<Coords>();
  }
  public void AddSelected(Coords item) {
    for (int i = 0; i < selected.Count; i++)
    {
      if (selected[i].x == item.x && selected[i].y == item.y) return;
    }
    selected.Add(item);
  }

  public void SetObjectForSelected(GameObject go) {
    if (go == null) return;
    for (int i = 0; i < selected.Count; i++)
    {
      SetObject(selected[i], go);
    }
  }

  void OnDrawGizmosSelected()
  {
    int gizmoHeight = 20;
    Color linesColor = new Color(0, 0, 0.2f,1);
    Color selectedColor = new Color(0, 1, 0, 0.3f);

    Gizmos.color = linesColor;
    for (int x = 0; x <= Width; x++)
    {
      Gizmos.DrawLine(new Vector3(transform.position.x + x* scale, transform.position.y, transform.position.z - gizmoHeight), new Vector3(transform.position.x + x* scale, transform.position.y - MaxY, transform.position.z - gizmoHeight));
    }
    for (int y = 0; y <= Height; y++)
    {
      Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y - y * scale, transform.position.z - gizmoHeight), new Vector3(transform.position.x + MaxX, transform.position.y - y * scale, transform.position.z - gizmoHeight));
    }

    Gizmos.color = selectedColor;
    for (int i = 0; i < selected.Count; i++)
    {
      Gizmos.DrawCube(new Vector3(transform.position.x + (selected[i].x+ 0.5f)* scale , transform.position.y  -(selected[i].y + 0.5f) * scale, transform.position.z - gizmoHeight), new Vector3(scale, scale, 0));
    }
  }

  public struct Coords {
    public int x;
    public int y;
  }
}
