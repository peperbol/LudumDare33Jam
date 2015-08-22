using UnityEngine;
using System.Collections;
public class SortingOrderFix : MonoBehaviour {
  SpriteRenderer[] renderers = new SpriteRenderer[1];
  bool continuousFix = false;
  int orderOffset = 0;
  private void Fix()
  {
    for (int i = 0; i < renderers.Length; i++)
    {
      renderers[i].sortingOrder = Mathf.FloorToInt(-transform.position.y) + orderOffset;
    }
  }
	void Start () {
    if (!continuousFix) Fix();
	}
  void Update() {
    if (continuousFix) Fix();
  }
}
