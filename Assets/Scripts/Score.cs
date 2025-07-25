using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI scoreText;
    private int currentScore;
    void Start()
    {
        currentScore = DataHolder.currentScore;
        scoreText.text = "Ñ÷¸ò: "+DataHolder.currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.CALC_SCORE, calculateScore);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.CALC_SCORE, calculateScore);
    }

    private void calculateScore()
    {
        currentScore += Mathf.FloorToInt((DataHolder.currentTime + 12.5f) * 15.4f);
        DataHolder.currentScore = currentScore;
        Write(currentScore);
    }
    public void Write(int score)
    {
        scoreText.text = "C÷¸ò: " + score.ToString();
    }
}
