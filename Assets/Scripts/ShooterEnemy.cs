using System.Collections;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour {
  public GameObject bullet;
  public float speed = 1.5f;
  public float shootSpeed = 1;
  public float rotationSpeed = 60;
  public bool canMove = true;
  public GameObject player;

  Rigidbody2D rb;
  Vector3 rotatePosition;
  Coroutine shootCoroutine;
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
      float distance = Vector2.Distance(transform.position, player.transform.position);
      
      if (distance > 3) {
        rb.velocity = transform.up * speed;
      }

      Vector2 diff = transform.position - player.transform.position;
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

  IEnumerator Shoot(GameObject player) {
    while (canMove) {
      Vector2 diff = transform.position - player.transform.position;
      float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90;
      Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));

      yield return new WaitForSeconds(shootSpeed);
    }
  }

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Player") {
      shootCoroutine = StartCoroutine(Shoot(other.gameObject));
      followPlayer = true;
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.tag == "Player") {
      StopCoroutine(shootCoroutine);
      followPlayer = false;
    }
  }
}
