using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    public static GameController instance;

    public GameObject enemyContainer, hudContainer, gameOverPanel;
    public Text enemyCounter, timeCounter;
    public bool gamePlaying { get; private set; }

    private int numberTotalEnemies, numberKilledEnemies;
    private float startTime, elapsedTime;
    TimeSpan timePlayed;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        numberTotalEnemies = enemyContainer.transform.childCount;
        numberKilledEnemies = 0;
        enemyCounter.text = "Enemies: 0/" + numberTotalEnemies;
        gamePlaying = false;
        BeginGame();
    }

    // Update is called once per frame
    void Update() {
        if (gamePlaying) { 
            elapsedTime = Time.time - startTime;
            timePlayed = TimeSpan.FromSeconds(elapsedTime);

            string timePlayingStr = "Time: " + timePlayed.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;
        }

        
    }

    private void BeginGame() {
        gamePlaying = true;
        startTime = Time.time;
    }

    public void KillEnemy() {
        numberKilledEnemies++;
        string enemyCounterStr = "Enemies: " + numberKilledEnemies + "/" + numberTotalEnemies;
        enemyCounter.text = enemyCounterStr;
    }

    public void EndGame() {
        gamePlaying = false;
        Invoke("ShowGameOverScreen", 1.25f);
    }

    private void ShowGameOverScreen() {
        gameOverPanel.SetActive(true);
        hudContainer.SetActive(false);
    }
}
