using Unity.Netcode;
using UnityEngine;

public class RotateModelForClient : NetworkBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        if (IsClient) return;
        mainCamera = Camera.main;
        InvokeRepeating("RepeatedCall", 0, 2.0f);
    }

    [ClientRpc]
    void RotateForClientRpc(float x, float y, float z, float w, string nomeModello)
    {
        //We need to take the model and the camera here, because the clientRpc cannot see other variables
        mainCamera = Camera.main;

        Quaternion rotation = new Quaternion(x, y, z, w);

        this.transform.rotation = mainCamera.transform.rotation * rotation;

        Debug.Log("Chiamata! -------- " + rotation + " ------- " + nomeModello);
    }

    void RepeatedCall()
    {
        Quaternion relativeRotation;
        relativeRotation = Quaternion.Inverse(mainCamera.transform.rotation) * this.transform.rotation;
        RotateForClientRpc(relativeRotation.x, relativeRotation.y, relativeRotation.z, relativeRotation.w,
            this.name);
    }
}