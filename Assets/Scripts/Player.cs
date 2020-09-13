using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
  public float speed = 2;
  public float rotationSpeed = 60;
  public GameObject bullet;
  public GameObject healthBar;
  public Transform playerGroup;
  public Transform spawnFrontalShoot;
  public Transform[] spawnLeftShoot;
  public Transform[] spawnRightShoot;

  int health = 100;
  Rigidbody2D rb;
  Vector3 healthBarPos;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    healthBarPos = healthBar.transform.localPosition;
    healthBar.transform.localPosition = transform.localPosition + healthBarPos;
  }

  void Update() {
    Movement();
    Shoot();
  }

  void Movement() {
    if (Input.GetAxis("Vertical") >= 0) {
      rb.velocity = transform.up * Input.GetAxis("Vertical") * speed;
    }

    if (Input.GetAxis("Horizontal") != 0) {
      transform.Rotate(
        new Vector3(0, 0, -Input.GetAxis("Horizontal")) * rotationSpeed * Time.deltaTime,
        Space.World
      );
    }

    healthBar.transform.localPosition = transform.localPosition + healthBarPos;
  }

  void Shoot() {
    if (Input.GetButtonDown("Fire1")) {
      FrontalShoot();
    }
    if (Input.GetButtonDown("Fire2")) {
      SideShoot();
    }
  }

  void FrontalShoot() {
    Instantiate(bullet, spawnFrontalShoot.position, transform.rotation);
  }

  void SideShoot() {
    foreach (var leftShoot in spawnLeftShoot) {
      Instantiate(bullet, leftShoot.position, transform.rotation * Quaternion.Euler(0, 0, 90));
    }
    foreach (var rightShoot in spawnRightShoot) {
      Instantiate(bullet, rightShoot.position, transform.rotation * Quaternion.Euler(0, 0, -90));
    }
  }

  public void SetDamage(int damage) {
    health -= damage;
    healthBar.GetComponent<HealthBar>().Health(damage);
  }
}
