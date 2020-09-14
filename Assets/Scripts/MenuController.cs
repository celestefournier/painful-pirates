using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
  public GameObject menu;
  public GameObject options;
  public Text sessionTimeText;
  public Text spawnTimeText;

  int sessionTime = 1;
  int spawnTime = 5;

  void Start() {
    if (PlayerPrefs.HasKey("SessionTime")) {
      sessionTime = PlayerPrefs.GetInt("SessionTime");
      sessionTimeText.text = sessionTime + "m";
    } else {
      PlayerPrefs.SetInt("SessionTime", sessionTime);
    }

    if (PlayerPrefs.HasKey("SpawnTime")) {
      spawnTime = PlayerPrefs.GetInt("SpawnTime");
      spawnTimeText.text = spawnTime + "s";
    } else {
      PlayerPrefs.SetInt("SpawnTime", spawnTime);
    }
  }

  public void StartGame() {
    SceneManager.LoadScene("Game");
  }

  public void ToggleOptions() {
    menu.SetActive(!menu.activeSelf);
    options.SetActive(!options.activeSelf);
  }

  public void OptionSessionTime(int time) {
    if (sessionTime + time >= 1 && sessionTime + time <= 3) {
      sessionTime += time;
      sessionTimeText.text = sessionTime + "m";
    }
  }

  public void OptionSpawnTime(int time) {
    if (spawnTime + time >= 1 && spawnTime + time <= 60) {
      spawnTime += time;
      spawnTimeText.text = spawnTime + "s";
    }
  }

  public void SaveConfig() {
    PlayerPrefs.SetInt("SessionTime", sessionTime);
    PlayerPrefs.SetInt("SpawnTime", spawnTime);
  }
}
