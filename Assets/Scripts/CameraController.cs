using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour {
  public Transform player;

  float smoothSpeed = 0.15f;
  Vector3 zPosition;

  void Start() {
    zPosition = new Vector3(0, 0, -10);
  }

  void FixedUpdate() {
    if (player) {
      Vector3 smoothedPosition = Vector3.Lerp(transform.position, player.position, smoothSpeed);
      transform.position = smoothedPosition + zPosition;
    }
  }
}
