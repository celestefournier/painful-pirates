using System.Collections;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour {
  public bool canMove = true;
  public float speed = 1.5f;
  public float shootSpeed = 1;
  public float rotationSpeed = 60;
  public GameObject healthBar;
  public GameObject bullet;

  Rigidbody2D rb;
  Transform player;
  Coroutine shootCoroutine;
  Vector3 rotatePosition;
  Vector3 healthBarPos;
  bool followPlayer;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    StartCoroutine(RandomRotate());
    healthBarPos = healthBar.transform.localPosition;
    healthBar.transform.localPosition = transform.localPosition + healthBarPos;
  }

  void Update() {
    if (canMove) {
      Movement();
    }
  }

  void Movement() {
    if (followPlayer) {
      float distance = Vector2.Distance(transform.position, player.position);

      if (distance > 3) {
        rb.velocity = transform.up * speed;
      }

      Vector2 diff = transform.position - player.position;
      float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90;
      transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    else {
      rb.velocity = transform.up * speed;
      transform.Rotate(rotatePosition * rotationSpeed * Time.deltaTime, Space.World);
    }

    healthBar.transform.localPosition = transform.localPosition + healthBarPos;
  }

  IEnumerator RandomRotate() {
    transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

    while (canMove) {
      if (followPlayer) {
        yield return new WaitForSeconds(1);
      }

      bool changeRotate = Random.Range(0, 100) < 40;

      if (changeRotate) {
        rotatePosition = new Vector3(0, 0, Random.Range(-1.0f, 1.0f));
      }

      yield return new WaitForSeconds(1);
    }
  }

  IEnumerator Shoot() {
    while (canMove) {
      Vector2 diff = transform.position - player.position;
      float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90;
      Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));

      yield return new WaitForSeconds(shootSpeed);
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Player") {
      player = other.gameObject.transform;
      shootCoroutine = StartCoroutine(Shoot());
      followPlayer = true;
    }
  }
}
