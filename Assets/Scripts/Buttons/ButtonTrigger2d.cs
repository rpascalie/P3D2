using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger2d : MonoBehaviour
{
    public bool buttonTriggered2d;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player2d"))
        {
            buttonTriggered2d = true;
            Debug.Log("Button triggered");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player2d"))
        {
            buttonTriggered2d = false;
            Debug.Log("Button untriggered");
        }
    }
}
