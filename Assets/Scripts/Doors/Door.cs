using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject button;
    private bool button3dTriggered;
    [SerializeField] private GameObject button2d;
    private bool button2dTriggered;
    [SerializeField] private GameObject door;

    private void Update()
    {
        button3dTriggered = button.GetComponent<ButtonTrigger>().buttonTriggered;
        button2dTriggered = button2d.GetComponent<ButtonTrigger2d>().buttonTriggered2d;

        if (button3dTriggered && button2dTriggered) 
        { 
            door.gameObject.SetActive(false);
        }
    }
}
