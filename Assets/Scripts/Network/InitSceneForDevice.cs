using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneForDevice : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject arSession;
    [SerializeField] private GameObject canvasHol;
    [SerializeField] private GameObject canvasAnd;
    [SerializeField] private GameObject mainCanvasHol;
  

    // Start is called before the first frame update
    void Start()
    {
        bool isStudent = Application.platform == RuntimePlatform.Android;

        camera.gameObject.SetActive(!isStudent);
        arSession.gameObject.SetActive(isStudent);

        canvasHol.gameObject.SetActive(!isStudent);
        canvasAnd.gameObject.SetActive(isStudent);

        mainCanvasHol.gameObject.SetActive(!isStudent);  

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
