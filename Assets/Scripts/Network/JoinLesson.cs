using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class JoinLesson : MonoBehaviour
{
    [SerializeField] private Canvas canvasAnd;
    [SerializeField] private TMP_InputField code;
    [SerializeField] private TMP_InputField name;
   
    // Start is called before the first frame update


    async public void Join() {
        
        GameObject playerPrefab = NetworkManager.Singleton.NetworkConfig.PlayerPrefab;
        playerPrefab.GetComponent<InitClient>().playerName = name.text;
        
        bool flag = await NetworkManager.Singleton.GetComponent<RelayLogic>().JoinRelay(code.text);

        if (flag)
        {
            canvasAnd.gameObject.SetActive(false);
        }
            
        
    }
    
}
