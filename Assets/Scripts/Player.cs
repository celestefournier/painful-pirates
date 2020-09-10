using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {
  public float speed = 2;
  public float rotationSpeed = 50;

  Rigidbody2D rb;

  void Start() {
    rb = GetComponent<Rigidbody2D>();
  }

  void Update() {
    Movement();
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
  }
}
