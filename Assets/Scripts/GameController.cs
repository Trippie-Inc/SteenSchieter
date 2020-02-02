using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public GameObject redLine;
    public bool cursorVisibility;
    private bool gameOver;
    private bool restart;
    private int score;
    void Start() {
        gameOver = false;
        restart = false;
        Cursor.visible = cursorVisibility;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
        GameObject.FindGameObjectWithTag("FirstPerson").GetComponent<Camera>().enabled = false;
    }
    void Update() {
        if (restart) {
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene("Main");
            }
        }
        if (Input.GetKeyDown(KeyCode.F1) && GameObject.FindGameObjectWithTag("FirstPerson").GetComponent<Camera>().enabled == false) {
            Debug.Log("false");
            GameObject.FindGameObjectWithTag("FirstPerson").GetComponent<Camera>().enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.F3) && GameObject.FindGameObjectWithTag("FirstPerson").GetComponent<Camera>().enabled == true) {
            Debug.Log("false");
            GameObject.FindGameObjectWithTag("FirstPerson").GetComponent<Camera>().enabled = false;
        }
    }
    IEnumerator SpawnWaves() {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
                if (gameOver) {
                    restartText.text = "Press 'R' to Restart!";
                    redLine.GetComponent<Image>().color = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
                    restart = true;
                    break;
                }
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    public void AddScore(int newScoreValue) {
        score += newScoreValue;
        UpdateScore();
    }
    void UpdateScore() {
        scoreText.text = "" + score;
        if (score < 0)
        {
            scoreText.color = new Color(253.0f / 255.0f, 66.0f / 255.0f, 66.0f / 255.0f);
        }
        else
        {
            scoreText.color = Color.white;
        }
    }
    public void GameOver() {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
