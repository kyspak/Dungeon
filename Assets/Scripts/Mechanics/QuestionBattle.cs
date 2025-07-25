using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

//using static UnityEditor.Experimental.GraphView.GraphView;

public class QuestionBattle : MonoBehaviour
{
    System.Random rand = new System.Random();
    public TextMeshProUGUI Quest_text;
    public List<GameObject> Buttons = new List<GameObject>();
    public List<Quest> Quest_List = new List<Quest>();
    private int i=1;
    // Start is called before the first frame update
    void Start()
    {
        i = rand.Next(0, Quest_List.Count);
        NextQuest(i);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Answer(GameObject button)
    {
        ButtonClick buttonClick = button.GetComponent<ButtonClick>();
        Messenger.Broadcast(GameEvent.PICK_TIME);
        

        Messenger<float>.Broadcast(GameEvent.TIMER_START, DataHolder.difficultySettings[0]);
        

        if (buttonClick.buttons_answer)
        {
            
            Messenger<float>.Broadcast(GameEvent.CORRECT_ANSW, DataHolder.difficultySettings[1]);
            /*if (i < Quest_List.Count-1)
            {
                i++;
            }*/
            i = rand.Next(0, Quest_List.Count);
            NextQuest(i);
            Messenger.Broadcast(GameEvent.CALC_SCORE);
        }
        else
        {
            //Quest_text.text = "Ответ не правильный";
            Messenger<float>.Broadcast(GameEvent.MISTAKE, DataHolder.difficultySettings[2]);
        }

    }

    public void NextQuest(int j)
    {
        
        Quest_text.text = Quest_List[j].Quest_str1;
        if (Buttons.Count >= Quest_List[j].Button_bools.Count)
        {
            for (int i = 0; i<Buttons.Count; i++)
            {
                if (i < Quest_List[j].Button_bools.Count)
                {
                    Buttons[i].SetActive(true);
                    Buttons[i].GetComponent<ButtonClick>().buttons_answer = Quest_List[j].Button_bools[i];
                    if (i < Quest_List[j].Button_name.Count)
                    {
                        Buttons[i].GetComponent<ButtonClick>().NameButtons(Quest_List[j].Button_name[i]);
                    }
                }
                else
                {
                    Buttons[i].SetActive(false);
                }
            }
        }
    }
}
[System.Serializable]
public struct Quest
{
    public string Quest_str1;
    public List<string> Button_name;
    public List<bool> Button_bools;
}
