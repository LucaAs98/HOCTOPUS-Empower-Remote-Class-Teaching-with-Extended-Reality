using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneForDevice : MonoBehaviour
{
    //[SerializeField] private GameObject camera;
    [SerializeField] private GameObject arSession;
    [SerializeField] private GameObject startMenuHol;
    [SerializeField] private GameObject canvasAnd;
    [SerializeField] private GameObject hololensStuffs;
    [SerializeField] private GameObject hololensToolkit;
  

    // Start is called before the first frame update
    void Start()
    {
        bool isStudent = Application.platform == RuntimePlatform.Android;

        if (!isStudent)
        {
            SpawnStartMenu(startMenuHol, true);
            hololensStuffs.gameObject.SetActive(!isStudent);
            hololensToolkit.gameObject.SetActive(!isStudent);
        }
        else {
            SpawnStartMenu(canvasAnd, false);
            arSession.gameObject.SetActive(isStudent);
        }
    }

    private void SpawnStartMenu(GameObject menuToSpawn, bool flag) {
        Transform parentTransform;

        if (flag) {
            parentTransform = FindParentTransform("UIHololens");
        }
        else {
            parentTransform = FindParentTransform("Canvas");
        }
        GameObject menu = Instantiate(menuToSpawn, parentTransform);

        if (flag) {
            Transform tranCam = Camera.main.transform;
            menu.transform.position = tranCam.position + tranCam.forward;
            menu.transform.LookAt(tranCam);
            menu.transform.RotateAround(menu.transform.position, menu.transform.up, 180f);
        }
    }

    private Transform FindParentTransform(string name) {
        return GameObject.Find(name).transform;
    }
}
