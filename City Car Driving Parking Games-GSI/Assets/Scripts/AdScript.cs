using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using UnityEngine.UI;
using UnityEngine.Advertisements;
//using Firebase.Analytics;
public class AdScript : MonoBehaviour
{
	private BannerView bannerView;
	private InterstitialAd interstitalAd;
	public static bool isLevelSkip = false;
	public static bool isRewardedVideoShow = false;
	private RewardedAd rewardedAds;
	[SerializeField] private string app_ID;
	[SerializeField] private string BannerAD_ID;
	[SerializeField] private string Interstitial_ID;
	[SerializeField] private string RewardedAds_ID;
	[SerializeField] private string RewardedInterstitalAds_ID;
	int userscore;

	public static AdScript adScript;
	// For testing
	public static bool isAd = false;

	public static bool isBannerAdsShow = false;
	public bool TestAD;
	// Use this for initialization
	void Awake()
	{
		if (!adScript)
		{

			adScript = this;

		}

		if (TestAD)
		{
			BannerAD_ID = "ca-app-pub-3940256099942544/6300978111";
			Interstitial_ID = "ca-app-pub-3940256099942544/1033173712";
			RewardedAds_ID = "ca-app-pub-3940256099942544/5224354917";
			RewardedInterstitalAds_ID = "ca-app-pub-3940256099942544/5354046379";
		}

		PlayerPrefs.SetInt("cars", 2);

	}

	private void Start()
	{

		LoadRewardedVideo();
		LoadInterstitialAds();
		//Invoke("FirstBanner",0.5f);

	}

	void FirstBanner()
	{
		//LoadBannerAD(AdSize.MediumRectangle, AdPosition.TopRight);
	}
	public void LoadBannerAD(AdSize adSize, AdPosition pos)
	{
		//	print("Load Ads Banner  -------------------------------------------------------");

		if (!PlayerPrefs.HasKey("RemoveAds"))
		{
			if (!isBannerAdsShow)
			{


				Request_Banner(adSize, pos);
				isBannerAdsShow = true;
			}
            else
            {
				RemoveBanner();
            }
		}
	}
	void Request_Banner(AdSize adSize, AdPosition pos)
	{


		this.bannerView = new BannerView(BannerAD_ID, adSize, pos);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.bannerView.LoadAd(request);

	}
	void LoadRewardedVideo()
	{

		this.rewardedAds = new RewardedAd(RewardedAds_ID);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the rewarded ad with the request.
		this.rewardedAds.LoadAd(request);


		// Called when an ad request has successfully loaded.
		this.rewardedAds.OnAdLoaded += HandleRewardedAdLoaded;
		// Called when an ad request failed to load.
		// Called when an ad is shown.
		this.rewardedAds.OnAdOpening += HandleRewardedAdOpening;
		// Called when an ad request failed to show.
		// Called when the ad is closed.
		this.rewardedAds.OnAdClosed += HandleRewardedAdClosed;

	}
	public void HandleRewardedAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdLoaded event received");
	}

	public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToLoad event received with message: "
							 + args.Message);
	}

	public void HandleRewardedAdOpening(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdOpening event received");
	}

	public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
	{
		MonoBehaviour.print(
			"HandleRewardedAdFailedToShow event received with message: "
							 + args.Message);
	}

	public void HandleRewardedAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardedAdClosed event received");
	}

	public void HandleUserEarnedReward(object sender, Reward args)
	{
		if (!isRewardedVideoShow)
		{
			isRewardedVideoShow = true;
			//if (GameManager.isLevelSkip)
			//{
			//	GameManager.gameManager.OnSkipLevel();
			//}
			//else if (!GameManager.isLevelSkip)
			//{
			//	userscore = PlayerPrefs.GetInt("score");
			//	userscore += 600;
			//}
			int CurrentCoins;
			CurrentCoins = PlayerPrefs.GetInt("CurrentMoney");
			CurrentCoins += 1000;
			PlayerPrefs.SetInt("CurrentMoney", CurrentCoins);
			UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
		}


	}

	public void UserChoseToWatchAd()
	{

		if (this.rewardedAds.IsLoaded())
		{
			this.rewardedAds.Show();

		}
	}
	// Request For InterstitialAds(){}
	public void LoadInterstitialAds()
	{

		this.interstitalAd = new InterstitialAd(Interstitial_ID);

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitalAd.LoadAd(request);
	}

	public void RemoveBanner()
	{
		if (isBannerAdsShow)
		{
			bannerView.Destroy();
			isBannerAdsShow = false;
		}
	}



	private void userEarnedRewardCallback(Reward reward)
	{


		print("User Show the rewared");
	}
	public void ShowInterstitial()
	{
		if (!PlayerPrefs.HasKey("RemoveAds"))
		{
			if (interstitalAd.IsLoaded())
			{
				interstitalAd.Show();
				LoadInterstitialAds();
			}
		}
	
	}




}
