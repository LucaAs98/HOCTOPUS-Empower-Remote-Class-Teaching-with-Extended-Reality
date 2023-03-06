using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneForDevice : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject arSession;

    // Start is called before the first frame update
    void Start()
    {
        bool isStudent = false;

        if (Application.platform == RuntimePlatform.Android)
        {
            isStudent = true;
        }

        camera.gameObject.SetActive(!isStudent);
        arSession.gameObject.SetActive(isStudent);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
