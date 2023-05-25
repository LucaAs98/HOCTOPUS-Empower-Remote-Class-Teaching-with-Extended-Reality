using System;
using System.Collections;
using System.Collections.Generic;
using Mono.CSharp;
using TMPro;
using UnityEngine;

public class ActiveOutlineFromClient : MonoBehaviour
{
    private string partName;
    private Transform root;


    void Start()
    {
        root = this.transform.root;
    }

    public void OutlineFromClient()
    {
        partName = this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        root.GetComponent<ClientHandler>().ShowOutline(partName);
        root.GetComponent<ClientHandler>().ChoosePartClick();
    }
}