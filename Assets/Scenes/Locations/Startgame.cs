using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Startgame : MonoBehaviour
{
    [SerializeField] private List<Button> Buttons;
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i=0; i<=DataHolder.locations-1; i++)
        {
            Debug.Log(DataHolder.locations);
            Buttons[i].interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
