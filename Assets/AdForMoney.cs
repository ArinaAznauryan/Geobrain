using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.Events;
using TMPro;

public class AdForMoney : MonoBehaviour
{
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-7333197465910069/3415242967";
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-7333197465910069/3415242967";
#else
    private string _adUnitId = "ca-app-pub-7333197465910069/3415242967";
#endif

    private RewardedAd _rewardedAd;
    private bool doAfterAddClosedInvoked = false;
    private bool _isConnected;
    private Coroutine fadeCoroutine;
    public TextMeshProUGUI noConnectionText;
    public TextMeshProUGUI tryAgainText;
    private bool loadAd = true;
    public bool RunAD = false; //once = false;
    //public UnityEvent DoAfterAddClosed;

    public void PlayAd()
    {
        CheckInternetConnection(); 

        if (_isConnected)
        {
            RunAD = true;
            FindObjectOfType<ClickSoundControl>().startMusic(0);
            ShowRewardedAd();
        }
        else
        {
            Debug.Log("Cannot play ad: no internet connection");
            if (noConnectionText != null)
            {
                noConnectionText.gameObject.SetActive(true); 
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(FadeText());
            }
        }
        FindObjectOfType<PauseButton>().BuyMoneyMenuPanel.SetActive(false);
        FindObjectOfType<PauseButton>().stopButton.SetActive(true);
    }

    public IEnumerator FadeText()
    {
        yield return new WaitForSeconds(1f); 
        float fadeDuration = 1f; 
        float timer = 0f;

        Color startColor = noConnectionText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0); 

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            noConnectionText.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            tryAgainText.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;
        }
        noConnectionText.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        tryAgainText.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        noConnectionText.gameObject.SetActive(false); 
        tryAgainText.gameObject.SetActive(false); 
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // If error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    // Debug log "no connection" for failed ad load
                    return;
                }

                _rewardedAd = ad;
            });
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg = "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                if (reward.Type == "Coin" && reward.Amount == 10)
                {
                    FindObjectOfType<MoneyManager>().AddMoney(); // Grant reward
                    //DoAfterAddClosed.Invoke();
                    doAfterAddClosedInvoked = true;
                }
                else
                {
                    Debug.Log("Rewarded ad did not grant the expected reward.");
                }

                loadAd = true;
                RunAD = true;
            });
        }
        else
        {
            if (tryAgainText != null)
            {
                tryAgainText.gameObject.SetActive(true); 
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(FadeText());
            }
            loadAd = true;
            RunAD = true;
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
        };

        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
        };

        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
        };

        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
        };

        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
        };

        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
        };
    }

    void CheckInternetConnection()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            _isConnected = true;
            Debug.Log("Internet connection available");
        }
        else
        {
            _isConnected = false;
            Debug.Log("No internet connection available");
        }
    }

    void Update()
    {
        if (loadAd)
        {
            LoadRewardedAd();
            loadAd = false;
        }
        if (_rewardedAd != null) RegisterEventHandlers(_rewardedAd);
    }

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
    }
}
