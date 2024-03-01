using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI appleScore;
    [SerializeField] private GameObject startMenuUI;
    [SerializeField] private GameObject gameOverUI;

    [SerializeField] private TextMeshProUGUI gameOverScoreUI;
    [SerializeField] private TextMeshProUGUI highScoreUI;

    GameManager gm;
    private void Start()
    {
        gm = GameManager.Instance;
        gm.onGameOver.AddListener(GameOverUI);
    }

    public void PlayButtion()
    {
        gm.StartGame();
    }

    public void QuitButton()
    {
        gm.QuitGame();
    }

    public void GameOverUI()
    {
        gameOverUI.SetActive(true);

        gameOverScoreUI.text = "Score:" + gm.AppleScoreUI();
        highScoreUI.text = "Highscore:" + gm.HighScoreUI();
    }

    private void OnGUI()
    {
        appleScore.text = gm.AppleScoreUI();
    }

}
