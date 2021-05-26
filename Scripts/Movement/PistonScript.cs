using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonScript : MonoBehaviour
{
    [SerializeField] float suspendTimeBack;
    [SerializeField] float suspendTimeFront;
    [SerializeField] float initialDelay;
    private float timeElapsed;
    public float lerpDuration;
    public Vector3 endLocation;
    private Vector3 startLocation;

    public bool forwards { get; private set; }
    public bool isWaiting { get; private set; }
    public float velocity { get; private set; }
    private Vector3 previousLocation;
    private bool waitingDone = false;

    void Start()
    {
        StartCoroutine(StartDelay());
        this.startLocation = transform.position;
        previousLocation = transform.position;
    }

    private void FixedUpdate()
    {
        if (waitingDone) 
        { 
            if (!isWaiting)
            {

                if (timeElapsed < lerpDuration && !forwards)
                {
                    transform.position = Vector3.Lerp(startLocation, endLocation, timeElapsed / lerpDuration);
                    timeElapsed += Time.deltaTime;
                }
                else if (timeElapsed < lerpDuration && forwards)
                {
                    transform.position = Vector3.Lerp(endLocation, startLocation, timeElapsed / lerpDuration);
                    timeElapsed += Time.deltaTime;
                }

                else
                {
                    isWaiting = true;
                    StartCoroutine(Delay());
                }
            }

            velocity = (transform.position.y - previousLocation.y) / Time.deltaTime;
            previousLocation = transform.position;
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds((forwards) ? suspendTimeBack : suspendTimeFront);
        forwards = !forwards;
        timeElapsed = 0;
        isWaiting = false;
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(initialDelay);
        this.waitingDone = true;
    }

}
