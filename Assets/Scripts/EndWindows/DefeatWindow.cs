using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatWindow : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI finalScore;
    void Start()
    {
        finalScore.text = "Набранные очки: " + DataHolder.currentScore.ToString();

        try
        {
            DataHolder.currentScore = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT score FROM Scores WHERE id_player='{DataHolder.currentProfileName}'"));
        }
        catch
        {
            DataHolder.currentScore = 0;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Anew(int i)
    {
        
        SceneManager.LoadScene(i);
    }
}
