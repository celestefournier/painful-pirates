using System.Collections;
using UnityEngine;

public class ChaserEnemy : MonoBehaviour {
  public bool canMove = true;
  public float speed = 1.5f;
  public float rotationSpeed = 60;
  public int damage = 50;
  public GameObject explosion;

  Rigidbody2D rb;
  Transform player;
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
    rb.velocity = transform.up * speed;

    if (followPlayer) {
      if (!player) {
        followPlayer = false;
        return;
      }
      Vector2 diff = transform.position - player.position;
      float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg + 90;
      transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    else {
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

  void OnTriggerEnter2D(Collider2D other) {
    if (other.gameObject.tag == "Player") {
      player = other.gameObject.transform;
      followPlayer = true;
    }
  }

  void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.tag == "Player") {
      player = null;
      followPlayer = false;
    }
  }

  void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag == "Player") {
      other.gameObject.GetComponent<Health>().SetDamage(damage);
      GetComponent<Health>().health = 0;
    }
  }
}
