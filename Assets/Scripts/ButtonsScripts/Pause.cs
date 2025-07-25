using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public float timer;
    public bool ispuse;
    public bool guipuse;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject settings;

    private void Start()
    {
        pause.SetActive(false);
    }
    
    void Update()
    {
        Time.timeScale = timer;
        
        
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            Messenger<bool, GameObject>.Broadcast(GameEvent.FREEZE, true, GameObject.FindGameObjectWithTag("Player"));
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true && GameObject.Find("HUD")==null)
        {
            Messenger<bool, GameObject>.Broadcast(GameEvent.FREEZE, false, GameObject.FindGameObjectWithTag("Player"));
            ispuse = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true && GameObject.Find("HUD").activeInHierarchy)
        {
            ispuse = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;
        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;
        }
    }
    public void OnGUI()
    {
        if (guipuse == true)
        {
            pause.SetActive(true);
        }
        else
        {
            pause.SetActive(false);
        }
    }

    public void Resume()
    {
        if(GameObject.Find("HUD") == null)
        {
            Messenger<bool, GameObject>.Broadcast(GameEvent.FREEZE, false, GameObject.FindGameObjectWithTag("Player"));
            ispuse = false;
        }
        else
        {
            ispuse = false;
        }
    }
    public void ToSettings()
    {
        settings.SetActive(true);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
