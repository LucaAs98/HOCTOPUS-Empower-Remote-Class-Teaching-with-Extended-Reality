using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorDisableBtn : MonoBehaviour
{
    [SerializeField] private Material disableMaterial;
    private Material startMaterial;


    void Start()
    {
        startMaterial = this.GetComponent<MeshRenderer>().materials[0];
    }

    public void ChangeColor(bool activating)
    {
        if (!activating)
        {
            this.GetComponent<MeshRenderer>().materials[0] = disableMaterial;
        }
        else
        {
            this.GetComponent<MeshRenderer>().materials[0] = startMaterial;
        }
    }
}