using System.Collections;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour {
  public float speed = 1.5f;
  public float rotationSpeed = 60;

  Rigidbody2D rb;
  Vector3 rotatePosition;
  bool canMove = true;

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
    transform.Rotate(rotatePosition * rotationSpeed * Time.deltaTime, Space.World);
  }

  IEnumerator RandomRotate() {
    transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));

    while (canMove) {
      bool changeRotate = Random.Range(0, 100) < 40;

      if (changeRotate) {
        rotatePosition = new Vector3(0, 0, Random.Range(-1.0f, 1.0f));
      }

      yield return new WaitForSeconds(1);
    }
  }
}
