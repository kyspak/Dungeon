using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinWindow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI finalScore;
    void Start()
    {
        MyDataBase.ExecuteQueryWithoutAnswer($"UPDATE Scores SET score={DataHolder.currentScore} WHERE (SELECT id FROM Player WHERE nickname='{DataHolder.currentProfileName}');");
        finalScore.text = "Набранные очки: " + DataHolder.currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ToNextLevel(int i)
    {
        MyDataBase.ExecuteQueryWithoutAnswer($"UPDATE Player SET locations={i - 1} WHERE nickname='{DataHolder.currentProfileName}'");
        DataHolder.locations = i-1;
        SceneManager.LoadScene(i);
    }
}
