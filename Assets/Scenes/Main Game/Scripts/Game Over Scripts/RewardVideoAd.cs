using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;

public class RewardVideoAd : MonoBehaviour
{
    private RewardBasedVideoAd rewardBasedVideo;
    private Boolean didInteract;
    private Boolean isStopped;

    private void Start()
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

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
    }

    public void RequestRewardVideo()
    {
        //Shows Loading Panel
        GetComponent<GameOverUIHandler>().ShowAdPanel();

        //Updates the bool
        didInteract = false;
        isStopped = false;

        this.RequestRewardBasedVideo();
    }

    private void RequestRewardBasedVideo()
    {
        //Gets the correct Unit ID
#if UNITY_EDITOR
            string adUnitId = "editor_platform";
#elif UNITY_ANDROID
            //REAL UNIT ID: ca-app-pub-5709626172947104/4604633226
            string adUnitId = "ca-app-pub-5709626172947104/4604633226";
#elif UNITY_IPHONE
            //REAL UNIT ID: ca-app-pub-5709626172947104/7174956243
            string adUnitId = "ca-app-pub-5709626172947104/7174956243";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        //AdRequest request = new AdRequest.Builder().AddTestDevice("a452c97175ecae1bbdc010df18db29c8").Build();

        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
        if (!isStopped)
        {
            this.rewardBasedVideo.Show();
        }
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
        GetComponent<GameOverUIHandler>().HideAdPanel();
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
        GetComponent<GameOverUIHandler>().HideAdPanel();
        if (didInteract)
        {
            GetComponent<GameOverUIHandler>().ContinueMainGame();
        }
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        /*
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
        */
        didInteract = true;
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        //MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    public void StopAdFromLoading()
    {
        isStopped = true;
    }

}
