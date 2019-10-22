using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{
    // Start is called before the first frame update
    public string andorid;
    public string Ios;

    public bool testMode;

    public bool playAd = false;
    
    // Start is called before the first frame update
    void Start()
    {
        string gameId = null;

#if UNITY_ANDROID
        gameId = andorid;
#elif UNITY_IOS
         gameId = Ios;
#endif

        if (Advertisement.isSupported)
        {
          
            Advertisement.Initialize(gameId, testMode);
            // Advertisement.debugMode(true);
        }
        Debug.Log("Unity Ads initialized: " + Advertisement.isInitialized);
        Debug.Log("Unity Ads is supported: " + Advertisement.isSupported);
        Debug.Log("Unity Ads test mode enabled: " + Advertisement.debugMode);
    }

    // Update is called once per frame
    void Update()
    {

        if (playAd)
        {
            // Debug.Log("W");
            if (Advertisement.IsReady("video"))
            {
                Debug.Log("ready");
                Advertisement.Show("video");
                playAd = false;
            }
        }
    }
    public void jan()
    {
        ShowAd();
    }
    public void ShowAd()
    {
        const string PlacementId = "video";


        Debug.Log("here");

        if (Advertisement.IsReady(PlacementId))
        {

            Debug.Log("READT");
            Advertisement.Show(PlacementId, new ShowOptions() { resultCallback = adViewResult });
        }
        else
        {
            Debug.Log("failed");
        }
        Debug.Log("eND");
    }
    public void adViewResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log(" Player viewed complete Ad");
                break;
            case ShowResult.Skipped:
                Debug.Log(" Player Skipped Ad ");
                break;
            case ShowResult.Failed:
                Debug.Log("Problem showing Ad ");
                break;
        }
    }
}
