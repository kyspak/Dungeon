using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Random = UnityEngine.Random;

public class KeyBattle : MonoBehaviour
{
    private string keys = "qwertyuiopasdfghjklzxcvbnm1234567890";
    private string temp;
    private char[] charKeys;
    private int counter;
    
    [SerializeField] private TextMeshProUGUI TextBox;
    [Header("Настройки урона и времени")]
    public float playerDamage = 0.2f;
    public float enemyDamage = 0.1f;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        charKeys = keys.ToCharArray();
        counter = Random.Range(0, charKeys.Length);
        TextBox.text = charKeys[counter].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (String.Compare(Input.inputString, keys[counter].ToString()) == 0)
            {
                counter = Random.Range(0, charKeys.Length);
                TextBox.text = charKeys[counter].ToString().ToUpper();
                Messenger<float>.Broadcast(GameEvent.CORRECT_ANSW, DataHolder.difficultySettings[1]);
                Messenger.Broadcast(GameEvent.PICK_TIME);
                Messenger.Broadcast(GameEvent.CALC_SCORE);
                Messenger<float>.Broadcast(GameEvent.TIMER_START, DataHolder.difficultySettings[0]);
                Debug.Log("Da");
            }
            else
            {
                Messenger<float>.Broadcast(GameEvent.MISTAKE, DataHolder.difficultySettings[2]);
            }
        }
    }

  
}
