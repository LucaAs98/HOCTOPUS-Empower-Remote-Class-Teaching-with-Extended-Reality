using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Utilities;
using TMPro;
using UnityEngine;

public class ManageNotification : MonoBehaviour
{
    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private GameObject notificationGroup;


    public void AddNotification(string studentName)
    {
        notificationPrefab.GetComponentInChildren<TextMeshPro>().text = studentName + " vuole fare una domanda!";
        GameObject objSpawned = Instantiate(notificationPrefab, notificationGroup.transform);
        notificationGroup.GetComponent<GridObjectCollection>().UpdateCollection();
        Destroy(objSpawned.gameObject, 5f);      
    }
}