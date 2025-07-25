using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMovement : MonoBehaviour
{

    void OnEnable()
    {
        Messenger<bool, GameObject>.AddListener(GameEvent.FREEZE, OnfreezeMovement);
    }
    void OnDisable()
    {
        Messenger<bool, GameObject>.RemoveListener(GameEvent.FREEZE, OnfreezeMovement);
    }

    void OnfreezeMovement(bool trigger, GameObject player)
    {
        if (trigger)
        {
            player.GetComponent<MouseLook>().enabled = false;
            player.GetComponent<FPSInput>().enabled = false;
            GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<MouseLook>().enabled = true;
            player.GetComponent<FPSInput>().enabled = true;
            GameObject.Find("Main Camera").GetComponent<MouseLook>().enabled = true;
        }
    }
}
