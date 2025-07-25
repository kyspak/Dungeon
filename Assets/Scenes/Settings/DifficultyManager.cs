using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [Header("1.Время на ответ." +
        " 2.Урон по противнику" +
        " 3.Урон по мне")]
    [SerializeField] public List<float> diff0Settings;
    [SerializeField] public List<float> diff1Settings;
    [SerializeField] public List<float> diff2Settings;
    [SerializeField] public TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        dropdown.value = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT Difficulty FROM Player WHERE nickname='{DataHolder.currentProfileName}';"))-1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDifficultyChange(TMP_Dropdown dropdown)
    {

        switch (dropdown.value)
        {
            case 0:
                Debug.Log("Легко");
                ChangeDifficulty(dropdown.value, diff0Settings);
                break;
            case 1:
                Debug.Log("Средне");
                ChangeDifficulty(dropdown.value, diff1Settings);
                break;
            case 2:
                Debug.Log("Тяжело");
                ChangeDifficulty(dropdown.value, diff2Settings);
                break;
        }
        
    }

    public void CallDifficultyChange()
    {
        int i = int.Parse(MyDataBase.ExecuteQueryWithAnswer($"SELECT Difficulty FROM Player WHERE nickname='{DataHolder.currentProfileName}';")) - 1;
        switch (i)
        {
            case 0:
                Debug.Log("Легко");
                ChangeDifficulty(i, diff0Settings);
                break;
            case 1:
                Debug.Log("Средне");
                ChangeDifficulty(i, diff1Settings);
                break;
            case 2:
                Debug.Log("Тяжело");
                ChangeDifficulty(i, diff2Settings);
                break;
        }
    }

    private void OnEnable()
    {
        Messenger<int, List<float>>.AddListener(GameEvent.SET_DIFF_SETTINGS, ChangeDifficulty);
    }
    private void OnDisable()
    {
        Messenger<int, List<float>>.RemoveListener(GameEvent.SET_DIFF_SETTINGS, ChangeDifficulty);
    }

    public void ChangeDifficulty(int difficulty, List<float> diffSettings) 
    {
        DataHolder.difficultySettings = diffSettings;
        MyDataBase.ExecuteQueryWithoutAnswer($"UPDATE Player SET Difficulty={difficulty+1} WHERE nickname='{DataHolder.currentProfileName}';");
    }
}
