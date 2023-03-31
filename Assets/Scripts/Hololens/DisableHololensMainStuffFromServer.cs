using Unity.Netcode;
using UnityEngine;

public class DisableHololensMainStuffFromServer : NetworkBehaviour
{
    [SerializeField] private GameObject handMenuToggles;
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            handMenuToggles.SetActive(false);
        }
    }
}
