using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Unity.Netcode;
using UnityEngine;

public class InstantiateModel : MonoBehaviour
{
    [SerializeField] Transform loadingBalls;

    public void InstantiateObject()
    {
        //We check which card is selected in the horizontal menu, then we can create the lesson with the specific model 
        foreach (var component in GameObject.FindObjectsOfType<RotateContentCard>())
        {
            if (component.enabled)
            {
                Camera cam = Camera.main;
                Vector3 pos = cam.transform.position + cam.transform.forward;

                this.gameObject.SetActive(false);

                Transform newLoadingObj = Instantiate(loadingBalls, pos, Quaternion.identity);
                newLoadingObj.GetComponent<ProgressIndicatorOrbsRotator>().OpenAsync();

                NetworkManager.Singleton.GetComponent<StartLesson>().CreateClass(component.modelToSpawn, newLoadingObj);
            }
        }
    }
}