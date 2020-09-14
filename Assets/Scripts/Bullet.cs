using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
  public float speed;

  float damage;
  string shooter;

  void Update() {
    transform.Translate(Vector3.up * speed * Time.deltaTime);
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy") {
      if (other.gameObject.tag != shooter && !other.isTrigger) {
        other.gameObject.GetComponent<Health>().SetDamage(damage);
        Destroy(gameObject);
      }
    }
  }

  public void Fire(string shooter, float damage) {
    this.shooter = shooter;
    this.damage = damage;
  }
}
