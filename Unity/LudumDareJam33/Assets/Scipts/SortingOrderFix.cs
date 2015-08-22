using UnityEngine;
using System.Collections;
public class SortingOrderFix : MonoBehaviour {
  SpriteRenderer[] renderers = new SpriteRenderer[1];
  bool continuousFix = false;
  private void Fix()
  {
    for (int i = 0; i < renderers.Length; i++)
    {
      renderers[i].sortingOrder = Mathf.FloorToInt(-transform.position.y);
    }
  }
	void Start () {
    if (!continuousFix) Fix();
	}
  void Update() {
    if (continuousFix) Fix();
  }
}
