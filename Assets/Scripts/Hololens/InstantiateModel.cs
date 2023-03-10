using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class InstantiateModel : MonoBehaviour
{

    public void InstantiateObject() {

        foreach (var component in GameObject.FindObjectsOfType<RotateContentCard>())
        {
            if (component.enabled)
            {
                NetworkManager.Singleton.GetComponent<StartLesson>().CreateClass(component.model2Spawn);
                this.gameObject.SetActive(false);
            }
        }    
    }

}
