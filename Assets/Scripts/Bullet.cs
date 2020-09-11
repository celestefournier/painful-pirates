using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour {
  public float speed;

  void Update() {
    transform.Translate(Vector3.up * speed * Time.deltaTime);
  }
}
