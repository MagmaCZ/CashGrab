using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour
{
#if UNITY_IOS
string gameId = "5303102";
#else
    string gameId = "5303103";
#endif
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.Initialize("5303103", true, (IUnityAdsInitializationListener)this);
    }

    public void PlayAd()
    {
            Advertisement.Show("video", (IUnityAdsShowListener)this);
        
    }
    void InuityAdsInitializationListener()
    {

    }
}
