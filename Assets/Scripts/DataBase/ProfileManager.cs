using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> profiles;
    [SerializeField] private List<Image> profilesImgs;
    [SerializeField] private List<TextMeshProUGUI> nicknames;
    [SerializeField] private List<TextMeshProUGUI> scores;
    [SerializeField] private List<GameObject> panels;
    [SerializeField] private List<GameObject> AddProfilePanels;
    [SerializeField] private AddProfileWindow addWind;
    [SerializeField] private Button exitButton;
    private Color color = new Vector4(159 / 255.0f, 255 / 255.0f, 255 / 255.0f, 1);
    private Color defaultColor = new Vector4(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 1);
    private DataTable namesTable;
    // Start is called before the first frame update
    void Start()
    {
        
        DataHolder.profileCount = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT COUNT(*) FROM Player;"));
        if (DataHolder.profileCount == 0)
        {

            exitButton.interactable = false;
        }

        // Получаем отсортированную таблицу лидеров
        /*DataTable scoreboard = MyDataBase.GetTable("SELECT * FROM Scores ORDER BY score DESC;");
       
        // Получаем id лучшего игрока
        int idBestPlayer = int.Parse(scoreboard.Rows[0][1].ToString());
        // Получаем ник лучшего игрока
        string nickname = MyDataBase.ExecuteQueryWithAnswer($"SELECT nickname FROM Player WHERE id = {1};");
        int score = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT score FROM Scores WHERE id=(SELECT id FROM Player WHERE nickname = '{nickname}');"));
        nicknames[0].text = nickname;
        scores[0].text = score.ToString();
        
        Debug.Log($"Лучший игрок {nickname} набрал {scoreboard.Rows[0][2].ToString()} очков.");*/
        findProfiles();
        for (int i = 0; i < DataHolder.profileCount; i++)
        {
            if (nicknames[i].text == DataHolder.currentProfileName && DataHolder.profileCount>0 && GameObject.FindGameObjectsWithTag("Activated").Length < 1)
            {
                activateProfile(i);
                profiles[i].tag = "Activated";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Activated").Length < 1)
        {
            exitButton.interactable = false;
        }
        else
        {
            exitButton.interactable = true;
        }
        
    }

    public void findProfiles()
    {
        namesTable = MyDataBase.GetTable("SELECT nickname FROM PLayer;");
        for (int i=0; i < DataHolder.profileCount; i++)
        {
            AddProfilePanels[i].SetActive(false);
            //nicknames[i].text= MyDataBase.ExecuteQueryWithAnswer($"SELECT nickname FROM Player WHERE id = {i+1};");
            
            nicknames[i].text = namesTable.Rows[i][0].ToString();
            scores[i].text = MyDataBase.ExecuteQueryWithAnswer($"SELECT score FROM Scores WHERE id_player=(SELECT id FROM Player WHERE nickname = '{nicknames[i].text}');");
            panels[i].SetActive(true);
        }
        
    }

    public void deleteProfile(int i)
    {
        MyDataBase.ExecuteQueryWithoutAnswer($"DELETE FROM Scores WHERE id_player= (SELECT id FROM Player WHERE nickname = '{nicknames[i].text}');");
        MyDataBase.ExecuteQueryWithoutAnswer($"DELETE FROM Player WHERE nickname='{nicknames[i].text}';");
        DataHolder.profileCount = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT COUNT(*) FROM Player;"));
        profiles[i].tag = "Deactivated";
        panels[i].SetActive(false);
        AddProfilePanels[i].SetActive(true);
    }

    public void activateProfile(int i)
    {
        Debug.Log("Add");
        DataHolder.profileCount = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT COUNT(*) FROM Player;"));
        for (int j = 0; j < DataHolder.profileCount; j++)
        {
            profilesImgs[j].color = defaultColor;
            profiles[j].tag = "Deactivated";
            profilesImgs[j].GetComponent<Button>().enabled = true;
        }
        DataHolder.currentProfileName = nicknames[i].text;
        DataHolder.currentScore = int.Parse(scores[i].text);
        DataHolder.locations = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT locations FROM Player WHERE nickname='{DataHolder.currentProfileName}';"));
        DataHolder.difficulty = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT Difficulty FROM Player WHERE nickname='{DataHolder.currentProfileName}';"));
        PlayerPrefs.SetString("nickname", DataHolder.currentProfileName);
        profilesImgs[i].color=color;
        //Debug.Log("Изменение цвета " + i);
        profilesImgs[i].GetComponent<Button>().enabled = false;
        profiles[i].tag = "Activated";
        exitButton.interactable = true;
    }

    public void addProfile(int i)
    {
        Messenger<int>.Broadcast(GameEvent.ADDPROFILEWINDOW_OPEN, i);
    }

    private void OnWrite(int i)
    {
        namesTable = MyDataBase.GetTable("SELECT nickname FROM PLayer;");
        AddProfilePanels[i].SetActive(false);
        nicknames[i].text = namesTable.Rows[namesTable.Rows.Count-1][0].ToString();
        scores[i].text = MyDataBase.ExecuteQueryWithAnswer($"SELECT score FROM Scores WHERE id=(SELECT id FROM Player WHERE nickname = '{nicknames[i].text}');");
        
        panels[i].SetActive(true);
        activateProfile(i);
    }

    private void OnOpen(int i)
    {
        addWind.Open(i);
        exitButton.interactable = true;
        
    }
    private void OnEnable()
    {
        Messenger<int>.AddListener(GameEvent.ADDPROFILEWINDOW_OPEN, OnOpen);
        Messenger<int>.AddListener(GameEvent.WRITE_PROFILE, OnWrite);
    }
    private void OnDisable()
    {
        Messenger<int>.RemoveListener(GameEvent.ADDPROFILEWINDOW_OPEN, OnOpen);
        Messenger<int>.RemoveListener(GameEvent.WRITE_PROFILE, OnWrite);
    }

}
