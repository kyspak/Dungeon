using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class AddProfileWindow : MonoBehaviour
{
    [SerializeField] TMP_InputField input;
    [SerializeField] TextMeshProUGUI warn;
    private int button_number;

    public void Start()
    {
        warn.gameObject.SetActive(false);
    }
    public void OnAdd()
    {
        MyDataBase.ExecuteQueryWithoutAnswer($"INSERT INTO Player (nickname, locations) VALUES ('{input.text}', {1})");
        MyDataBase.ExecuteQueryWithoutAnswer($"INSERT INTO Scores (id_player, score) VALUES ((SELECT id FROM Player WHERE nickname='{input.text}'), {0})");
        DataHolder.nickname = input.text;
        Close();
    }

    public void Open(int i)
    {
        gameObject.SetActive(true);
        button_number = i;
    }

    public void Close()
    {
        input.text = "";
        Messenger<int>.Broadcast(GameEvent.WRITE_PROFILE, button_number);
        gameObject.SetActive(false);
    }
}
