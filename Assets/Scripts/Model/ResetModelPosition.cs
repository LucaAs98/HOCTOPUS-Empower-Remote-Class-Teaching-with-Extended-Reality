using Unity.Netcode;
using UnityEngine;

public class ResetModelPosition : MonoBehaviour
{
    private Camera cameraScene; //Client's main camera
    private Vector3 startingModelScale; //Starting scale of the model, we save it for reset when needed

    void Start()
    {
        startingModelScale = this.transform.localScale;
    }

    //Reset position and scale of the model. We put it in front of the client's camera, looking at him. 
    public void RepositionModel(bool isTeacher)
    {
        cameraScene = Camera.main;
        this.transform.localScale = startingModelScale;
        this.transform.position = cameraScene.transform.position + cameraScene.transform.forward * 2;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.2f,
            this.transform.position.z);

        Vector3 relativePos = cameraScene.transform.position - this.transform.position;
        relativePos.y = 0;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        this.transform.rotation = rotation;

        if (isTeacher)
        {
            Debug.Log("Sono un professore!!!!");

            //Call clientRPC to reset it also in clients
        }
    }
}