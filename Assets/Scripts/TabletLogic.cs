using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletLogic : MonoBehaviour
{
    [SerializeField] GameObject room1;
    [SerializeField] GameObject room2;    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ChangeTabletRoom();                     
        }           
    }   
    private void ChangeTabletRoom()
    {   
        if (room1.activeInHierarchy == true)
        {
            room1.SetActive(false);
            room2.SetActive(true);
        } 
        else
        {
            room1.SetActive(true);
            room2.SetActive(false);
        }     
    }
}
