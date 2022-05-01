using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float hp = 0;
    private void Start()
    {
        EventManager.StartListening("ENEMYDAMAGE", Damaged);
    }
    private void Damaged(EventParam eventParam)
    {
        Debug.Log("?");
        hp -= eventParam.intParam;
    }
}
