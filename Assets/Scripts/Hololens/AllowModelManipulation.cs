using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class AllowModelManipulation : MonoBehaviour
{
    private GameObject model;

    private void Start()
    {
        model = GameObject.FindGameObjectWithTag("SpawnedModel");
    }

    //Function called from the click of the toggle "ManipulatePermission" in HandMenuInfo
    public void AllowManipulation()
    {
        //We take the boolean value of the toggle
        bool isToggle = this.GetComponent<Interactable>().IsToggled;

        //We enable/disable the components for the manipulation of the model
        model.GetComponent<NearInteractionGrabbable>().enabled = isToggle;
        model.GetComponent<ObjectManipulator>().enabled = isToggle;
        model.GetComponent<CursorContextObjectManipulator>().enabled = isToggle;
    }
}