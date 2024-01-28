using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTablet : MonoBehaviour
{
    [SerializeField] private GameObject endText;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            endText.SetActive(true);
        }
    }
}
