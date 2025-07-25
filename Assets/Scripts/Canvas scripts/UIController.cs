using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("HUD-û")]
    [SerializeField] Timer timer;
    [SerializeField] Demo PDollar;
    [SerializeField] QuesCanvas quescanvas;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject DHUD;
    [SerializeField] GameObject KHUD;
    // Start is called before the first frame update
    private void Awake()
    {
        timer.Close();
        
    }
    void Start()
    {
        quescanvas.Close();
        PDollar.Close();
    }

    void OnEnable()
    {
        Messenger<float>.AddListener(GameEvent.TIMER_START, OnTimerStart);
        Messenger.AddListener(GameEvent.TIMER_STOP, OnTimerStop);
        Messenger.AddListener(GameEvent.HEALTH_ZERO_UI, OnEnemyDeath);
        Messenger<float>.AddListener(GameEvent.RAY_HIT, OnHit);
    }
    void OnDisable()
    {
        Messenger<float>.RemoveListener(GameEvent.TIMER_START, OnTimerStart);
        Messenger.RemoveListener(GameEvent.TIMER_STOP, OnTimerStop);
        Messenger.RemoveListener(GameEvent.HEALTH_ZERO_UI, OnEnemyDeath);
        Messenger<float>.RemoveListener(GameEvent.RAY_HIT, OnHit);
    }

    private void OnHit(float mechType)
    {
        switch(mechType)
        {
            case 1: 
                quescanvas.Open();
                break;
            case 2:
                PDollar.Open();
                HUD.SetActive(true);
                DHUD.SetActive(true);
                break;
            case 3:
                this.GetComponent<KeyBattle>().enabled = true;
                HUD.SetActive(true);
                KHUD.SetActive(true);
                break;
        }
        
    }

    private void OnEnemyDeath()
    {
        this.GetComponent<KeyBattle>().enabled = false;
        quescanvas.Close();
        DHUD.SetActive(false);
        KHUD.SetActive(false);
        PDollar.Close();
    }

    private void OnTimerStart(float time)
    {
        
        timer.Open(time);
    }

    private void OnTimerStop()
    {
        timer.TimerStop();
    }
}
