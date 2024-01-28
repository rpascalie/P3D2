using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door3 : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private bool buttonTriggered;
    [SerializeField] private GameObject door;

    private void Update()
    {
        buttonTriggered = button.GetComponent<ButtonTrigger>().buttonTriggered;

        if (buttonTriggered)
        {
            door.gameObject.SetActive(false);
        }
    }
}

