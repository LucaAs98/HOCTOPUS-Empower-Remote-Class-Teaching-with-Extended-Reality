using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.UI;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class Prova : NetworkBehaviour
{
    [SerializeField] private Material material;

    // [ServerRpc(RequireOwnership = false)]
    // public void ChangeValueServerRpc(Color baseColor, GameObject slider, Material material)
    // {
    //     ChangeValueClientRpc(baseColor, slider, material);
    // }
    //
    // [ClientRpc]
    // public void ChangeValueClientRpc(Color baseColor, GameObject slider, Material material)
    // {
    //     Color newColor = new Color(baseColor.r, baseColor.g, baseColor.b,
    //         slider.GetComponent<PinchSlider>().SliderValue);
    //     material.SetColor("_Color", newColor);
    // }
    //
    // void Update()
    // {
    //     /*Debug.Log("Materiale: " + material.color.a);
    //     Debug.Log("Slider: " + slider.GetComponent<PinchSlider>().SliderValue);*/
    // }


    [ClientRpc]
    public void TestStampaClientRpc(float alpha)
    {
        Color newColor = new Color(material.color.r, material.color.g, material.color.b, alpha);
        material.SetColor("_Color", newColor);
    }
}