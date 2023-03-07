using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class JoinLesson : MonoBehaviour
{
    [SerializeField] private Canvas canvasAnd;
    [SerializeField] private TMP_InputField inputField;
   
    // Start is called before the first frame update


    async public void Join() {

        bool flag = await NetworkManager.Singleton.GetComponent<RelayLogic>().JoinRelay(inputField.text);

        if (flag)
            canvasAnd.gameObject.SetActive(false);
                
    }
    
}
