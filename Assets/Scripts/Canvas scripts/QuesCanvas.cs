using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuesCanvas : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    public void Open()
    {
        Enemy.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    public void Close()
    {
        Enemy.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
