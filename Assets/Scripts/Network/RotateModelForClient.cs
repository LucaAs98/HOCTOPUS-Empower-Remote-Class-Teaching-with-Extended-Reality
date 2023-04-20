using Unity.Netcode;
using UnityEngine;

public class RotateModelForClient : NetworkBehaviour
{
    private GameObject objToRotate;

    private Camera mainCamera;

    public int number = 10;


    void Start()
    {
        if (IsClient) return;
        objToRotate = GameObject.FindGameObjectWithTag("SpawnedModel");
        mainCamera = Camera.main;
        InvokeRepeating("RepeatedCall", 0, 2.0f);
    }

    [ClientRpc]
    void RotateForClientRpc(int n, float x, float y, float z, float w, string nomeModello)
    {
        //We need to take the model and the camera here, because the clientRpc cannot see other variables
        objToRotate = GameObject.FindGameObjectWithTag("SpawnedModel");
        mainCamera = Camera.main;

        Quaternion rotation = new Quaternion(x, y, z, w);

        objToRotate.transform.rotation = mainCamera.transform.rotation * rotation;

        Debug.Log("Chiamata! -------- " + n + " -------- " + rotation + " ------- " + nomeModello);
    }

    void RepeatedCall()
    {
        Quaternion relativeRotation;
        relativeRotation = Quaternion.Inverse(mainCamera.transform.rotation) * objToRotate.transform.rotation;
        RotateForClientRpc(number, relativeRotation.x, relativeRotation.y, relativeRotation.z, relativeRotation.w,
            objToRotate.name);
    }
}