using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
  public float maxHealth;
  public float health;
  public GameObject healthBar;
  public GameObject explosion;

  Vector3 healthBarPos;

  void Start() {
    healthBarPos = healthBar.transform.localPosition;
    healthBar.transform.localPosition = transform.localPosition + healthBarPos;
  }

  void Update() {
    healthBar.transform.localPosition = transform.localPosition + healthBarPos;
    healthBar.GetComponent<HealthBar>().Health(health / maxHealth);

    if (health <= 0) {
      Die();
    }
  }

  public void SetDamage(float damage) {
    health -= damage;
    healthBar.GetComponent<HealthBar>().Health(health / maxHealth);
  }

  void Die() {
    Instantiate(explosion, transform.position, Quaternion.identity);
    Destroy(transform.parent.gameObject);
  }
}
