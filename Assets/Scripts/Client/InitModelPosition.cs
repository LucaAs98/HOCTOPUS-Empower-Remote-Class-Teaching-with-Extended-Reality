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

        Vector3 relativePos = cameraClient.transform.position - model.transform.position;
        relativePos.y = 0;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        model.transform.rotation = rotation;
    }
}