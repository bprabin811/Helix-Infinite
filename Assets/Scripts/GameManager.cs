using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;


public class GameManager : MonoBehaviour
{
    public AudioSource nxtLvl;
    public Button startButton;
    public Button exitButton;
   
    public int best;
    public int score;
  
    public int currentStage = 0;
    public static GameManager singleton;


    // Start is called before the first frame update
    private void Start()
    {
        nxtLvl = GetComponent<AudioSource>();
    }
    void Awake()
    {
        Advertisement.Initialize("4225047");
        Time.timeScale = 0f;
        Button btn = exitButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
        
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);
        best = PlayerPrefs.GetInt("Highscore");
    }
  
    void TaskOnClick()
    {
        Application.Quit();
        Debug.Log("You tuched button");
    }
    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
       
    }
    private void OnDisable()
    {
        startButton.onClick.RemoveListener(StartGame);
    }
    private void StartGame()
    {

        Time.timeScale = 1f;
        //Hide button
        startButton.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
        
    }
    public void NextLevel()
    {
        nxtLvl.Play();
        //  Debug.Log("Next level called");
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);


    }

    public void RestartLevel()
    {
        Debug.Log("show ads");
        //show adds
        //4225047
        Advertisement.Show();

        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        //reload stage
        FindObjectOfType<HelixController>().LoadStage(currentStage);

    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if(score > best)
        {
            best = score;
            //store high/best in Playerprefs
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
   
}