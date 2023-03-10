using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartLesson : MonoBehaviour
{
    public Transform mixedRealitySceneContent;
    [SerializeField] private TextMeshProUGUI lobbyCodeText;
    [SerializeField] private GameObject sceneProps;
    [SerializeField] private Camera hololensCamera;
    float distance = 2.0f;
    // Start is called before the first frame update
    public async void CreateClass(Transform model3d)
    {
        sceneProps.gameObject.SetActive(true);
        string joinCode = await NetworkManager.Singleton.GetComponent<RelayLogic>().CreateRelay();

        if (joinCode != null) { 
            Vector3 pos = hololensCamera.transform.position + hololensCamera.transform.forward * distance;
            Quaternion rot = model3d.transform.rotation;
            Transform spawnedModel = Instantiate(model3d, pos, rot, mixedRealitySceneContent);
            spawnedModel.GetComponent<NetworkObject>().Spawn(true);
            
            lobbyCodeText.text = "Codice: " + joinCode;
            
        }
        else
        {
            lobbyCodeText.text = "Errore nella creazione del relay";
        }

    }
}
