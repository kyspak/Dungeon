using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class SceneController : MonoBehaviour
{
    [Header("Противники")]
    [SerializeField] GameObject QuesEnemyPrefab;
    [SerializeField] GameObject DrawEnemyPrefab;
    [SerializeField] GameObject KeyEnemyPrefab;
    [Header("Здоровье")]
    [SerializeField] Healthbar healthbar;
    [SerializeField] Healthbar enemyHealthbar;
    [Header("Конечные окна")]
    [SerializeField] GameObject winWind;
    [SerializeField] GameObject defeatWind;
    private GameObject enemy;
    private int myTrigger;
    private int levelCounter=1;
    // Start is called before the first frame update
    void Start()
    {
        if (enemy == null)
        {
            if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                enemy = Instantiate(QuesEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(1.4f, 1, 76.5f);
                enemy = Instantiate(QuesEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(-15f, 1, 48f);
                enemy = Instantiate(QuesEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(16f, 1, 57.3f);
                enemy = Instantiate(DrawEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(0f, -13.6f, -23.17f);
                enemy = Instantiate(KeyEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(17f, -13.6f, -23.17f);
            }
            else if(SceneManager.GetActiveScene().buildIndex == 3)
            {
                winWind.SetActive(false);
                enemy = Instantiate(DrawEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(1.4f, 1, 76.5f);
                enemy = Instantiate(DrawEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(-19.3f, 1, 66.2f);
                enemy = Instantiate(DrawEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(-30.4f, 1, 82.5f);
                enemy = Instantiate(KeyEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(-30.4f, 1f, 49.1f);
                enemy = Instantiate(KeyEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(-51f, 1, 92.1f);
            }
            else if(SceneManager.GetActiveScene().buildIndex == 4)
            {
                enemy = Instantiate(KeyEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(1.4f, 1, 76.5f);
                enemy = Instantiate(KeyEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(1.4f, 1, 55f);
                enemy = Instantiate(KeyEnemyPrefab) as GameObject;
                enemy.transform.position = new Vector3(0, 15.5f, 21.8f);
            }
            
            /*float angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
            enemy = Instantiate(QuesEnemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(16.58f, 1, 2f);
            angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
            enemy = Instantiate(QuesEnemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(4.97f, 1, 20.3f);
            angle = Random.Range(0, 360);
            enemy.transform.Rotate(0, angle, 0);
            enemy = Instantiate(KeyEnemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(15, 1.5f, 85f);
            enemy = Instantiate(DrawEnemyPrefab) as GameObject;
            enemy.transform.position = new Vector3(10, 1.5f, 56.3f);
            
            */

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (enemyHealthbar.fill <= 0f)
        {
            Messenger.Broadcast(GameEvent.HEALTH_ZERO_UI);
            Messenger<bool, GameObject>.Broadcast(GameEvent.FREEZE, false, GameObject.FindGameObjectWithTag("Player"));
            Messenger.Broadcast(GameEvent.TIMER_STOP);
            
            Destroy(GameObject.Find("Enemytriggered"));
            
            Debug.Log("Killed");
            enemyHealthbar.fill = 1f;
            
        }
        else if(healthbar.fill <= 0f && myTrigger == 0)
        {
            Debug.Log("Вы проиграли");
            defeatWind.SetActive(true);
            myTrigger++;
        }
        if (GameObject.FindWithTag("Enemy") == null && myTrigger==0)
        {
            Messenger<bool, GameObject>.Broadcast(GameEvent.FREEZE, true, GameObject.FindGameObjectWithTag("Player"));
            Debug.Log("Игра пройдена!");
            myTrigger++;
            winWind.SetActive(true);
        }
    }

    void OnEnable()
    {
        Messenger<float>.AddListener(GameEvent.CORRECT_ANSW, OnCorrect);
        Messenger<float>.AddListener(GameEvent.MISTAKE, OnMistake);
        
    }
    void OnDisable()
    {
        Messenger<float>.RemoveListener(GameEvent.CORRECT_ANSW, OnCorrect);
        Messenger<float>.RemoveListener(GameEvent.MISTAKE, OnMistake);
        
    }
    private void OnMistake(float value)
    {
        healthbar.OnHealthChanged(value);
    }

    private void OnCorrect(float value)
    {
        enemyHealthbar.OnHealthChanged(value);
    }

   
}
