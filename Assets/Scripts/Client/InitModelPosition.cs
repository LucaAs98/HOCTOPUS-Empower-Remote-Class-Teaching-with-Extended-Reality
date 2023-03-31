using Unity.Netcode;
using UnityEngine;


public class InitModelPosition : NetworkBehaviour
{
    private GameObject model;
    private Camera camera;


    void Start()
    {
        camera = Camera.main;
    }

    public void RepositionModel()
    {
        model = GameObject.FindGameObjectWithTag("SpawnedModel");
        model.transform.position = camera.transform.position + camera.transform.forward * 2;
        model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y - 0.2f,
            model.transform.position.z);

        Vector3 relativePos = camera.transform.position - model.transform.position;
        relativePos.y = 0;
        
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        model.transform.rotation = rotation;
    }
}