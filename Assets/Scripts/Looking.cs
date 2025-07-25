using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var playerPos = GameObject.Find("Player").transform.position;
        playerPos.y = transform.position.y;
        this.transform.LookAt(playerPos);
    }
}
