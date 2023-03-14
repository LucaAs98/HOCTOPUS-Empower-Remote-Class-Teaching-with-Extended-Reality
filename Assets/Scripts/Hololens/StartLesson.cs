using TMPro;
using Unity.Netcode;
using UnityEngine;

public class StartLesson : MonoBehaviour
{
    [SerializeField] public Transform mixedRealitySC; //Parent where we spawn the objects to manipulate and share
    [SerializeField] public GameObject hololensCanvas; //Canvas where we display the lobby code
    [SerializeField] private TextMeshProUGUI lobbyCodeText; //LobbyCode we get from Relay
    [SerializeField] private Camera hololensCamera;
    float distance = 2.0f; //Distance between the holo camera and the model

    // Function where we spawn the object that corresponds to the selected card 
    public async void CreateClass(Transform modelToSpawn)
    {
        //We activate the canvas where we display the lobby code
        hololensCanvas.gameObject.SetActive(true);

        //Getting the lobbycode
        string joinCode = await NetworkManager.Singleton.GetComponent<RelayLogic>().CreateRelay();

        //If the connection is ok we spawn the interested model
        if (joinCode != null)
        {
            SpawnObject(modelToSpawn);
            lobbyCodeText.text = "Codice: " + joinCode;
        }
        else
        {
            lobbyCodeText.text = "Errore nella creazione del relay";
        }
    }


    //Spawns the object in front of the hololens camera position
    public void SpawnObject(Transform model)
    {
        Vector3 pos = hololensCamera.transform.position + hololensCamera.transform.forward * distance;
        Quaternion rot = model.transform.rotation;

        //We spawn the obj in the scene, but we need to spawn it also for the network
        Transform spawnedModel = Instantiate(model, pos, rot, mixedRealitySC);
        spawnedModel.GetComponent<NetworkObject>().Spawn(true);
    }
}