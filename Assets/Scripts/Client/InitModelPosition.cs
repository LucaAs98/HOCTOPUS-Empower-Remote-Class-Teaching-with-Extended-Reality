using System.Collections;
using System.Collections.Generic;
using System.Globalization;
<<<<<<< Updated upstream
using Unity.Netcode;
using UnityEngine;
=======
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
>>>>>>> Stashed changes

public class InitModelPosition : NetworkBehaviour
{
    private GameObject model;
    [SerializeField] private Camera cameraClient;

<<<<<<< Updated upstream
    private void Start()
    {
        model = GameObject.FindGameObjectWithTag("SpawnedModel");
        model.transform.position = cameraClient.transform.position + cameraClient.transform.forward * 2;         
    }

}
=======
    public void RepositionModel()
    {
        model = GameObject.FindGameObjectWithTag("SpawnedModel");
        model.transform.position = cameraClient.transform.position + cameraClient.transform.forward * 2;
        model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y - 0.2f, model.transform.position.z);
        model.transform.LookAt(cameraClient.transform);
    }
}
>>>>>>> Stashed changes
