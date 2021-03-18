using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class UIManager : MonoBehaviour
{
    private string bannerID = "ca-app-pub-3940256099942544/6300978111";
    private BannerView bannerView;

    public Player _player;
    public PlayerDeath _playerDeath;
    public Camera uiCamera;

    public GameObject canvas;
    public GameObject fadeImage;
    public GameObject backToMainMenuButton;
    public GameObject menuButtons;
    public GameObject menuTitle;
    public GameObject rateGameButton;
    public GameObject score;
    public GameObject endScore;
    public GameObject highscore;
    public GameObject highscoreText;
    public GameObject endScoreText;
    public GameObject restartButton;
    public GameObject diamondValue;
    public GameObject skinsMenu;

    public GameObject lock1;
    public GameObject lock2;
    public GameObject lock3;
    public GameObject lock4;
    public GameObject lock5;
    public GameObject lock1Value;
    public GameObject lock2Value;
    public GameObject lock3Value;
    public GameObject lock4Value;
    public GameObject lock5Value;

    public GameObject skin1;
    public GameObject skin2;
    public GameObject skin3;
    public GameObject skin4;
    public GameObject skin5;

    public GameObject skin1Cube;
    public GameObject skin2Cube;
    public GameObject skin3Cube;
    public GameObject skin4Cube;
    public GameObject skin5Cube;
    public GameObject defaultSkinCube;

    public GameObject highlightedDefaultEffect;
    public GameObject highlightedSkin1Effect;
    public GameObject highlightedSkin2Effect;
    public GameObject highlightedSkin3Effect;
    public GameObject highlightedSkin4Effect;
    public GameObject highlightedSkin5Effect;

    public GameObject activateSkin1Button;
    public GameObject activateSkin2Button;
    public GameObject activateSkin3Button;
    public GameObject activateSkin4Button;
    public GameObject activateSkin5Button;
    public GameObject activateDefaultButton;

    public GameObject newSkinUnlockedEffect;
    public GameObject notEnoughDiamondsPanel;
    public Text NotEnoughDiamondValueText;

    public Material defaultSkinMaterial;
    public Material Skin1Material;
    public Material Skin2Material;
    public Material Skin3Material;
    public Material Skin4Material;
    public Material Skin5Material;

    public GameObject audioSourceObject;

    public AudioSource menuMusic;
    public AudioSource notEnoughAudioSource;
    public AudioSource skinSelectedAudioSource;
    public AudioSource skinUnlockedAudioSource;
    public AudioSource runtimeAudioSource;

    bool skin1Bought;
    bool skin2Bought;
    bool skin3Bought;
    bool skin4Bought;
    bool skin5Bought;

    public GameObject pickUpText;
    public Image pickUpTimer;
    float maxTime = 5f;
    float timeLeft;

    public float diamondValueAmount;
    float scoreAmount;
    float highscoreAmount;
    float scoreIncreaseAmount = 1f;

    public bool startButtonPressed = false;

    string appID = "ca-app-pub-2442126721782189/1949174943";

    int skinChose;

    void AuthenticateUser()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                //Debug.Log("Logged into Google Play Services");
            }
            else
            {
                //Debug.Log("Unable to Sign into Google Play Services");
            }
        });
    }

    public static void PostToLeaderBoard(long newScore)
    {
        Social.ReportScore(newScore, GPGSIds.leaderboard_leaderboard, (bool success) =>
         {
             if (success)
             {
                 //Debug.Log("Posted To LeaderBoard");
             }
             else
             {
                 //Debug.Log("Failed to post to LeaderBoard");
             }
         });
    }

    public static void ShowLeaderboardUI()
    {
        //Debug.Log("Showing Board");
        PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard_leaderboard);
    }

    void PostToLeaderBoardButtonPress()
    {
        long highscore;

        if (long.TryParse(highscoreText.GetComponent<Text>().text, out highscore))
        {
            PostToLeaderBoard(highscore);
            //Debug.Log(highscore + " posted to leaderboard successfully");
        }
        else
        {
            //Debug.Log("failed to post leaderboard successfully");
        }
    }

    public void PlayButtonSound()
    {
        audioSourceObject.GetComponent<AudioSource>().Play();
    }

    private void Start()
    {
        RequestBanner();

        AuthenticateUser();

        MobileAds.Initialize(appID);
    }

    private void Awake()
    {
        if(!PlayerPrefs.HasKey("SkinChose"))
        {
            highlightedSkin1Effect.SetActive(false);
            highlightedSkin2Effect.SetActive(false);
            highlightedSkin3Effect.SetActive(false);
            highlightedSkin4Effect.SetActive(false);
            highlightedSkin5Effect.SetActive(false);
            highlightedDefaultEffect.SetActive(true);
        }

        if (PlayerPrefs.HasKey("SkinChose"))
        {
            if (PlayerPrefs.GetInt("SkinChose") == 0)
            {
                _player.GetComponent<Renderer>().material = defaultSkinMaterial;

                highlightedSkin1Effect.SetActive(false);
                highlightedSkin2Effect.SetActive(false);
                highlightedSkin3Effect.SetActive(false);
                highlightedSkin4Effect.SetActive(false);
                highlightedSkin5Effect.SetActive(false);
                highlightedDefaultEffect.SetActive(true);
            }
        }
        if (PlayerPrefs.HasKey("SkinChose"))
        {
            if (PlayerPrefs.GetInt("SkinChose") == 1)
            {
                _player.GetComponent<Renderer>().material = Skin1Material;

                highlightedSkin1Effect.SetActive(true);
                highlightedSkin2Effect.SetActive(false);
                highlightedSkin3Effect.SetActive(false);
                highlightedSkin4Effect.SetActive(false);
                highlightedSkin5Effect.SetActive(false);
                highlightedDefaultEffect.SetActive(false);
            }
        }
        if (PlayerPrefs.HasKey("SkinChose"))
        {
            if (PlayerPrefs.GetInt("SkinChose") == 2)
            {
                _player.GetComponent<Renderer>().material = Skin2Material;

                highlightedSkin1Effect.SetActive(false);
                highlightedSkin2Effect.SetActive(true);
                highlightedSkin3Effect.SetActive(false);
                highlightedSkin4Effect.SetActive(false);
                highlightedSkin5Effect.SetActive(false);
                highlightedDefaultEffect.SetActive(false);
            }
        }
        if (PlayerPrefs.HasKey("SkinChose"))
        {
            if (PlayerPrefs.GetInt("SkinChose") == 3)
            {
                _player.GetComponent<Renderer>().material = Skin3Material;

                highlightedSkin1Effect.SetActive(false);
                highlightedSkin2Effect.SetActive(false);
                highlightedSkin3Effect.SetActive(true);
                highlightedSkin4Effect.SetActive(false);
                highlightedSkin5Effect.SetActive(false);
                highlightedDefaultEffect.SetActive(false);
            }
        }
        if (PlayerPrefs.HasKey("SkinChose"))
        {
            if (PlayerPrefs.GetInt("SkinChose") == 4)
            {
                _player.GetComponent<Renderer>().material = Skin4Material;

                highlightedSkin1Effect.SetActive(false);
                highlightedSkin2Effect.SetActive(false);
                highlightedSkin3Effect.SetActive(false);
                highlightedSkin4Effect.SetActive(true);
                highlightedSkin5Effect.SetActive(false);
                highlightedDefaultEffect.SetActive(false);
            }
        }
        if (PlayerPrefs.HasKey("SkinChose"))
        {
            if (PlayerPrefs.GetInt("SkinChose") == 5)
            {
                _player.GetComponent<Renderer>().material = Skin5Material;

                highlightedSkin1Effect.SetActive(false);
                highlightedSkin2Effect.SetActive(false);
                highlightedSkin3Effect.SetActive(false);
                highlightedSkin4Effect.SetActive(false);
                highlightedSkin5Effect.SetActive(true);
                highlightedDefaultEffect.SetActive(false);
            }
        }

        if (PlayerPrefs.HasKey("lock1"))
        {
            if (PlayerPrefs.GetFloat("lock1") <= 0)
            {
                activateSkin1Button.SetActive(true);
                skin1Bought = true;
                skin1Cube.GetComponent<MeshRenderer>().enabled = true;
                lock1.GetComponent<Button>().interactable = false;
                lock1Value.SetActive(false);
            }
        }
        if (PlayerPrefs.HasKey("lock2"))
        {
            if (PlayerPrefs.GetFloat("lock2") <= 0)
            {
                activateSkin2Button.SetActive(true);
                skin2Bought = true;
                skin2Cube.GetComponent<MeshRenderer>().enabled = true;
                lock2.GetComponent<Button>().interactable = false;
                lock2Value.SetActive(false);
            }
        }
        if (PlayerPrefs.HasKey("lock3"))
        {
            if (PlayerPrefs.GetFloat("lock3") <= 0)
            {
                activateSkin3Button.SetActive(true);
                skin3Bought = true;
                skin3Cube.GetComponent<MeshRenderer>().enabled = true;
                lock3.GetComponent<Button>().interactable = false;
                lock3Value.SetActive(false);
            }
        }
        if (PlayerPrefs.HasKey("lock4"))
        {
            if (PlayerPrefs.GetFloat("lock4") <= 0)
            {
                activateSkin4Button.SetActive(true);
                skin4Bought = true;
                skin4Cube.GetComponent<MeshRenderer>().enabled = true;
                lock4.GetComponent<Button>().interactable = false;
                lock4Value.SetActive(false);
            }
        }
        if (PlayerPrefs.HasKey("lock5"))
        {
            if (PlayerPrefs.GetFloat("lock5") <= 0)
            {
                activateSkin5Button.SetActive(true);
                skin5Bought = true;
                skin5Cube.GetComponent<MeshRenderer>().enabled = true;
                lock5.GetComponent<Button>().interactable = false;
                lock5Value.SetActive(false);
            }
        }

        backToMainMenuButton.SetActive(false);
        pickUpTimer.gameObject.SetActive(false);
        pickUpText.SetActive(false);
        skinsMenu.SetActive(false);

        highscoreAmount = PlayerPrefs.GetFloat("highscore");
        diamondValueAmount = PlayerPrefs.GetFloat("diamondValue");

        timeLeft = maxTime;
    }

    private void FixedUpdate()
    {
        defaultSkinCube.transform.Rotate(0, Time.deltaTime * 10.0f, 0);
        skin1Cube.transform.Rotate(0, Time.deltaTime * 10.0f, 0);
        skin2Cube.transform.Rotate(0, Time.deltaTime * 10.0f, 0);
        skin3Cube.transform.Rotate(0, Time.deltaTime * 10.0f, 0);
        skin4Cube.transform.Rotate(0, Time.deltaTime * 10.0f, 0);
        skin5Cube.transform.Rotate(0, Time.deltaTime * 10.0f, 0);
    }

    private void Update()
    {     
        diamondValue.GetComponent<Text>().text = diamondValueAmount.ToString("0");

        score.GetComponent<Text>().text = scoreAmount.ToString("0");
        endScore.GetComponent<Text>().text = score.GetComponent<Text>().text;

        highscore.GetComponent<Text>().text = highscoreAmount.ToString("0");

        if (scoreAmount > highscoreAmount)
        {
            highscoreAmount = scoreAmount;
        }

        if (_player.gameIsRunning)
        {
            scoreAmount += scoreIncreaseAmount * Time.deltaTime;
        }

        if (_player.tankModeEnabled)
        {
            pickUpTimer.gameObject.SetActive(true);
            pickUpText.SetActive(true);

            if (timeLeft > 0)
            {              
                timeLeft -= Time.deltaTime;

                pickUpTimer.fillAmount = timeLeft / maxTime;
            }
            else
            {
                _player.tankModeEnabled = false;
                _player.tankForceField.SetActive(false);

                pickUpTimer.gameObject.SetActive(false);
                pickUpText.SetActive(false);

                timeLeft = maxTime;
            }
        }

        if(menuMusic.volume <=0 && runtimeAudioSource.volume <0.5)
        {
            runtimeAudioSource.volume += 0.5f * Time.deltaTime;
        }

        if(!_playerDeath.playerHasDied)
        {
            restartButton.SetActive(false);
        }
        else if(_playerDeath.playerHasDied)
        {
            restartButton.SetActive(true);
        }
    }

    public void StartGame()
    {     
        backToMainMenuButton.SetActive(false);
        rateGameButton.SetActive(false);
        highscore.SetActive(true);
        endScore.SetActive(true);
        highscoreText.SetActive(true);
        endScoreText.SetActive(true);
        restartButton.SetActive(true);

        startButtonPressed = true;
        
        canvas.GetComponent<Animator>().Play("DoNothingRunTime");

        runtimeAudioSource.Play();
        StartCoroutine(FadeOutMenuAudio());
        StartCoroutine(DisableMenuUI());
    }

    IEnumerator DisableMenuUI()
    {
        yield return new WaitForSeconds(1);
        menuButtons.SetActive(false);
        menuTitle.SetActive(false);
    }

    public IEnumerator FadeOutMenuAudio()
    {
        while (menuMusic.volume > 0.0f)
        {
            menuMusic.volume -= Time.deltaTime / 1.0f;            

            yield return null;
        }        
    }



    public IEnumerator FadeOutRuntimeAudio()
    {
        while (runtimeAudioSource.volume > 0f)
        {
            runtimeAudioSource.volume -= 0.1f - Time.deltaTime;

            yield return null;
        }
    }

    public IEnumerator FadeInMenuAudio()
    {
        if (!menuMusic.isPlaying)
        {
            menuMusic.Play();
        }

        if (runtimeAudioSource.volume <= 0)
        {
            menuMusic.volume += 0.2f + Time.deltaTime;
        }

        yield return null;
    }


    public void ActivateBackToMainMenuButton()
    {
        backToMainMenuButton.SetActive(true);
    }

    public void ActivateSkinsMenu()
    {
        skinsMenu.SetActive(true);
    }

    public void DeactivateSkinsMenu()
    {
        skinsMenu.SetActive(false);
    }

    IEnumerator FadeOutScene()
    {
        gameObject.GetComponent<Animator>().Play("FadeScene");

        yield return new WaitForSeconds(.6f);

        string sceneName = "Scene";

        SceneManager.LoadScene(sceneName);
    }

    public void RestartGame()
    {
        PlayButtonSound();

        PlayerPrefs.SetFloat("highscore", highscoreAmount);
        PlayerPrefs.SetFloat("diamondValue", diamondValueAmount);

        PostToLeaderBoardButtonPress();

        PlayerPrefs.Save();        

        StartCoroutine(FadeOutScene());
    }

    public void ChangeToDefaultSkin()
    {
        skinSelectedAudioSource.Play();

        highlightedSkin1Effect.SetActive(false);
        highlightedSkin2Effect.SetActive(false);
        highlightedSkin3Effect.SetActive(false);
        highlightedSkin4Effect.SetActive(false);
        highlightedSkin5Effect.SetActive(false);
        highlightedDefaultEffect.SetActive(true);

        skinChose = 0;
        PlayerPrefs.SetInt("SkinChose", skinChose);
        _player.GetComponent<Renderer>().material = defaultSkinMaterial;
    }
    public void ChangeToSkin1()
    {
        if (skin1Bought)
        {
            skinSelectedAudioSource.Play();

            highlightedSkin1Effect.SetActive(true);
            highlightedSkin2Effect.SetActive(false);
            highlightedSkin3Effect.SetActive(false);
            highlightedSkin4Effect.SetActive(false);
            highlightedSkin5Effect.SetActive(false);
            highlightedDefaultEffect.SetActive(false);

            skinChose = 1;
            PlayerPrefs.SetInt("SkinChose", skinChose);
            _player.GetComponent<Renderer>().material = Skin1Material;
        }                    
    }
    public void ChangeToSkin2()
    {
        if (skin2Bought)
        {
            skinSelectedAudioSource.Play();

            highlightedSkin1Effect.SetActive(false);
            highlightedSkin2Effect.SetActive(true);
            highlightedSkin3Effect.SetActive(false);
            highlightedSkin4Effect.SetActive(false);
            highlightedSkin5Effect.SetActive(false);
            highlightedDefaultEffect.SetActive(false);

            skinChose = 2;
            PlayerPrefs.SetInt("SkinChose", skinChose);
            _player.GetComponent<Renderer>().material = Skin2Material;
        }
    }
    public void ChangeToSkin3()
    {
        if (skin3Bought)
        {
            skinSelectedAudioSource.Play();

            highlightedSkin1Effect.SetActive(false);
            highlightedSkin2Effect.SetActive(false);
            highlightedSkin3Effect.SetActive(true);
            highlightedSkin4Effect.SetActive(false);
            highlightedSkin5Effect.SetActive(false);
            highlightedDefaultEffect.SetActive(false);

            skinChose = 3;
            PlayerPrefs.SetInt("SkinChose", skinChose);
            _player.GetComponent<Renderer>().material = Skin3Material;
        }
    }
    public void ChangeToSkin4()
    {
        if (skin4Bought)
        {
            skinSelectedAudioSource.Play();

            highlightedSkin1Effect.SetActive(false);
            highlightedSkin2Effect.SetActive(false);
            highlightedSkin3Effect.SetActive(false);
            highlightedSkin4Effect.SetActive(true);
            highlightedSkin5Effect.SetActive(false);
            highlightedDefaultEffect.SetActive(false);

            skinChose = 4;
            PlayerPrefs.SetInt("SkinChose", skinChose);
            _player.GetComponent<Renderer>().material = Skin4Material;
        }
    }
    public void ChangeToSkin5()
    {
        if (skin5Bought)
        {
            skinSelectedAudioSource.Play();

            highlightedSkin1Effect.SetActive(false);
            highlightedSkin2Effect.SetActive(false);
            highlightedSkin3Effect.SetActive(false);
            highlightedSkin4Effect.SetActive(false);
            highlightedSkin5Effect.SetActive(true);
            highlightedDefaultEffect.SetActive(false);

            skinChose = 5;
            PlayerPrefs.SetInt("SkinChose", skinChose);
            _player.GetComponent<Renderer>().material = Skin5Material;
        }
    }

    public void Skin1Unlocked()
    {
        if (diamondValueAmount >= 400) 
        {
            skinUnlockedAudioSource.Play();

            highlightedSkin1Effect.SetActive(true);
            highlightedSkin2Effect.SetActive(false);
            highlightedSkin3Effect.SetActive(false);
            highlightedSkin4Effect.SetActive(false);
            highlightedSkin5Effect.SetActive(false);
            highlightedDefaultEffect.SetActive(false);

            _player.GetComponent<Renderer>().material = Skin1Material;
            activateSkin1Button.SetActive(true);
            lock1.GetComponent<Image>().fillAmount = 0;
            lock1.GetComponent<Button>().interactable = false;
            lock1Value.SetActive(false);
            PlayerPrefs.SetFloat("lock1", lock1.GetComponent<Image>().fillAmount);
            PlayerPrefs.Save();

            diamondValueAmount -= 400;

            skin1Bought = true;

            Instantiate(newSkinUnlockedEffect, skin1.transform.position, skin1.transform.rotation);

            skin1Cube.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            notEnoughAudioSource.Play();
            NotEnoughDiamondValueText.text = diamondValueAmount.ToString("0") + " / 400";
            notEnoughDiamondsPanel.SetActive(true);
            skinsMenu.SetActive(false);
            backToMainMenuButton.SetActive(false);
        }
    }
    public void Skin2Unlocked()
    {
        if (diamondValueAmount >= 400)
        {
            skinUnlockedAudioSource.Play();

            highlightedSkin1Effect.SetActive(false);
            highlightedSkin2Effect.SetActive(true);
            highlightedSkin3Effect.SetActive(false);
            highlightedSkin4Effect.SetActive(false);
            highlightedSkin5Effect.SetActive(false);
            highlightedDefaultEffect.SetActive(false);

            _player.GetComponent<Renderer>().material = Skin2Material;
            activateSkin2Button.SetActive(true);
            lock2.GetComponent<Image>().fillAmount = 0;
            lock2.GetComponent<Button>().interactable = false;
            lock2Value.SetActive(false);
            PlayerPrefs.SetFloat("lock2", lock2.GetComponent<Image>().fillAmount);
            PlayerPrefs.Save();

            diamondValueAmount -= 400;

            skin2Bought = true;

            Instantiate(newSkinUnlockedEffect, skin2.transform.position, skin2.transform.rotation);

            skin2Cube.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            notEnoughAudioSource.Play();
            NotEnoughDiamondValueText.text = diamondValueAmount.ToString("0") + " / 400";
            notEnoughDiamondsPanel.SetActive(true);
            skinsMenu.SetActive(false);
            backToMainMenuButton.SetActive(false);
        }
    }
    public void Skin3Unlocked()
    {
        if (diamondValueAmount >= 600)
        {
            skinUnlockedAudioSource.Play();

            highlightedSkin1Effect.SetActive(false);
            highlightedSkin2Effect.SetActive(false);
            highlightedSkin3Effect.SetActive(true);
            highlightedSkin4Effect.SetActive(false);
            highlightedSkin5Effect.SetActive(false);
            highlightedDefaultEffect.SetActive(false);

            _player.GetComponent<Renderer>().material = Skin3Material;
            activateSkin3Button.SetActive(true);
            lock3.GetComponent<Image>().fillAmount = 0;
            lock3.GetComponent<Button>().interactable = false;
            lock3Value.SetActive(false);
            PlayerPrefs.SetFloat("lock3", lock3.GetComponent<Image>().fillAmount);
            PlayerPrefs.Save();

            diamondValueAmount -= 600;

            skin3Bought = true;

            Instantiate(newSkinUnlockedEffect, skin3.transform.position, skin3.transform.rotation);

            skin3Cube.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            notEnoughAudioSource.Play();
            NotEnoughDiamondValueText.text = diamondValueAmount.ToString("0") + " / 600";
            notEnoughDiamondsPanel.SetActive(true);
            skinsMenu.SetActive(false);
            backToMainMenuButton.SetActive(false);
        }
    }
    public void Skin4Unlocked()
    {
        if (diamondValueAmount >= 600)
        {
            skinUnlockedAudioSource.Play();

            highlightedSkin1Effect.SetActive(false);
            highlightedSkin2Effect.SetActive(false);
            highlightedSkin3Effect.SetActive(false);
            highlightedSkin4Effect.SetActive(true);
            highlightedSkin5Effect.SetActive(false);
            highlightedDefaultEffect.SetActive(false);

            _player.GetComponent<Renderer>().material = Skin4Material;
            activateSkin4Button.SetActive(true);
            lock4.GetComponent<Image>().fillAmount = 0;
            lock4.GetComponent<Button>().interactable = false;
            lock4Value.SetActive(false);
            PlayerPrefs.SetFloat("lock4", lock4.GetComponent<Image>().fillAmount);
            PlayerPrefs.Save();

            diamondValueAmount -= 600;

            skin4Bought = true;

            Instantiate(newSkinUnlockedEffect, skin4.transform.position, skin4.transform.rotation);

            skin4Cube.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            notEnoughAudioSource.Play();
            NotEnoughDiamondValueText.text = diamondValueAmount.ToString("0") + " / 600";
            notEnoughDiamondsPanel.SetActive(true);
            skinsMenu.SetActive(false);
            backToMainMenuButton.SetActive(false);
        }
    }
    public void Skin5Unlocked()
    {
        if (diamondValueAmount >= 800)
        {
            skinUnlockedAudioSource.Play();

            highlightedSkin1Effect.SetActive(false);
            highlightedSkin2Effect.SetActive(false);
            highlightedSkin3Effect.SetActive(false);
            highlightedSkin4Effect.SetActive(false);
            highlightedSkin5Effect.SetActive(true);
            highlightedDefaultEffect.SetActive(false);

            _player.GetComponent<Renderer>().material = Skin5Material;
            activateSkin5Button.SetActive(true);
            lock5.GetComponent<Image>().fillAmount = 0;
            lock5.GetComponent<Button>().interactable = false;
            lock5Value.SetActive(false);
            PlayerPrefs.SetFloat("lock5", lock5.GetComponent<Image>().fillAmount);
            PlayerPrefs.Save();

            diamondValueAmount -= 800;

            skin5Bought = true;

            Instantiate(newSkinUnlockedEffect, skin5.transform.position, skin5.transform.rotation);

            skin5Cube.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            notEnoughAudioSource.Play();
            NotEnoughDiamondValueText.text = diamondValueAmount.ToString("0") + " / 800";
            notEnoughDiamondsPanel.SetActive(true);
            skinsMenu.SetActive(false);
            backToMainMenuButton.SetActive(false);
        }
    }

    public void CloseNotEnoughDiamondsPanel()
    {
        skinsMenu.SetActive(true);
        notEnoughDiamondsPanel.SetActive(false);
        backToMainMenuButton.SetActive(true);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("highscore", highscoreAmount);
        PlayerPrefs.SetFloat("diamondValue", diamondValueAmount);

        PlayerPrefs.Save();
    }

    public void WatchToUnlock()
    {
        if(Advertisement.IsReady("rewardedVideo"))
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleShowResult });
        }

        StartCoroutine(CloseWindow());
    }

    IEnumerator CloseWindow()
    {
        yield return new WaitForSeconds(1);

        CloseNotEnoughDiamondsPanel();
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                diamondValueAmount += 25;
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Failed:
                break;
        }
    }


    public void RequestBanner()
    {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);

        bannerView.Show();
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }

    /// <summary>
    /// CHANGE ANDYDEVELOPER.CUBERUSH TO CORRECT FIELD!!!
    /// </summary>
    public void RateGameButton()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.AndyDeveloper.CubeRush");
#endif
    }
}
