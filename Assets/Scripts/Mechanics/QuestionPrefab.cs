using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPrefab : MonoBehaviour
{

    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    private bool isTriggered;
    public float maxDistance = 5.0f;
    [SerializeField] public float time = 1f;
    [SerializeField] private float mechType = 1f;
    // Start is called before the first frame update
    void Start()
    {
        isTriggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        int layerMask = 1 << 6;
        if (!isTriggered)
        {
            //transform.Translate(0, 0, speed * Time.deltaTime);
            //Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    //startTimer(index);
                    Messenger<float>.Broadcast(GameEvent.RAY_HIT, mechType);
                    Messenger<bool, GameObject>.Broadcast(GameEvent.FREEZE, true, GameObject.FindGameObjectWithTag("Player"));
                    isTriggered = true;
                    this.name = "Enemytriggered";
                    Messenger<float>.Broadcast(GameEvent.TIMER_START, DataHolder.difficultySettings[0]);
                    Debug.Log("ענטדדונ");
                }
            }
        }
    }
}
