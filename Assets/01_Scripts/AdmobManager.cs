using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmobManager : MonoBehaviour
{
    // ��� Id
    private string _bannerTestId = "ca-app-pub-3940256099942544/6300978111";
    private string _bannerId = "ca-app-pub-7239844525407980/6082277407";
    // ��� �� �ν��Ͻ�
    private BannerView _bannerView;

    // ���� ����
    private string _frontAdTestId = "ca-app-pub-3940256099942544/1033173712";
    private string _frontAdId = "ca-app-pub-7239844525407980/2346497788";
    private InterstitialAd _interstitialAd;

    // ������ ����
    private string _rewardTestId = "ca-app-pub-3940256099942544/5224354917";
    private string _rewardId = "";
    private RewardedAd _rewardedAd;

    private void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        // ���� �� �� ����� ���� SDK �ʱ�ȭ
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
            CreateBannerView();
            LoadBanner();
            LoadInterstitialAd();
        });
    }

    #region ���
    /// <summary>
    /// Creates a 320x50 banner at top of the screen
    /// </summary>
    public void CreateBannerView()
    {
        Debug.Log("Creating banner View");

        // �̹� ��ʺ䰡 ������ ���� ���� ����
        if(_bannerView != null)
        {
            DestroyBanner();
        }
        // ��ܿ� ��� ����
        AdSize adsize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
        _bannerView = new BannerView(_bannerId, adsize, AdPosition.Bottom);
    }
    public void LoadBanner()
    {
        if(_bannerView == null)
        {
            CreateBannerView();
        }

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("Unity-admob-sample");

        Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);

    }
    public void DestroyBanner()
    {
        if(_bannerView != null)
        {
            Debug.Log("Destroying banner ad.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
    #endregion

    #region ����
    // ���� ���� �ε�
    public void LoadInterstitialAd()
    {
        if(_interstitialAd != null)
        {
            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        InterstitialAd.Load(_frontAdId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                if(error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                        "with error : " + error);
                    return;
                }
                Debug.Log("Interstitial ad loaded with response : "
                    + ad.GetResponseInfo());

                RegisterFrontEventHandlers(ad);

                _interstitialAd = ad;
            });
    }

    // ���� ���� ǥ��
    public void ShowFrontAd()
    {

        if(_interstitialAd != null)
        {
            Debug.Log("Showing interstitial ad.");
            _interstitialAd.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            LoadInterstitialAd();
            _interstitialAd.Show();
        }
    }
    #endregion

    private void RegisterFrontEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            SoundManager.instance.StopBGM();
            Debug.Log("Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            SoundManager.instance.PlayBgm(true, Define.Bgm.LobbyBgm);
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
        };
    }

    #region ������ ����

    //// ������ ���� �ε�
    //public void LoadRewardedAd()
    //{
    //    if(_rewardedAd != null)
    //    {
    //        _rewardedAd.Destroy();
    //        _rewardedAd = null;
    //    }
    //
    //    Debug.Log("Loading the rewarded ad.");
    //
    //    var adRequest = new AdRequest();
    //    adRequest.Keywords.Add("unity-admob-sample");
    //
    //    RewardedAd.Load(_rewardTestId, adRequest,
    //        (RewardedAd ad, LoadAdError error) =>
    //        {
    //            if (error != null || ad == null)
    //            {
    //                Debug.LogError("Rewarded ad failed to load an ad " +
    //                               "with error : " + error);
    //                return;
    //            }
    //
    //            Debug.Log("Rewarded ad loaded with response : "
    //                      + ad.GetResponseInfo());
    //
    //            _rewardedAd = ad;
    //
    //        });
    //}
    //public void ShowRewardedAd()
    //{
    //    const string rewardMsg =
    //        "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";
    //
    //    if (_rewardedAd != null && _rewardedAd.CanShowAd())
    //    {
    //        _rewardedAd.Show((Reward reward) =>
    //        {
    //            // TODO: Reward the user.
    //            Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
    //        });
    //    }
    //}
    #endregion
}
