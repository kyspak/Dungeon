using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameVolume : MonoBehaviour
{
    [SerializeField] Slider slider;
    private AudioSource AudioSrc;
    private float musicVolume = 1f;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("Volume");
        AudioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        AudioSrc.volume = musicVolume;
    }

    public void SetVolume()
    {
        musicVolume = slider.value;
        PlayerPrefs.SetFloat("Volume", musicVolume);
    }
}
