using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject Settings;
    [SerializeField] GameObject Profiles;
    [SerializeField] GameObject Notification;
    [SerializeField] GameObject NotificationChoose;
    [SerializeField] DifficultyManager difficultyManager;

    // Start is called before the first frame update
    void Start()
    {
        DataHolder.profileCount = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT COUNT(*) FROM Player;"));
        if (DataHolder.profileCount == 0)
        {
            Notification.SetActive(true);
            Profiles.SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenSettings()
    {
        Settings.SetActive(true);
    }
    public void GoToLocationChoose()
    {
        if (DataHolder.currentProfileName == "")
        {
            NotificationChoose.SetActive(true );
            Profiles.SetActive(true);
        }
        else
        {
            difficultyManager.CallDifficultyChange();
            SceneManager.LoadScene(1);
        }
        
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        Settings.SetActive(false);
    }

    public void GoBackPR()
    {
        Profiles.SetActive(false);
    }

    public void OpenProfiles()
    {
        Profiles.SetActive(true);
    }

    public void CloseNotification()
    {
        Notification.SetActive(false);
    }
}
