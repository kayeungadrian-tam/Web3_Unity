using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

using Thirdweb;

public class StartManager : MonoBehaviour
{
    [SerializeField] Transform parentTransform;

    private List<string> assetUrls = new List<string>();
    private string assetBundleUrl;
    private string assetName;
    
    // Start is called before the first frame update
    void Start()
    {
        LoadNft();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public async void ConnectWallet()
    {
        Debug.Log("Connecting ...");
        string address = await SDKManager.Instance.SDK.wallet.Connect(new WalletConnection()
        {
            provider = WalletProvider.MetaMask,
            chainId = 5 // Switch the wallet Goerli on connection
        });

        CheckNFT();
    }   

    public async Task CheckNFT()
    {
        Contract contract = SDKManager.Instance.SDK.GetContract("0x952d7677bF196C26c245324620B26FD91fED87Ad");
        var tokenId = "0"; // Id of the NFT to check
        string WallentAddress = await SDKManager.Instance.SDK.wallet.GetAddress();
        var balance = await contract.ERC1155.BalanceOf(WallentAddress, tokenId);
        float balanceFloat = float.Parse(balance);
    }

    public async void GetOwnedNft()
    {
        Debug.Log("Getting owned NFTs ...");
        await LoadNft();
        
        float x = 1;

        foreach(string url in assetUrls){
            if (url == "https://gateway.ipfscdn.io/ipfs/QmSdLHpHDReQc9s5D82w6vaeGTiBXqYVkKHDQET5WvE1iE")
            {
                assetName = "LOVEDUCK";
            } else {
                assetName = "SHEEP";
            }
            StartCoroutine(SpawnNft(url, assetName, x));
            x += 10;
        }
        // Debug.Log(assetUrls);

    }

    async Task<List<string>> LoadNft()
    {

        Contract contract =
            SDKManager.Instance.SDK.GetContract("0x2E9d43891276EE33a22358526C88D426290BCFA0");
        
        var address = "0xb6491Ab6a5486a3b2f4cb5e0518430683d967333";
        var nfts = await contract.ERC721.GetOwned(address);
        Debug.Log(nfts);
        
        foreach (NFT nft in nfts)
        {
            Debug.Log(nft);
            assetUrls.Add(nft.metadata.image.ToString());
        }

        return assetUrls;
    }

    IEnumerator SpawnNft(string url, string assetName, float x_loc)
    {


            Debug.Log($"Processing [{url}]. AssetName [{assetName}]");

            UnityWebRequest www =
                UnityWebRequestAssetBundle.GetAssetBundle(url);
            yield return www.SendWebRequest();
            
            // Something failed with the request.
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Network error");
                Debug.Log(www.error);
            }
            
            // Successfully downloaded the asset bundle, instantiate the prefab now.
            else
            {
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
                GameObject prefab = bundle.LoadAsset<GameObject>(assetName);
                GameObject instance =
                    Instantiate(prefab, new Vector3(x_loc, 0, 0), Quaternion.identity);
                    
                // (Optional) - Configure the shader of your NFT as it renders.
                Material material = instance.GetComponent<Renderer>().material;
                material.shader = Shader.Find("Standard");
            }
        
    }
}

