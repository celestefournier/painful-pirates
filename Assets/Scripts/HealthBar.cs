using System.Collections;
using UnityEngine;

public class HealthBar : MonoBehaviour {
  public Transform healthBar;

  public void Health(float health) {
    healthBar.localScale = new Vector3(health / 100, 1, 1);
  }
}
