using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckActiveParts : MonoBehaviour
{
    //private GameObject body;
    private GameObject squeleton;
    private List<GameObject> activePartsList = new();
    [SerializeField] private GameObject containerSelectPartsPanel;
    [SerializeField] private GameObject rootPartPrefab;
    [SerializeField] private GameObject buttonSpecificPartPrefab;

    private bool permissions = false;

    void Start()
    {
        //body = GameObject.FindGameObjectWithTag("SpawnedModel");
        squeleton = GameObject.Find("Squeleton");
    }

    public void UpdateSelectPanelParts()
    {
        if (permissions)
        {
            activePartsList = GetActiveParts();
            DestroyAllChildren();
            AddNewChildren();
        }
    }

    private List<GameObject> GetActiveParts()
    {
        List<GameObject> activeParts = new();

        foreach (Transform child in squeleton.transform)
        {
            if (child.gameObject.activeSelf)
                activeParts.Add(child.gameObject);
        }

        return activeParts;
    }

    private void DestroyAllChildren()
    {
        foreach (Transform button in containerSelectPartsPanel.transform)
        {
            Destroy(button.gameObject);
        }
    }

    private void AddNewChildren()
    {
        foreach (GameObject rootPart in activePartsList)
        {
            rootPartPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rootPart.name;
            GameObject newRoot = Instantiate(rootPartPrefab, containerSelectPartsPanel.transform);
            AddButtonsToRoot(newRoot, rootPart);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(containerSelectPartsPanel.GetComponent<RectTransform>());
    }

    private void AddButtonsToRoot(GameObject newRoot, GameObject rootPart)
    {
        foreach (Transform specificPart in rootPart.transform)
        {
            buttonSpecificPartPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = specificPart.name;
            Instantiate(buttonSpecificPartPrefab.gameObject, newRoot.transform.GetChild(1));
        }
    }

    public void SetPermission(bool permission)
    {
        permissions = permission;

        if (permissions)
            UpdateSelectPanelParts();
    }
}