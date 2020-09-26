using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class TopBannerAd : MonoBehaviour
{
    private BannerView bannerView;
    private bool isLoaded;
    private bool wasHidden;

    void Start()
    {
        GameData gdata = SaveLoadSystem.LoadGameData();
        if (!gdata.hasNoAds)
        {
#if UNITY_ANDROID
            string appId = "ca-app-pub-5709626172947104~8965185682";
#elif UNITY_IPHONE
            string appId = "ca-app-pub-5709626172947104~1880902581";
#else
            string appId = "unexpected_platform";
#endif

            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize(appId);

            this.RequestBanner();
        }
    }

    private void RequestBanner()
    {
        isLoaded = false;

#if UNITY_EDITOR
            string adUnitId = "editor_platform";
#elif UNITY_ANDROID
            //REAL UNIT ID: ca-app-pub-5709626172947104/5053255593
            string adUnitId = "ca-app-pub-5709626172947104/5053255593";
#elif UNITY_IPHONE
            //REAL UNIT ID: ca-app-pub-5709626172947104/5516290029
            string adUnitId = "ca-app-pub-5709626172947104/5516290029";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);


        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        //AdRequest request = new AdRequest.Builder().AddTestDevice("a452c97175ecae1bbdc010df18db29c8").Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        isLoaded = true;
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdLoaded event received");
        if (!wasHidden)
        {
            ShowBanner();
        }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdLeavingApplication event received");
        HideBanner();
        DestroyBanner();
        RequestBanner();
    }

    public void DestroyBanner()
    {
        this.bannerView.Destroy();
        isLoaded = false;
    }

    public void HideBanner()
    {
        wasHidden = true;
        if (isLoaded)
        {
            this.bannerView.Hide();
        }
    }

    public void ShowBanner()
    {
        wasHidden = false;
        if (isLoaded)
        {
            this.bannerView.Show();
        }
    }

}
