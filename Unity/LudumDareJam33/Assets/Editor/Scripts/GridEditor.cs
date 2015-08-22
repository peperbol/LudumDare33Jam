using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(Grid))]
public class GridEditor : Editor {
  private Grid g;
  public override void OnInspectorGUI()
  {
    //base.OnInspectorGUI();
    //g.Width = EditorGUILayout.IntField("Width", g.Width);
    //g.Height = EditorGUILayout.IntField("Height", g.Height);
    //Grid.scale = EditorGUILayout.FloatField("Scale", Grid.scale);

    if (g.selected.Count > 0) {
      g.SetObjectForSelected((GameObject)EditorGUILayout.ObjectField("Set GameObject", null, typeof(GameObject), false));
    }
  }
  public void OnEnable()
  {
    g = (Grid)target;
  }
  void OnSceneGUI()
  {
    int controlId = GUIUtility.GetControlID(FocusType.Passive);
    Event e = Event.current;
    switch (e.type)
    {
      case EventType.MouseDown:

        Vector2 pos = GetMousePosition(e);
        pos.x -= g.transform.position.x;

        pos.y -= g.transform.position.y;

        pos.y *= -1;

        if (pos.x > 0 && pos.y > 0 && pos.x < g.MaxX && pos.y < g.MaxY) {

          Grid.Coords c = new Grid.Coords()
          {
            x = Mathf.FloorToInt(pos.x),
            y = Mathf.FloorToInt(pos.y)
          };

          if (!e.control&& ! e.shift)
          {
            g.UnSelect();
          }
          if (e.shift && g.selected.Count >0) {
            int minX = Mathf.Min(g.selected[0].x, c.x);
            int minY = Mathf.Min(g.selected[0].y, c.y);
            int maxX = Mathf.Max(g.selected[0].x, c.x);
            int maxY = Mathf.Max(g.selected[0].y, c.y);

            for (int X = minX; X <= maxX; X++)
            {
              for (int Y = minY; Y <= maxY; Y++)
              {
                g.AddSelected(new Grid.Coords() { x = X, y =Y});
              }
            }


          }
          else
          {
            g.AddSelected(c);
            
          }

            GUIUtility.hotControl = controlId;
          e.Use();
        }

        break;
    }
  }

  public static Vector2 GetMousePosition(Event e)
  {
    Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
    return r.origin;
  }
}
