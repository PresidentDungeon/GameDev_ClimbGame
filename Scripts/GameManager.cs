using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScoreRating))]

public class GameManager : MonoBehaviour
{
    
    private static GameManager instance = null;
    private ScoreRating scoreRating;

    [SerializeField] MenuScript menuScript;
    [SerializeField] Timer timerScript;
    [SerializeField] GameObject player;
    private new AudioSource audio;

    [HideInInspector] public Vector3 startingLocation;
    [HideInInspector] public Quaternion startingRotation;

    private bool gameStarted = false;
    private bool loadingDisplay = false;
    [HideInInspector] public bool gamePaused = false;
    public bool completeRun { get; set; }

    public static GameManager GetInstance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else
        {
            instance = this;
            this.scoreRating = GetComponent<ScoreRating>();
            this.audio = GetComponent<AudioSource>();
            Time.timeScale = 1;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        //Pause menu pull up or pull down
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted && !loadingDisplay)
        {
            if (gamePaused){ResumeGame();}
            else{PauseGame();}
            this.gamePaused = !gamePaused;
            Cursor.visible = gamePaused;
        }

        //Instantiate play-state - stops showing load screen
        if (Input.GetKeyDown(KeyCode.Space) && loadingDisplay)
        {
            gameStarted = true;
            gamePaused = false;
            loadingDisplay = false;
            menuScript.hidePanels();

            Camera[] cameras = GameObject.FindObjectsOfType<Camera>(); 

            foreach (Camera camera in cameras)
            {
                camera.gameObject.SetActive(false);                     //Sets animated camera inactive, since Player has its own camera
            }

            Instantiate(player);
            timerScript.startTimer();
        }
    }

    //Starts game with the first level
    public void startGame()
    {
        loadLevel("Level 1");
        this.gameStarted = true;
        Cursor.visible = false;
    }

    private void PauseGame()
    {
        this.menuScript.goToMain();
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;                         
    }

    private void ResumeGame()
    {
        this.menuScript.hidePanels();
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;                         
    }

    //Reloads the active scene
    public void restartLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        loadLevel(scene.name);
    }

    //Stops running the application
    public void exitGame()
    {
        Application.Quit();
    }

    public void loadLevel(string sceneName)
    {
        gameStarted = false;
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        loadingDisplay = true;
        menuScript.displayLoaded();
        menuScript.displayResume();
        timerScript.stopTimer();
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void restartGame()
    {
        gameStarted = false;
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    public void OnSceneUnloaded(Scene scene)
    {
        this.menuScript.restartScript();
        instance = null;
        Destroy(gameObject);
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    //Plays a specific audioclip in the Audiosource
    public void playSong(AudioClip audioClip)
    {
        this.audio.clip = audioClip;
        startPlayingAudio();
    }
    //Plays the audio
    public void startPlayingAudio()
    {
        this.audio.Play();
    }
    //Stops playing the audio
    public void stopPlayingAudio()
    {
        this.audio.Stop();
    }
    //Updates the value for the volume
    public void updateAudioVolume(float volume)
    {
        this.audio.volume = volume;
    }

    public void finishLevel()
    {
        gameStarted = false;
        float timeElapsed = timerScript.stopTimer();
        timerScript.addTotalTime();
        string currentSceneName = SceneManager.GetActiveScene().name;
        Rating rating = scoreRating.calculateScore(currentSceneName, timeElapsed);
        scoreRating.saveScore(currentSceneName, timeElapsed);
        menuScript.displayFinish(timeElapsed, rating);
    }

    public void finishGame()
    {
        gameStarted = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
        gamePaused = true;


        float timeElapsed = timerScript.stopTimer();
        timerScript.addTotalTime();
        float totalTimeElapsed = timerScript.totalTime;
        string currentSceneName = SceneManager.GetActiveScene().name;

        Rating rating = scoreRating.calculateScore(currentSceneName, timeElapsed);
        scoreRating.saveScore(currentSceneName, timeElapsed);

        if (completeRun)
        {
            scoreRating.saveTotalScore(totalTimeElapsed);
        }

        menuScript.displayGameFinish(timeElapsed, (completeRun) ? totalTimeElapsed : (float?) null, rating);
    }
}
