using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Unity.Netcode;
using UnityEngine;

public class InstantiateModel : MonoBehaviour
{
    [SerializeField] Transform floorFinder;

    public void InstantiateObject()
    {
        //We check which card is selected in the horizontal menu, then we can create the lesson with the specific model 
        foreach (var component in GameObject.FindObjectsOfType<RotateContentCard>())
        {
            if (component.enabled)
            {
                this.gameObject.SetActive(false);
                Transform ff = Instantiate(floorFinder);
                ff.GetComponent<PlaceObject>().SetModelToSpawn(component.modelToSpawn);
            }
        }
    }
}