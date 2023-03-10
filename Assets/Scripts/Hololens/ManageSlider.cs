using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using UnityEngine;

public class ManageSlider : MonoBehaviour
{
    [SerializeField] private GameObject objToModify;
    [SerializeField] private GameObject slider;
    private Material materialGameObj;
    private Color baseColor;

    void Start()
    {
        materialGameObj = objToModify.GetComponentInChildren<Renderer>().material;
        baseColor = materialGameObj.color;
        ChangeValue();
    }



    public void ChangeValue()
    {
        Color newColor = new Color(baseColor.r, baseColor.g, baseColor.b, slider.GetComponent<PinchSlider>().SliderValue);
        materialGameObj.SetColor("_Color", newColor);
    }
}
