using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.Events;
using TMPro;

public class AdForReset : MonoBehaviour
{
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-7333197465910069/3414546494";
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-7333197465910069/3414546494";
#else
    private string _adUnitId = "ca-app-pub-7333197465910069/3414546494";
#endif

    private RewardedAd _rewardedAd;
    public TextMeshProUGUI coin250;
    public TextMeshProUGUI coin50;
    public TextMeshProUGUI coin10;
    private bool _isConnected;
    public TextMeshProUGUI tryAgainText;

    private Coroutine fadeCoroutine;
    public TextMeshProUGUI noConnectionText;
    public GameObject[] stars;
    private bool loadAd = true;
    public QuizManager quizManager;
    private int adCount = 0;

    public void PlayAd()
    {
        CheckInternetConnection(); 

        if (_isConnected)
        {
            if (FindObjectOfType<PauseButton>().startreset)
            {
                FindObjectOfType<PauseButton>().startreset = true;
                FindObjectOfType<ClickSoundControl>().startMusic(0);
                ShowRewardedAd(); // Show the rewarded ad directly
            }
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

    IEnumerator FadeText()
    {
        yield return new WaitForSeconds(2f); 
        float fadeDuration = 1f; 
        float timer = 0f;

        Color startColor = noConnectionText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0); 

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            noConnectionText.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            coin10.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            coin50.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            coin250.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;
        }
        noConnectionText.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        coin10.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        coin50.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        coin250.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        tryAgainText.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

        noConnectionText.gameObject.SetActive(false); 
        coin10.gameObject.SetActive(false); 
        coin50.gameObject.SetActive(false); 
        coin250.gameObject.SetActive(false); 
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

        //Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : "
                          //+ ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }

    public void ResetAdCount()
    {
        PlayerPrefs.SetInt("adCount", 0);
        PlayerPrefs.Save();
    }

    public void ShowRewardedAd()
    {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                // TODO: Reward the user.
                adCount++;
                Debug.Log("fuckyoass"+adCount);
                PlayerPrefs.SetInt("adCount", adCount);
                PlayerPrefs.Save();

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
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log(string.Format("Rewarded ad paid {0} {1}.",
                //adValue.Value,
                //adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            //Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
        
                loadAd = true;
                //Debug.Log("User rewarded, biach");
                FindObjectOfType<PauseButton>().AfterAd();
                

            //Debug.Log("Rewarded ad full screen content closed.");
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Rewarded ad failed to open full screen content " +
                        //"with error : " + error);
        };
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
        if (stars.Length > 0)
        {
            LogStars();
        }
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
    }

    public void LogStars()
    {
        int count = PlayerPrefs.GetInt("adCount");
        if (count >= 4 && count <= 9)
        {
            Debug.Log("Debug log: You got 2 stars."+count);
            stars[0].GetComponent<Image>().color = new Color(1f, 1f, 1f);
            stars[1].GetComponent<Image>().color = new Color(1f, 1f, 1f);
            if (coin50 != null)
            {
                coin50.gameObject.SetActive(true); 
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(FadeText());
            }
            FindObjectOfType<MoneyManager>().AddMoneyMore();
        }
        else if (count >= 10 && count <= 15)
        {
            Debug.Log("Debug log: You got 1 star."+count);
            stars[0].GetComponent<Image>().color = new Color(1f, 1f, 1f);
            FindObjectOfType<MoneyManager>().AddMoney();
            if (coin10 != null)
            {
                coin10.gameObject.SetActive(true); 
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(FadeText());
            }
            
        }
        else if (count >= 0 && count <= 4)
        {
            Debug.Log("Debug log: You got 3 stars."+count);
            stars[0].GetComponent<Image>().color = new Color(1f, 1f, 1f);
            stars[1].GetComponent<Image>().color = new Color(1f, 1f, 1f);
            stars[2].GetComponent<Image>().color = new Color(1f, 1f, 1f);
            FindObjectOfType<MoneyManager>().AddMoneyALot();
            if (coin250 != null)
            {
                coin250.gameObject.SetActive(true); 
                if (fadeCoroutine != null)
                {
                    StopCoroutine(fadeCoroutine);
                }
                fadeCoroutine = StartCoroutine(FadeText());
            }

        }
        else if (count >= 16 && count <= 20)
        {
            Debug.Log("Debug log: You got 0 stars."+count);
        }
    }
}
