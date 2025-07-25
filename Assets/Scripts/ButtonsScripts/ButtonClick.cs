using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public bool answer;
    public bool buttons_answer
    {
        get { return (answer); }
        set { answer = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void NameButtons(string name)
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
