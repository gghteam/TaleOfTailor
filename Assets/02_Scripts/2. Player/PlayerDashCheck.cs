using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashCheck : MonoBehaviour
{
    private Vector3 minusvec = Vector3.zero;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player")
            minusvec = Mathf.Abs(other.transform.position.x) > Mathf.Abs(other.transform.position.z) ? new Vector3(1, 0, 0) : new Vector3(0, 0, 1);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player")
        {
            other.transform.position -= minusvec;
            Debug.Log("?");
        }
    }
}
