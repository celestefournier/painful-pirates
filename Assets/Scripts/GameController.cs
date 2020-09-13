using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour {
  public GameObject[] enemies;
  public float enemySpawnTime = 4;

  Vector2 spawnStart, spawnEnd;
  bool timeEnd;

  void Start() {
    spawnStart = new Vector2(-13, 11);
    spawnEnd = new Vector2(28, -25);
    StartCoroutine(SpawnEnemy());
  }

  IEnumerator SpawnEnemy() {
    while (!timeEnd) {
      Vector2 randomPos = new Vector2(
        Random.Range(spawnStart.x, spawnEnd.x),
        Random.Range(spawnStart.y, spawnEnd.y)
      );
      RaycastHit2D hit = Physics2D.BoxCast(randomPos, new Vector2(2.5f, 2.5f), 0, Vector2.zero);

      while (hit.collider != null) {
        randomPos = new Vector2(
          Random.Range(spawnStart.x, spawnEnd.x),
          Random.Range(spawnStart.y, spawnEnd.y)
        );
        hit = Physics2D.BoxCast(randomPos, new Vector2(2.5f, 2.5f), 0, Vector2.zero);
      }

      GameObject enemy = enemies[Random.Range(0, enemies.Length)];
      Instantiate(enemy, randomPos, Quaternion.identity);

      yield return new WaitForSeconds(enemySpawnTime);
    }
  }
}
