using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestConnection : MonoBehaviour
{
    [SerializeField] private Button serverBtn;
    [SerializeField] private Button clientBtn;

    // Start is called before the first frame update
    void Awake()
    {
        serverBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
        });

        clientBtn.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
