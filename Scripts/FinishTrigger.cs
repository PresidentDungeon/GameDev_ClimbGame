using System.Collections;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private string nextScene;
    private bool isActive = false;
    private GameManager manager;

    private void Start()
    {
        this.manager = GameManager.GetInstance;     //Makes calls to GameManager methods possible
    }

    private void OnTriggerEnter(Collider other)
    {
        //Ends the level, when Player enters collider
        if (!this.isActive && other.tag == "Player")
        {
            this.isActive = true;                   //Boolean to make sure it only runs one time
            manager.finishLevel();                  //Call to GameManager
            StartCoroutine(loadCountdown());
        }
    }

    //Waits for 5 seconds before loading next level
    public IEnumerator loadCountdown()
    {
        yield return new WaitForSeconds(5);
        manager.loadLevel(nextScene);
        this.isActive = false;
    }
}
