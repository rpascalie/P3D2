using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door2 : MonoBehaviour
{
    [SerializeField] private GameObject button2d;
    private bool button2dTriggered;
    [SerializeField] private GameObject door;

    private void Update()
    {
        button2dTriggered = button2d.GetComponent<ButtonTrigger2d>().buttonTriggered2d;

        if (button2dTriggered)
        {
            door.gameObject.SetActive(false);
        }
    }
}

