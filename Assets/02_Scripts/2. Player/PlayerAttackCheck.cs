using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCheck : MonoBehaviour
{
    [SerializeField]
    private PlayerAttack playerAttack;

    public bool isfirst = false;

    EventParam eventParam;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ENEMY") && playerAttack.IsAttacking && !isfirst)
        {
            Debug.Log("^^");
            isfirst = true;
            eventParam.intParam = (int)playerAttack.PlayerDamage;
            EventManager.TriggerEvent("ENEMYDAMAGE", eventParam);
        }
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.collider.CompareTag("ENEMY") && playerAttack.IsAttacking && !isfirst)
    //    {
    //        isfirst = true;
    //        eventParam.intParam = (int)playerAttack.PlayerDamage;
    //        EventManager.TriggerEvent("ENEMYDAMAGE", eventParam);
    //    }
    //}
}
