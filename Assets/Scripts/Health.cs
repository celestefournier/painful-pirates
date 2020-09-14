using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour {
  public float maxHealth;
  public float health;
  public GameObject healthBar;
  public GameObject explosion;
  public Sprite[] shipStages;

  Vector3 healthBarPos;
  SpriteRenderer sprite;
  GameController gameController;

  void Start() {
    sprite = GetComponent<SpriteRenderer>();
    healthBarPos = healthBar.transform.localPosition;
    healthBar.transform.localPosition = transform.localPosition + healthBarPos;
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
  }

  void Update() {
    healthBar.transform.localPosition = transform.localPosition + healthBarPos;
    healthBar.GetComponent<HealthBar>().Health(health / maxHealth);

    if (health <= 0) {
      Die();
    }

    if (health / maxHealth < 0.34f) {
      sprite.sprite = shipStages[2];
    }
    else if (health / maxHealth < 0.67f) {
      sprite.sprite = shipStages[1];
    }
    else {
      sprite.sprite = shipStages[0];
    }
  }

  public void SetDamage(float damage) {
    health -= damage;
    healthBar.GetComponent<HealthBar>().Health(health / maxHealth);
  }

  void Die() {
    if (gameObject.tag == "Enemy") {
      gameController.SetScore();
    } else if (gameObject.tag == "Player") {
      gameController.GameOver();
    }
    Instantiate(explosion, transform.position, Quaternion.identity);
    Destroy(transform.parent.gameObject);
  }
}
