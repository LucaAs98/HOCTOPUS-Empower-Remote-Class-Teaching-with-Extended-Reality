using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckActiveParts : MonoBehaviour
{
    //private GameObject body;
    //private GameObject squeleton;
    GameObject model;
    private Dictionary<string, List<GameObject>> activePartsList = new();
    [SerializeField] private GameObject scrollView;
    [SerializeField] private GameObject containerPartsPrefab;
    [SerializeField] private GameObject rootPartPrefab;
    [SerializeField] private GameObject buttonSpecificPartPrefab;

    private bool permissions = false;

    void Start()
    {
       model = GameObject.FindGameObjectWithTag("SpawnedModel");
        //squeleton = GameObject.Find("Squeleton");
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

    private Dictionary<string, List<GameObject>> GetActiveParts()
    {
        Dictionary<string, List<GameObject>> dictParts = new Dictionary<string, List<GameObject>>();

        foreach (Transform child in model.transform)
        {
            if (child.gameObject.activeSelf && child.tag == "Layer")
            {

                foreach (Transform nephew in child)
                {
                    if (nephew.gameObject.activeSelf && nephew.tag == "Container")
                    {

                        if (!dictParts.ContainsKey(child.name))
                            dictParts.Add(child.name, new List<GameObject>());

                        dictParts[child.name].Add(nephew.gameObject);
                    }

                }
            }
        }

        return dictParts;
    }

    private void DestroyAllChildren()
    {
        foreach (Transform container in scrollView.transform)
        {
            Destroy(container.gameObject);
        }
    }

    private void AddNewChildren()
    {

        foreach (string nameLayer in activePartsList.Keys) {

            GameObject container = Instantiate(containerPartsPrefab, scrollView.transform);
            container.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = nameLayer;

            Transform containerButtons = container.transform.GetChild(1).transform;

            foreach (GameObject c in activePartsList[nameLayer]) {
                GameObject newRoot = Instantiate(rootPartPrefab, containerButtons);
                newRoot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = c.name;
                AddButtonsToRoot(newRoot, c);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(container.GetComponent<RectTransform>());

        }
    }

    private void AddButtonsToRoot(GameObject newRoot, GameObject rootPart)
    {
        foreach (Transform specificPart in rootPart.transform)
        {
            GameObject button = Instantiate(buttonSpecificPartPrefab.gameObject, newRoot.transform.GetChild(1));
            button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = specificPart.name;
        }
    }

    public void SetPermission(bool permission)
    {
        permissions = permission;

        if (permissions)
            UpdateSelectPanelParts();
    }
}