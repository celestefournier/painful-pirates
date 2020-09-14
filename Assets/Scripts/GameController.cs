using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
  public float sessionTime;
  public float spawnTime;
  public GameObject[] enemies;
  public Text scoreText;
  public GameObject gameOverScreen;

  Vector2 spawnStart, spawnEnd;
  bool timeOut;
  int score;

  void Start() {
    sessionTime = PlayerPrefs.GetInt("SessionTime");
    spawnTime = PlayerPrefs.GetInt("SpawnTime");
    spawnStart = new Vector2(-13, 11);
    spawnEnd = new Vector2(28, -25);
    StartCoroutine(SpawnEnemy());
    StartCoroutine(TimeOut());
  }

  IEnumerator SpawnEnemy() {
    yield return new WaitForSeconds(spawnTime);

    while (!timeOut) {
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

      yield return new WaitForSeconds(spawnTime);
    }
  }

  IEnumerator TimeOut() {
    yield return new WaitForSeconds(sessionTime * 60);
    timeOut = true;
  }

  public void GameOver() {
    scoreText.text = score.ToString();
    gameOverScreen.SetActive(true);
  }

  public void SetScore() {
    score++;
  }

  public void PlayAgain() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void BackToMenu() {
    SceneManager.LoadScene("Menu");
  }
}
