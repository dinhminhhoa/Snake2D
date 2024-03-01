using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   public Data data;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
            Instance = this;
    }

    public int appleCurrentScore = 0;
    
    public bool isPlaying = false;

    public UnityEvent onPlay = new UnityEvent();
    public UnityEvent onGameOver = new UnityEvent();

    
    private void Start()
    {
        string loadedData = SaveSystem.Load("save");

        if (loadedData != null)
        {
            data = JsonUtility.FromJson<Data>(loadedData);
        }
        else
        {
            data = new Data();

        }
    }

    private void Update()
    {
        
    }

    public void StartGame()
    {
        onPlay.Invoke();
        isPlaying = true;
        Time.timeScale = 1f;
        appleCurrentScore = 0;
        
    }

    public void GameOver()
    {
        if (data.highscore < appleCurrentScore)
        {
            data.highscore = appleCurrentScore;

            string saveString = JsonUtility.ToJson(data);

            SaveSystem.Save("save", saveString);
        }

        isPlaying = false;
        Time.timeScale = 0;
        onGameOver.Invoke();

       
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public string AppleScoreUI()
    {
        return Mathf.Round(appleCurrentScore).ToString();
    }

    public string HighScoreUI() 
    {
        return Mathf.Round(data.highscore).ToString();
    }


}
