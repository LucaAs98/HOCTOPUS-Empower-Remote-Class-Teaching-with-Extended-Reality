using Unity.Netcode;
using UnityEngine;

public class RotateModelForClient : NetworkBehaviour
{
    private Camera mainCamera;
    private Quaternion startingRotation;
    Quaternion relRot;

    void Start()
    {
        if (IsClient) return;

        startingRotation = this.transform.rotation;
        mainCamera = Camera.main;
        InvokeRepeating("RepeatedCall", 0, 2.0f);
    }

    [ClientRpc]
    void RotateForClientRpc(float x, float y, float z, float w, string nomeModello)
    {
        //We need to take the model and the camera here, because the clientRpc cannot see other variables
        mainCamera = Camera.main;

        Quaternion relativeRotation = new Quaternion(x, y, z, w);

        this.transform.rotation = this.transform.rotation * relativeRotation;
    }

    void RepeatedCall()
    {
        Quaternion finalRotation = this.transform.rotation;

        if (finalRotation != startingRotation)
        {
            relRot = Quaternion.Inverse(startingRotation) * finalRotation;
            startingRotation = finalRotation;
            RotateForClientRpc(relRot.x, relRot.y, relRot.z, relRot.w, this.name);
        }
    }

    public void ResetStartingRotation()
    {
        startingRotation = this.transform.rotation;
        ClientResetRotClientRpc();
    }

    [ClientRpc]
    void ClientResetRotClientRpc()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            GameObject student = NetworkManager.Singleton.LocalClient.PlayerObject.gameObject;
            if (student != null)
                student.GetComponent<InitModelPosition>().ResetRotationClient();
        }
        else
        {
            this.gameObject.GetComponent<ResetModelPosition>().RepositionModel(true);
        }
    }
}