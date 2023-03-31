using QFSW.QC.Utilities;
using System.Collections.Generic;
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

    [SerializeField] private Transform handMenuInfo;
    private List<string> studentList = new List<string>();

    // Function where we spawn the object that corresponds to the selected card 
    public async void CreateClass(Transform modelToSpawn, Transform loadingBalls, Vector3 position,
        GameObject floorFinder)
    {
        //We activate the canvas where we display the lobby code
        hololensCanvas.gameObject.SetActive(true);

        //Getting the lobbycode
        string joinCode = await NetworkManager.Singleton.GetComponent<RelayLogic>().CreateRelay();

        //If the connection is ok we spawn the interested model
        if (joinCode != null)
        {
            Destroy(floorFinder);
            Destroy(loadingBalls.gameObject);
            SpawnObject(modelToSpawn, position);
            lobbyCodeText.text = "Codice: " + joinCode;
            Transform codeText = FindDeepChild(handMenuInfo, "Code");
            codeText.GetComponent<TextMeshPro>().text = "CODICE: " + joinCode;
            Instantiate(handMenuInfo);
        }
        else
        {
            lobbyCodeText.text = "Errore nella creazione del relay";
        }
    }


    //Spawns the object in front of the hololens camera position
    public void SpawnObject(Transform model, Vector3 pos)
    {
        Vector3 relativePos = Camera.main.transform.position - pos;
        relativePos.y = 0;
        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //We spawn the obj in the scene, but we need to spawn it also for the network
        Transform spawnedModel = Instantiate(model, pos + new Vector3(0, 0.988f, 0), rotation, mixedRealitySC);
        spawnedModel.GetComponent<NetworkObject>().Spawn(true);
    }


    public void AddUser(string studentName)
    {
        studentList.Add(studentName);

        GameObject studentListObj = GameObject.Find("StudentList(Clone)");
        if (studentListObj != null)
        {
            studentListObj.GetComponent<ManageStudentList>().UpdateStudentListSpecific(studentName);
        }
    }

    public void RemoveUser(string studentName)
    {
        Debug.Log("2. RemoveUser");
        Debug.Log("3. FullList: " + studentList);

        foreach (var x in studentList)
        {
            Debug.Log("Print della lista prima della remove");
            Debug.Log(x.ToString());
        }

        Debug.Log("studentName: " + studentName);
        bool removedOK = studentList.Remove(studentName);


        foreach (var x in studentList)
        {
            Debug.Log("Print della lista dopo la remove");
            Debug.Log(x.ToString());
        }
        Debug.Log("4. RemovedOK " + removedOK);

        GameObject studentListObj = GameObject.Find("StudentList(Clone)");
        if (studentListObj != null)
        {
            Debug.Log("5. Entrato nell'if ");
            studentListObj.GetComponent<ManageStudentList>().RemoveStudentSpecific(studentName);
        }
    }

    public List<string> GetStudentList()
    {
        return studentList;
    }

    private Transform FindDeepChild(Transform parent, string name)
    {
        Transform child = parent.Find(name);
        if (child != null)
            return child;

        foreach (Transform tr in parent)
        {
            child = FindDeepChild(tr, name);
            if (child != null)
                return child;
        }

        return null;
    }
}