using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    /* [SerializeField] public float time=20f;
     [SerializeField] private TextMeshProUGUI timerText;
     [SerializeField] private Image timerImage;

     private float _timeLeft = 0f;

     private IEnumerator StartTimer()
     {

         while (_timeLeft > 0)
         {
             _timeLeft -= Time.deltaTime;
             var normalizedValue = Mathf.Clamp(_timeLeft / time, 0.0f, 1.0f);
             timerImage.fillAmount = normalizedValue;
             UpdateTimeText();
             //Debug.Log(_timeLeft);
             yield return null;
         }
     }
     private void Start()
     {
         _timeLeft = time;
         StartCoroutine(StartTimer());
     }

     private void UpdateTimeText()
     {
         if (_timeLeft < 0)
         {
             _timeLeft = 0;
             Messenger<float>.Broadcast(GameEvent.MISTAKE, 0.5f);
             StopCoroutine(StartTimer());

         }

         float minutes = Mathf.FloorToInt(_timeLeft / 60);
         float seconds = Mathf.FloorToInt(_timeLeft % 60);
         //Debug.Log("Timer" + seconds);
         timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
     }*/

    [SerializeField] private Image timerImage;
    private float time;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float damage = 0.5f;

    private float _timeLeft = 0f;
    private bool _timerOn = false;

    public void Open(float settime)
    {
        //StartCoroutine(StartTimer());
        Debug.Log("Started when" + _timeLeft);
        time = settime;
        _timeLeft = settime;
        _timerOn = true;
        gameObject.SetActive(true);

    }
    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void TimerStop()
    {
        Debug.Log("Finished when" + _timeLeft);
        gameObject.SetActive(false);
    }
    private void Start()
    {
        _timeLeft = time;
        _timerOn = true;
    }

    private void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                var normalizedValue = Mathf.Clamp(_timeLeft / time, 0.0f, 1.0f);
                timerImage.fillAmount = normalizedValue;
                UpdateTimeText();
            }
            else
            {
                _timeLeft = time;
                Messenger<float>.Broadcast(GameEvent.MISTAKE, DataHolder.difficultySettings[2]);
                _timerOn = false;
            }
        }
    }

    private void OnEnable()
    {
        Messenger.AddListener(GameEvent.PICK_TIME, Pick);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameEvent.PICK_TIME, Pick);
    }

    private void Pick()
    {
        DataHolder.currentTime =  _timeLeft;
        Debug.Log(DataHolder.currentTime);
    }
    private void UpdateTimeText()
    {
        if (_timeLeft < 0)
            _timeLeft = 0;

        float minutes = Mathf.FloorToInt(_timeLeft / 60);
        float seconds = Mathf.FloorToInt(_timeLeft % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
