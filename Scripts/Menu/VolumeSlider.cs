using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private GameManager manager;

    private void Start()
    {
        this.manager = GameManager.GetInstance;
    }

    public void onChange()
    {
        float volume = GetComponent<Slider>().value;
        manager.updateAudioVolume(volume);
    }
}
