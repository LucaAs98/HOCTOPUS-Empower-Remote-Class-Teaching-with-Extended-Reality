using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class InitModelPosition : NetworkBehaviour
{
    private GameObject model;
    [SerializeField] private Camera cameraClient;

    private void Start()
    {
        model = GameObject.FindGameObjectWithTag("SpawnedModel");
        model.transform.position = cameraClient.transform.position + cameraClient.transform.forward * 2;         
    }

}
