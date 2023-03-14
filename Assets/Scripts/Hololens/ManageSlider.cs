using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Unity.Netcode;
using UnityEngine;

public class ManageSlider : NetworkBehaviour
{
    //Similar to ManageToggle, but in this case we have already the interrested objects in the serialized fields
    [SerializeField] private Material material;
    [SerializeField] private GameObject slider;
    private Color baseColor;

    void Awake()
    {
        baseColor = material.color;
        ChangeColor();
    }

    //Called every time we change the slider value
    public void ChangeColor()
    {
        float alpha = slider.GetComponent<PinchSlider>().SliderValue;
        SetNewAlpha(alpha);
        ChangeColorClientRpc(alpha);
    }

    [ClientRpc]
    public void ChangeColorClientRpc(float alpha)
    {
        SetNewAlpha(alpha);
    }

    //Base function called from the server and also from the clients
    private void SetNewAlpha(float alpha)
    {
        Color newColor = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
        material.SetColor("_Color", newColor);
    }
}