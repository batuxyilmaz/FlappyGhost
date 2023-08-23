using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System.Drawing;


public class AdManager : MonoBehaviour
{
    private InterstitialAd interstitial;
    private BannerView banner;
    public bool adReady;
    public static AdManager instance;
    public float adTimer;
    private RewardedAd reward;
    public bool rewardReady;
    private void Awake()
    {
        instance = this;
    }

    
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        RequestRewarded();
        RequestBanner();
    }

    // Update is called once per frame
  
    void Update()
    {

        if (adReady)
        {
            adTimer += Time.deltaTime;
            if (adTimer >= 1.5f)
            {
                if (interstitial.IsLoaded())
                {
                    interstitial.Show();
                    adReady = false;
                }
                adTimer = 0;
            }
          
        }
        if (rewardReady)
        {
            if (reward.IsLoaded())
            {
                reward.Show();
                rewardReady = false;
            }
          

        }
    }


    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string AdID = "ca-app-pub-5543829207976032/6602861045";
#else
      string reklamID="Unexpected platform";
#endif

        this.interstitial = new InterstitialAd(AdID);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string AdID = "ca-app-pub-5543829207976032/1525261356";
#else
      string reklamID="Unexpected platform";
#endif
       
        this.banner = new BannerView(AdID, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        this.banner.LoadAd(request);
    }

    
    private void RequestRewarded()
    {
#if UNITY_ANDROID
        string AdID = "ca-app-pub-5543829207976032/5660194906";
#else
      string reklamID="Unexpected platform";
#endif

        reward = new RewardedAd(AdID);
        AdRequest request = new AdRequest.Builder().Build();
        reward.LoadAd(request);
        reward.OnUserEarnedReward += rewardPlayer;
    }
    private void rewardPlayer(object sender, Reward e)
    {
      
        GameManager.instance.point += GameManager.instance.point;
        GameManager.instance.coinText.text = GameManager.instance.point.ToString();
        PlayerPrefs.SetInt("Point", GameManager.instance.point);
    }

}

