using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;

public class SDKManager : MonoBehaviour
{

    public static SDKManager Instance;
    public ThirdwebSDK SDK;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SDK = new ThirdwebSDK("https://goerli.prylabs.net");
        // Contract myContract = SDK.GetContract("0x87211C1FD0540758d3514316b477868f37d12A44");
        // Contract TokenContract = SDK.GetContract("0x5d0De718eB55D3E15f87843bc8e4D1e15FAeC701");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
