using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class VolumeValue : MonoBehaviour
{
    [SerializeField] Slider slider;
    private AudioSource AudioSrc;
    private float musicVolume = 1f;
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        slider.value = PlayerPrefs.GetFloat("Volume");
        AudioSrc = GetComponent<AudioSource>();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            AudioSrc.Stop();
        }
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
