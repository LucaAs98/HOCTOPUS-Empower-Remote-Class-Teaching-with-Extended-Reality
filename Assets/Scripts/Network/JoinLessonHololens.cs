using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class JoinLessonHololens : MonoBehaviour
{
    [SerializeField] private GameObject canvasHol;
    [SerializeField] private GameObject codeObj;
    [SerializeField] private GameObject errorImage;
    [SerializeField] private GameObject placeholder;
    [SerializeField] private GameObject name;
    [SerializeField] private GameObject studentPrefab;

    async public void Join()
    {
        GameObject playerPrefab = studentPrefab;
        playerPrefab.GetComponent<InitClient>().playerName = name.GetComponent<MRTKTMPInputField>().text;

        bool flag = await NetworkManager.Singleton.GetComponent<RelayLogic>()
            .JoinRelay(codeObj.GetComponent<MRTKTMPInputField>().text);

        if (flag)
        {
            canvasHol.gameObject.SetActive(false);
        }
        else
        {
            errorImage.SetActive(true);
            placeholder.GetComponent<TextMeshProUGUI>().text = "Codice errato";
            codeObj.GetComponent<MRTKTMPInputField>().text = "";
        }
    }
}