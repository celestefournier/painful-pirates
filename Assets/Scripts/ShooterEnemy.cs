using System.Collections;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour {
  public bool canMove = true;
  public float speed = 1.5f;
  public float shootSpeed = 1;
  public float rotationSpeed = 60;
  public float damage = 10;
  public GameObject bullet;

  Rigidbody2D rb;
  Transform player;
  Coroutine shootCoroutine;
  Vector3 rotatePosition;
  bool followPlayer;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
    StartCoroutine(RandomRotate());
  }

  void Update() {
    if (canMove) {
      Movement();
    }
  }

  void Movement() {
    if (followPlayer) {
      if (!player) {
        followPlayer = false;
        return;
      }

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
      if (!player) {
        yield break;
      }
      Vector2 diff = transform.position - player.position;
      float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90;
      GameObject bulletSpawn = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));
      bulletSpawn.GetComponent<Bullet>().Fire(gameObject.tag, damage);

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
