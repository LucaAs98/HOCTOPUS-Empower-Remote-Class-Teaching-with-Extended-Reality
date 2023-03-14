using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InstantiateModel : MonoBehaviour
{
    public void InstantiateObject()
    {
        //We check which card is selected in the horizontal menu, then we can create the lesson with the specific model 
        foreach (var component in GameObject.FindObjectsOfType<RotateContentCard>())
        {
            if (component.enabled)
            {
                NetworkManager.Singleton.GetComponent<StartLesson>().CreateClass(component.modelToSpawn);
                this.gameObject.SetActive(false);
            }
        }
    }
}