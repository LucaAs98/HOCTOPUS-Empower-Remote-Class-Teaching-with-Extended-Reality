using Unity.Netcode;
using UnityEngine;


public class InitModelPosition : NetworkBehaviour
{
    private GameObject model;
    [SerializeField] private Camera cameraClient;

    public void RepositionModel()
    {
        model = GameObject.FindGameObjectWithTag("SpawnedModel");
        model.transform.position = cameraClient.transform.position + cameraClient.transform.forward * 2;
        model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y - 0.2f,
            model.transform.position.z);
        model.transform.LookAt(cameraClient.transform);
    }
}