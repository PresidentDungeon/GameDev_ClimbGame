using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] private Canvas UIMain;
    [SerializeField] private Canvas LoadingCanvas;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject restartButton;

    [SerializeField] private FinishCanvas finishScript;
    [SerializeField] private FinishGameCanvas finishGameScript;
    [SerializeField] private LevelCanvas levelScript;

    private static MenuScript instance = null;

    private void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
            this.optionsCanvas.gameObject.SetActive(false);
            this.finishScript.gameObject.SetActive(false);
        }
        DontDestroyOnLoad(this.gameObject);
        
    }

    public void goToOptions()
    {
        this.optionsCanvas.gameObject.SetActive(true);
        this.UIMain.gameObject.SetActive(false);
    }

    public void goToLevel()
    {
        this.levelScript.displayCanvas();
        this.UIMain.gameObject.SetActive(false);
    }

    public void goToMain()
    {
        this.optionsCanvas.gameObject.SetActive(false);
        this.levelScript.hideCanvas();
        this.UIMain.gameObject.SetActive(true);
    }

    public void hidePanels()
    {
        this.UIMain.gameObject.SetActive(false);
        this.optionsCanvas.gameObject.SetActive(false);
        this.levelScript.hideCanvas();
        LoadingCanvas.gameObject.SetActive(false);

        finishScript.hideFinish();
    }

    public void displayResume()
    {
        startButton.SetActive(false);
        restartButton.SetActive(true);
    }

    public void displayLoaded()
    {
        hidePanels();
        LoadingCanvas.gameObject.SetActive(true);
    }

    public void displayFinish(double elapsedTime, Rating rating)
    {
        finishScript.displayFinish(elapsedTime, rating);
    }

    public void displayGameFinish(float elapsedTime, float? totalElapsedTime, Rating rating)
    {
        finishGameScript.displayFinish(elapsedTime, totalElapsedTime, rating);
    }

    public void restartScript()
    {
        instance = null;
        Destroy(gameObject);
    }
}
