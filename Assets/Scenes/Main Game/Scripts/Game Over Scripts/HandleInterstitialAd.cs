using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class HandleInterstitialAd : MonoBehaviour
{
    private InterstitialAd interstitial;
    private bool wantedToShow;
    private bool failedToLoad;
    private bool shownBeforeMenuSwitch;

    public void RequestInterstitial()
    {
        //Resets the var
        wantedToShow = false;
        failedToLoad = false;
        shownBeforeMenuSwitch = false;

#if UNITY_EDITOR
            string adUnitId = "editor_platform";
#elif UNITY_ANDROID
            //REAL UNIT ID: ca-app-pub-5709626172947104/8859591519
            string adUnitId = "ca-app-pub-5709626172947104/8859591519";
#elif UNITY_IPHONE
            //REAL UNIT ID: ca-app-pub-5709626172947104/5628575901
            string adUnitId = "ca-app-pub-5709626172947104/5628575901";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);


        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;


        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        //AdRequest request = new AdRequest.Builder().AddTestDevice("a452c97175ecae1bbdc010df18db29c8").Build();

        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void ShowAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else if (failedToLoad)
        {
            GetComponent<GameOverUIHandler>().RetryMainGame();
        }
        else
        {
            wantedToShow = true;
        }
    }

    public void ShowBeforeMenu()
    {
        shownBeforeMenuSwitch = true;
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else if (failedToLoad)
        {
            GetComponent<GameOverUIHandler>().SwitchToMenu();
        }
        else
        {
            wantedToShow = true;
        }
    }

    public void DestroyAd()
    {
        this.interstitial.Destroy();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdLoaded event received");
        if (wantedToShow)
        {
            ShowAd();
        }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
        failedToLoad = true;
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdClosed event received");
        DestroyAd();
        if (!shownBeforeMenuSwitch)
        {
            GetComponent<GameOverUIHandler>().RetryMainGame();
        }
        else
        {
            GetComponent<GameOverUIHandler>().SwitchToMenu();
        }
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void StopAdFromLoading()
    {
        wantedToShow = false;
        if (this.interstitial != null)
        {
            if (!this.interstitial.IsLoaded())
            {
                DestroyAd();
            }
        }
    }

}
