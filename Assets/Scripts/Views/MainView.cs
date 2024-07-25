using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
//using Notionhub;
public class MainView : MonoBehaviour {

	#region Variables, Constants & Initializers
	private bool quitFlag = true;
	private int pizzaDeliveredNo;
	public GameObject clouds, cloud2;
	public RectTransform CloudsEndPoint, Cloud2EndPoint;
	public GameObject play, rateus, moreGames, privacyPolicy, setting, volume;
	public RectTransform playEndPoint, rateusEndPoint, moreGamesEndPoint, privacyPolicyEndPoint, settingEndPoint;
	public GameObject Character, CharacterHead;
	public RectTransform CharacterEndPoint;
	public GameObject rotatingImage;
	public GameObject quitPopup;
	public GameObject settingPopup;
	public GameObject LoadinBg;
	public Image LoadingFilled;

    //New Variables added
    public GameObject ModeSelection;
    public GameObject LowCashSelection;
    public GameObject CarrerSelection;
    public Sprite clickedButtonImgage;
    public Sprite otherButtonImgage;
    public Button carrerMode;
    public Button MultiPlayerMode;
    public Button FreeMode;

    [SerializeField] Image soundon;
    [SerializeField] Image soundoff;
    private bool muted = false;
    public Text onoffText;

    public Sprite[] Player2Images;
    public Image Player2FinalImage;
    public int ImageNumber;
	
	public GameObject BattelStart;
    public GameObject BattelAnimation;
    public GameObject Player2Pic;
    public GameObject Player2Name;

    public GameObject LeftPlayer;
    public GameObject RightPlayer;
    public RectTransform AnimationEndPoint;
    public GameObject watchAdPanel;
    public GameObject multiplayerLock, freeModeLock;
    public GameObject multiplayerBackImage, freeModeBackImage;
    public GameObject multiplayerWatchAdButton, freeModeWatchAdButton;

    public Text watchAdTextCount;
    public Text coinText;
    public Text scoreText;

    public ScrollRect careerModeScroll;
    private int lastPlayedLevel;

    public Button soundBtn;

    public Sprite onSprite;
    public Sprite offSprite;

    public GameObject Content;

    public float[] levelPos;



    //public string[] LevelNames;
    //New Variables added
    //public GameObject petgameButton;// burgerGameButton;

    #endregion

    #region Lifecycle Methods

    // Use this for initialization
    void Start () {
        // sfx sound //
        if (PlayerPrefs.GetInt("isSetting") == 0)
        {
            PlayerPrefs.SetInt("SoundController", 1);
            PlayerPrefs.SetInt("isSetting", 1);
        }
        // end ///

        CareerScrollSetting();

        //PlayerPrefs.SetInt("PlayerScore", 40000);
        scoreText.text = PlayerPrefs.GetInt("PlayerScore").ToString();
        //AdsSDKManager ads = GameObject.FindObjectOfType<AdsSDKManager>();
        //if (ads != null)
        //{
        //    ads.showBanner(GoogleMobileAds.Api.AdPosition.Top);
        //}
        UniversalAnalytics.LogScreenView ("Main Menu");
		Invoke ("SetViewContents", 0.1f);
        if (PlayerPrefs.GetInt("ComingFromSplash1") == 0)
        {
            if (PlayerPrefs.GetInt("CareerMode") == 1)
            {
                ModeSelection.SetActive(true);
                CarrerSelection.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Multiplayer") == 1)
            {
                ModeSelection.SetActive(true);
                CarrerSelection.SetActive(false);
            }
            else if (PlayerPrefs.GetInt("FreeMode") == 1)
            {
                ModeSelection.SetActive(true);
                CarrerSelection.SetActive(false);
            }
        }
        else
        {
            PlayerPrefs.SetInt("ComingFromSplash1", 0);

        }

        // for Music Bg on/off   //

        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();

        // for sfx sound 
        if (PlayerPrefs.GetInt("SoundController") == 0)
        {
            soundBtn.transform.gameObject.GetComponent<Image>().sprite = offSprite;
        }
        else
        {
            soundBtn.transform.gameObject.GetComponent<Image>().sprite = onSprite;
        }
        //AudioListener.pause = muted;

        //-----end------//
    }

    void updateScrollForStart()
    {
        // Load the last played level from PlayerPrefs
        lastPlayedLevel = PlayerPrefs.GetInt("LevelPlayed", 0);

        // Call the function to set the scroll position based on the last played level
        //CareerScrollSetting(lastPlayedLevel);
    }





    public void CareerScrollSetting()
    {
        careerModeScroll.verticalNormalizedPosition = levelPos[PlayerPrefs.GetInt("LevelPlayed")];
    }

    // Update is called once per frame
    void Update () {
        //moreGames.SetActive(false);
        #if UNITY_ANDROID
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			if(quitFlag){
                //AdsSDKManager ads = GameObject.FindObjectOfType<AdsSDKManager>();
                //if (ads != null)
                //{
                //    ads.ShowAdd();
                //}
                quitFlag = false;
			}
			
			if (quitPopup != null) {
				if(quitPopup.GetComponent<RectTransform>().localScale != Vector3.one) {
					Hashtable tweenParams = new Hashtable();
					tweenParams.Add ("scale", Vector3.one);
					tweenParams.Add ("time", 0.5f);
					//tweenParams.Add ("oncompletetarget", gameObject);
					//tweenParams.Add ("oncomplete", "HideCartFullIndication");
					iTween.ScaleTo(quitPopup.gameObject, tweenParams);
				}
			} else {
				OnQuitYesButtonClicked();
			}
		}

#endif
    }

	void Destroy() {
		iTween.Stop ();
	}

	#endregion

	#region Callback Methods

	private void SetViewContents() {
		buttonActive ();
		CloudsMovement ();
		CharacterHeadMovement ();
        if (PlayerPrefs.GetInt("CareerMode") == 1)
        {
            //carrerMode.transform.GetChild(1).GetComponent<Image>().sprite = clickedButtonImgage;
            carrerMode.transform.GetChild(0).gameObject.SetActive (true);
            MultiPlayerMode.transform.GetChild(0).gameObject.SetActive(false);
            FreeMode.transform.GetChild(0).gameObject.SetActive(false);
            carrerMode.transform.GetComponent<ActionManager>().enabled = true;
            Debug.Log("carrerMode" + "clicked OK this button");
        }
        if (PlayerPrefs.GetInt("Multiplayer") == 1)
        {
            //MultiPlayerMode.transform.GetChild(1).GetComponent<Image>().sprite = clickedButtonImgage;
            MultiPlayerMode.transform.GetChild(0).gameObject.SetActive (true);
            carrerMode.transform.GetChild(0).gameObject.SetActive(false);
            FreeMode.transform.GetChild(0).gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("FreeMode") == 1)
        {
            //FreeMode.transform.GetChild(1).GetComponent<Image>().sprite = clickedButtonImgage;
            FreeMode.transform.GetChild(0).gameObject.SetActive(true);
            carrerMode.transform.GetChild(0).gameObject.SetActive(false);
            MultiPlayerMode.transform.GetChild(0).gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("MultiplayerUnLock") == 1)
        {
            multiplayerLock.SetActive(false);
            multiplayerBackImage.SetActive(false);
            multiplayerWatchAdButton.SetActive(false);
            MultiPlayerMode.enabled = true;
            //MultiPlayerMode.transform.GetComponent<ActionManager>().enabled = true;
        }

        if (PlayerPrefs.GetInt("FreeModeUnLock") == 1)
        {
            freeModeLock.SetActive(false);
            freeModeBackImage.SetActive(false);
            freeModeWatchAdButton.SetActive(false);
            FreeMode.enabled = true;
            //FreeMode.transform.GetComponent<ActionManager>().enabled = true;
        }


        //		LoadingBgActive ();
        Invoke ("CharacterComesInn", 0.5f);
	}

	private void ScaleAction(GameObject obj,float scaleval,float time,iTween.EaseType type,iTween.LoopType loopType) {
		Hashtable tweenParams = new Hashtable();
		tweenParams.Add ("scale", new Vector3 (scaleval,scaleval, 0));
		tweenParams.Add ("time", time);
		tweenParams.Add ("easetype", type);
		tweenParams.Add ("looptype", loopType);
		iTween.ScaleTo(obj, tweenParams);
	}

	private void MoveAction(GameObject obj,RectTransform pos,float time,iTween.EaseType actionType,iTween.LoopType loopType){
		Hashtable tweenParams = new Hashtable();
		tweenParams.Add ("x", pos.position.x);
		tweenParams.Add ("y", pos.position.y);
		tweenParams.Add ("time", time);
		tweenParams.Add ("easetype", actionType);
		tweenParams.Add ("looptype", loopType);
		iTween.MoveTo (obj, tweenParams);
	}

	private void RotateAction(GameObject obj,float roatationamount,float t,iTween.EaseType actionType,iTween.LoopType loopType){
		Hashtable tweenParams = new Hashtable ();
		tweenParams.Add ("z", roatationamount);
		tweenParams.Add ("time", t);
		tweenParams.Add ("easetype", actionType);
		tweenParams.Add ("looptype", loopType);
		iTween.RotateTo (obj, tweenParams);
	}

	private void RotateAction360(GameObject obj,float roatationamount,float t,iTween.EaseType actionType,iTween.LoopType loopType){
		Hashtable tweenParams = new Hashtable ();
		tweenParams.Add ("z", roatationamount);
		tweenParams.Add ("time", t);
		tweenParams.Add ("easetype", actionType);
		tweenParams.Add ("looptype", loopType);
		iTween.RotateBy (obj, tweenParams);
	}

	private void buttonActive(){
		int index = PlayerPrefs.GetInt ("ButtonActive");
		index = index % 2;
		//if (index == 0) {
		//	//petgameButton.SetActive (true);
  //         // burgerGameButton.SetActive(false);
  //         // ScaleAction (petgameButton, 1.1f, 0.5f, iTween.EaseType.easeInBounce, iTween.LoopType.pingPong);
		//} else {
		//	//burgerGameButton.SetActive (true);
  //         // petgameButton.SetActive(false);
  //         // ScaleAction (burgerGameButton, 1.1f, 0.5f, iTween.EaseType.easeInBounce, iTween.LoopType.pingPong);
		//}

		PlayerPrefs.SetInt ("ButtonActive", PlayerPrefs.GetInt ("ButtonActive") + 1 );

	}

    private void CarearSelectionAnimatorOff()
    {
        CarrerSelection.GetComponent<Animator>().enabled = false;
    }

	private void CloudsMovement(){
		MoveAction (clouds, CloudsEndPoint, 20.0f, iTween.EaseType.linear, iTween.LoopType.loop);
		MoveAction (cloud2, Cloud2EndPoint, 25.0f, iTween.EaseType.linear, iTween.LoopType.loop);
	}

	private void CharacterHeadMovement(){
		RotateAction (CharacterHead, -2.0f, 3.0f, iTween.EaseType.linear, iTween.LoopType.pingPong);
	
	}

	private void CharacterComesInn(){
		MoveAction (Character, CharacterEndPoint, 0.2f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
		Invoke ("settingButtonComes", 0.2f);
	}

    private void settingButtonComes()
    {
        MoveAction(setting, settingEndPoint, 0.2f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
        Invoke("PrivacyPolicyButtonComes", 0.2f);
    }

    private void PrivacyPolicyButtonComes()
    {
        MoveAction(privacyPolicy, privacyPolicyEndPoint, 0.2f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
        Invoke("rateUsButtonComes", 0.2f);
    }
    private void rateUsButtonComes(){
		MoveAction (rateus, rateusEndPoint, 0.2f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
		Invoke ("MoreFunButtonComes", 0.2f);
	}

    private void MoreFunButtonComes()
    {
        MoveAction(moreGames, moreGamesEndPoint, 0.2f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
        Invoke("PlayButtonComes", 0.2f);
    }


    private void PlayButtonComes(){
		MoveAction (play, playEndPoint, 0.2f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
		Invoke ("SprinkleActive", 0.5f);
	}

	private void SprinkleActive(){
		rotatingImage.SetActive (true);
	}


	public void OnPlayButtonClicked() {
        LoadinBg.SetActive(true);
        StartCoroutine(FillAction(LoadingFilled));
        GameManager.instance.LogDebug ("Play Clicked");
		SoundManager.instance.PlayButtonClickSound ();
        Invoke("modSelectionPanelOn", 4.0f);
        //Invoke("callAds", 1.0f);
        //LoadingBgActive ();

    }

    void modSelectionPanelOn()
    {
        SoundManager.instance.PlayButtonClickSound();
        LoadingFilled.fillAmount = 0;
        ModeSelection.SetActive(true);
        LoadinBg.SetActive(false);

    }

	public void OnRateUsButtonClicked() {
        GameManager.instance.LogDebug("RateUs Clicked");
		SoundManager.instance.PlayButtonClickSound ();
        //Application.OpenURL ("https://play.google.com/store/apps/details?id=" +Application.identifier);
        Application.OpenURL("market://details?id=com.pdi.mypizzashop.goodpizza.business.makergamepotrait");

    }

    public void OnMoreFunButtonClicked() {
		GameManager.instance.LogDebug ("MoreFun Clicked");
		SoundManager.instance.PlayButtonClickSound ();
		Application.OpenURL ("https://play.google.com/store/apps/dev?id=4826365601331502275");
	}
		
	public void OnPrivacyPolicyButtonClicked() {
		GameManager.instance.LogDebug ("PrivacyPolicy Clicked");
		SoundManager.instance.PlayButtonClickSound ();
		Application.OpenURL ("https://doc-hosting.flycricket.io/pet-dragon-inc-policy/0eaa397d-d077-424f-9603-bc62ba307b2f/privacy");
	}

	public void OnPetGameClicked() {
		SoundManager.instance.PlayButtonClickSound ();
		Application.OpenURL ("https://play.google.com/store/apps/developer?id=BestOne+Games");
	}

	public void OnBurgerGameClicked() {
		SoundManager.instance.PlayButtonClickSound ();
		Application.OpenURL ("https://play.google.com/store/apps/developer?id=BestOne+Games");
	}


	public void OnQuitNoButtonClicked()
    {
        SoundManager.instance.PlayButtonClickSound();
        GameManager.instance.LogDebug ("QuitNo Clicked");
		quitPopup.GetComponent<RectTransform> ().localScale = Vector3.zero;
	}

	public void OnQuitYesButtonClicked()
    {
        SoundManager.instance.PlayButtonClickSound();
        GameManager.instance.LogDebug ("QuitYes Clicked");
		Application.Quit ();
	}

    //New Function Added
    public void OnCareerModeSelect()
	{
        SoundManager.instance.PlayButtonClickSound();
        carrerMode.transform.GetChild(0).gameObject.SetActive(true);
        MultiPlayerMode.transform.GetChild(0).gameObject.SetActive(false);
        FreeMode.transform.GetChild(0).gameObject.SetActive(false);
        //carrerMode.transform.GetChild(1).GetComponent<Image>().sprite = clickedButtonImgage;
        //MultiPlayerMode.transform.GetChild(1).GetComponent<Image>().sprite = otherButtonImgage;
        //FreeMode.transform.GetChild(1).GetComponent<Image>().sprite = otherButtonImgage;
        CarrerSelection.SetActive(true);
        //ModeSelection.SetActive(false);
        PlayerPrefs.SetInt("CareerMode", 1);
        PlayerPrefs.SetInt("Multiplayer", 0);
        PlayerPrefs.SetInt("FreeMode", 0);
        Invoke("CarearSelectionAnimatorOff", 1.8f);
    }

    public void OnMultiPlayerModeSelect()
    {
        SoundManager.instance.PlayButtonClickSound();
        MultiPlayerMode.transform.GetChild(0).gameObject.SetActive(true);
        FreeMode.transform.GetChild(0).gameObject.SetActive(false);
        carrerMode.transform.GetChild(0).gameObject.SetActive(false);
        //carrerMode.transform.GetChild(1).GetComponent<Image>().sprite = otherButtonImgage;
        //MultiPlayerMode.transform.GetChild(1).GetComponent<Image>().sprite = clickedButtonImgage;
        //FreeMode.transform.GetChild(1).GetComponent<Image>().sprite = otherButtonImgage;
        //ModeSelection.SetActive(false);
        PlayerPrefs.SetInt("CareerMode", 0);
        PlayerPrefs.SetInt("Multiplayer", 1);
        PlayerPrefs.SetInt("FreeMode", 0);
		BattleFunction();
        //if (PlayerPrefs.GetInt("Multiplayer") == 1)
        //{
            //Invoke("BattleStartFun", 8.0f);
            //Invoke("AnimationStopFun", 8.0f);
            //Invoke("BattleAnimation", 1.0f);
            //Invoke("versusAnimation", 5.0f);
            //BattelStart.SetActive(false);
        //}
        //Invoke("PlayMultiplayer", 0.5f);
        //PlayMultiplayer();
    }

	void BattleFunction()
	{
        SoundManager.instance.backgroundLoop.Stop();
        SoundManager.instance.PlayBgGameSoundLoop();
        BattelAnimation.SetActive(true);
        InvokeRepeating("Player2ImageShuffle", 0.1f, 0.1f);
        Invoke("cancelImageShuffle", 3.0f);
        Invoke("versusAnimation", 4.0f);

    }



    void cancelImageShuffle()
    {
        CancelInvoke("Player2ImageShuffle");
        //SoundManager.instance.PlayScoreAddSound();
        //ParticleManger.instance.ShowStarParticleForScore(Player2Pic.gameObject);
    }

    void Player2ImageShuffle()
    {
        int imageInt = Random.Range(0, Player2Images.Length - 1);
        ImageNumber = imageInt;
        Player2Pic.GetComponent<Image>().sprite = Player2Images[imageInt];
        SoundManager.instance.PlayButtonClickSound();
        //SoundManager.instance.playPhotoFlip();

    }

    void versusAnimation()
    {
        Player2FinalImage.GetComponent<Image>().sprite = Player2Images[ImageNumber];
        //SoundManager.instance.PlaysparkleAchivementSound();
        MoveAction(LeftPlayer, AnimationEndPoint, 1.0f, iTween.EaseType.linear, iTween.LoopType.none);
        MoveAction(RightPlayer, AnimationEndPoint, 1.0f, iTween.EaseType.linear, iTween.LoopType.none);
        Invoke("PlayMultiplayer", 2.0f);
        //levelCompletedParticles.SetActive(true);
        //Invoke("ParticalOff", 6.0f);
    }


    void PlayMultiplayer()
	{
        LoadingBackgroundActive ();
    }

    private void LoadingBackgroundActive()
    {
        LoadinBg.SetActive(true);
        StartCoroutine(FillAction(LoadingFilled));
        Invoke("SenceLoad", 4.0f);
       // Invoke("callAds", 1.0f);
    }

    void callAds()
    {
        // MyAdsManager.instance.CallInterstitialAd(Adspref.Menu);
        AdsManager.Instance.ShowInterstitial("Show AD on Loading Screen");
    }

    private void SenceLoad()
    {
        print("Loading Completed");
        GameManager.instance.SelectedPizzaFlavour = 8;
        NavigationManager.instance.ReplaceScene(GameScene.STORESHOPPINGVIEW);
    }

    public void OnFreeModeSelect()
    {
        //SoundManager.instance.PlayButtonClickSound();
        //carrerMode.transform.GetChild(1).GetComponent<Image>().sprite = otherButtonImgage;
        ////MultiPlayerMode.transform.GetChild(1).GetComponent<Image>().sprite = otherButtonImgage;
        //FreeMode.transform.GetChild(1).GetComponent<Image>().sprite = clickedButtonImgage;
        FreeMode.transform.GetChild(0).gameObject.SetActive(true);
        carrerMode.transform.GetChild(0).gameObject.SetActive(false);
        MultiPlayerMode.transform.GetChild(0).gameObject.SetActive(false);
        ModeSelection.SetActive(false);
        PlayerPrefs.SetInt("CareerMode", 0);
        PlayerPrefs.SetInt("Multiplayer", 0);
        PlayerPrefs.SetInt("FreeMode", 1);
        LoadingBgActive ();
    }

    public void closePanel(GameObject parent)
    {
        SoundManager.instance.PlayButtonClickSound();
        parent.SetActive(false);
        //AdsManager.Instance.HideMREC();
    }

    public void backPanel(GameObject parent)
    {
        LoadinBg.SetActive(true);
        StartCoroutine(FillAction(LoadingFilled));
        GameManager.instance.LogDebug("Play Clicked");
        Invoke("loadingBgOff", 4.0f);
        //Invoke("callAds", 1.0f);
        SoundManager.instance.PlayButtonClickSound();
        parent.SetActive(false);
    }

    void loadingBgOff()
    {
        SoundManager.instance.PlayButtonClickSound();
        LoadingFilled.fillAmount = 0;
        LoadinBg.SetActive(false);
    }
    //New Function Added

    private void LoadingBgActive()
    {
        LoadinBg.SetActive(true);
        StartCoroutine(FillAction(LoadingFilled));
        Invoke("LoadingFull", 4.0f);
    }

    IEnumerator FillAction (Image img){
		if (img.fillAmount < 1) {
			img.fillAmount = img.fillAmount + 0.009f;
			yield return new WaitForSeconds (0.02f);
			StartCoroutine (FillAction (img));
		}  else if (img.color.a >= 1f) {
			StopCoroutine (FillAction (img));
		}
	}

	private void LoadingFull(){
		print ("Loading Completed");
		NavigationManager.instance.ReplaceScene (GameScene.CLEANINGVIEW);
        SoundManager.instance.backgroundLoop.Stop ();
        SoundManager.instance.PlayBgGameSoundLoop();
    }

    public void OnClickWatchAdPanelMultiPlayer()
    {
        Debug.Log("clickMultiplayer");
        SoundManager.instance.PlayButtonClickSound();
        PlayerPrefs.SetInt("CareerMode", 0);
        PlayerPrefs.SetInt("Multiplayer", 1);
        PlayerPrefs.SetInt("FreeMode", 0);
        watchAdPanel.SetActive(true);
        //AdsManager.Instance.ShowMREC();-----
        // WatchRewarded();
        if (PlayerPrefs.GetInt("Multiplayer") == 1)
        {
            watchAdTextCount.text = "Watch Ad " + PlayerPrefs.GetInt("MultiplayerCount") + "/" + "3";
            coinText.text = "10000";
        }
    }

    public void OnClickWatchAdPanelFreeMode()
    {
        SoundManager.instance.PlayButtonClickSound();
        PlayerPrefs.SetInt("CareerMode", 0);
        PlayerPrefs.SetInt("Multiplayer", 0);
        PlayerPrefs.SetInt("FreeMode", 1);
        watchAdPanel.SetActive(true);
        //AdsManager.Instance.ShowMREC();
        if (PlayerPrefs.GetInt("FreeMode") == 1)
        {
            watchAdTextCount.text = "Watch Ad " + PlayerPrefs.GetInt("FreeModeCount") + "/" + "4";
            coinText.text = "30000";
        }
    }

    public void WatchRewarded()
    {
        AdsManager.Instance.ShowRewarded(() =>
        {
            if (PlayerPrefs.GetInt("Multiplayer") == 1)
            {
                if (PlayerPrefs.GetInt("MultiplayerCount") <= 3 && PlayerPrefs.GetInt("MultiplayerUnLock") == 0)
                {
                    PlayerPrefs.SetInt("MultiplayerCount", PlayerPrefs.GetInt("MultiplayerCount") + 1);
                    watchAdTextCount.text = "Watch Ad " + PlayerPrefs.GetInt("MultiplayerCount") + "/" + "3";
                    unLockMultiplayer();
                }
            }
            if (PlayerPrefs.GetInt("FreeMode") == 1)
            {
                if (PlayerPrefs.GetInt("FreeModeCount") <= 3 && PlayerPrefs.GetInt("FreeModeUnLock") == 0)
                {
                    PlayerPrefs.SetInt("FreeModeCount", PlayerPrefs.GetInt("FreeModeCount") + 1);
                    watchAdTextCount.text = "Watch Ad " + PlayerPrefs.GetInt("FreeModeCount") + "/" + "4";
                    unLockFreeMode();
                }
            }
        }, "Watch AD For Unlock Mode");
    }

    public void watchAdButton()
    {
        SoundManager.instance.PlayButtonClickSound();
        AdsManager.Instance.HideMREC();
        WatchRewarded();

        //if (PlayerPrefs.GetInt("Multiplayer") == 1)
        //{
        //    if (PlayerPrefs.GetInt("MultiplayerCount") <= 3 && PlayerPrefs.GetInt("MultiplayerUnLock") == 0)
        //    {
        //        PlayerPrefs.SetInt("MultiplayerCount", PlayerPrefs.GetInt("MultiplayerCount") + 1);
        //        watchAdTextCount.text = "Watch Ad " + PlayerPrefs.GetInt("MultiplayerCount") + "/" + "3";
        //        unLockMultiplayer();
        //    }
        //}
        //if (PlayerPrefs.GetInt("FreeMode") == 1)
        //{
        //    if (PlayerPrefs.GetInt("FreeModeCount") <= 3 && PlayerPrefs.GetInt("FreeModeUnLock") == 0)
        //    {
        //        PlayerPrefs.SetInt("FreeModeCount", PlayerPrefs.GetInt("FreeModeCount") + 1);
        //        watchAdTextCount.text = "Watch Ad " + PlayerPrefs.GetInt("FreeModeCount") + "/" + "4";
        //        unLockFreeMode();
        //    }
        //}
    }

    public void coinPurchaseButton()
    {
        SoundManager.instance.PlayButtonClickSound();
        if (PlayerPrefs.GetInt("Multiplayer") == 1)
        {
            if (PlayerPrefs.GetInt("MultiplayerCount") <= 3 && PlayerPrefs.GetInt("MultiplayerUnLock") == 0)
            {
                if (PlayerPrefs.GetInt("PlayerScore") >= 10000)
                {
                    PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") - 10000);
                    watchAdPanel.SetActive(false);
                    scoreText.text = PlayerPrefs.GetInt("PlayerScore").ToString();
                    PlayerPrefs.SetInt("MultiplayerUnLock", 1);
                    multiplayerLock.SetActive(false);
                    multiplayerBackImage.SetActive(false);
                    multiplayerWatchAdButton.SetActive(false);
                    MultiPlayerMode.transform.GetComponent<ActionManager>().enabled = true;
                } else
                {
                    LowCashSelection.SetActive(true);
                    //watchAdPanel.SetActive(false);
                }
                
            }
        }
        if (PlayerPrefs.GetInt("FreeMode") == 1)
        {
            if (PlayerPrefs.GetInt("FreeModeCount") <= 3 && PlayerPrefs.GetInt("FreeModeUnLock") == 0)
            {
                if (PlayerPrefs.GetInt("PlayerScore") >= 30000)
                {
                    PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") - 30000);
                    watchAdPanel.SetActive(false);
                    scoreText.text = PlayerPrefs.GetInt("PlayerScore").ToString();
                    PlayerPrefs.SetInt("FreeModeUnLock", 1);
                    freeModeLock.SetActive(false);
                    freeModeBackImage.SetActive(false);
                    freeModeWatchAdButton.SetActive(false);
                    FreeMode.transform.GetComponent<ActionManager>().enabled = true;
                }
                else
                {
                    LowCashSelection.SetActive(true);
                    //watchAdPanel.SetActive(false);
                }
            }
        }
    }

    void unLockMultiplayer()
    {
        SoundManager.instance.PlayButtonClickSound();
        if (PlayerPrefs.GetInt("MultiplayerCount") == 3)
        {
            watchAdPanel.SetActive(false);
            PlayerPrefs.SetInt("MultiplayerUnLock", 1);
            multiplayerLock.SetActive(false);
            multiplayerBackImage.SetActive(false);
            multiplayerWatchAdButton.SetActive(false);
            MultiPlayerMode.enabled = true;
            MultiPlayerMode.transform.GetComponent<ActionManager>().enabled = true;
        }
    }

    void unLockFreeMode()
    {
        SoundManager.instance.PlayButtonClickSound();
        if (PlayerPrefs.GetInt("FreeModeCount") == 4)
        {
            watchAdPanel.SetActive(false);
            PlayerPrefs.SetInt("FreeModeUnLock", 1);
            freeModeLock.SetActive(false);
            freeModeBackImage.SetActive(false);
            freeModeWatchAdButton.SetActive(false);
            FreeMode.enabled = true;
            FreeMode.transform.GetComponent<ActionManager>().enabled = true;
        }
    }

    public void lowCoin()
    {
        SoundManager.instance.PlayButtonClickSound();
        LowCashSelection.SetActive(true);
        //AdsManager.Instance.ShowMREC();
    }

    public void OnButtonPress()
    {
        AudioSource bgMusic = SoundManager.instance.backgroundLoop.GetComponent<AudioSource>();
        if (muted == false)
        {
            muted = true;
            bgMusic.Pause();
            onoffText.text = "Off";
            SoundManager.instance.PlayButtonClickSound();
            //AudioListener.pause = true;
        }
        else
        {
            muted = false;
            bgMusic.Play();
            onoffText.text = "On";
            SoundManager.instance.PlayButtonClickSound();
            //AudioListener.pause = false;
        }

        Save();
        UpdateButtonIcon();
    }

    private void UpdateButtonIcon()
    {
        if (muted == false)
        {
            soundon.enabled = true;
            soundoff.enabled = false;
            onoffText.text = "On";
        }
        else
        {
            soundon.enabled = false;
            soundoff.enabled = true;
            onoffText.text = "Off";
        }
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;

        // Update the audio state based on the loaded mute state
        AudioSource bgMusic = SoundManager.instance.backgroundLoop.GetComponent<AudioSource>();
        if (muted)
        {
            bgMusic.Pause();
        }
        else
        {
            bgMusic.Play();
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    public void setting_popup()
    {
        SoundManager.instance.PlayButtonClickSound();
        settingPopup.SetActive(true);
        //AdsManager.Instance.ShowMREC();
    }

    public void getCoinWatchAd()
    {
        SoundManager.instance.PlayButtonClickSound();
        AdsManager.Instance.ShowRewarded(() =>
        {
            PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 1000);
            scoreText.text = PlayerPrefs.GetInt("PlayerScore").ToString();
            LowCashSelection.SetActive(false);
        }, "Coins 1000 Added");

        //PlayerPrefs.SetInt("PlayerScore", PlayerPrefs.GetInt("PlayerScore") + 1000);
        //scoreText.text = PlayerPrefs.GetInt("PlayerScore").ToString();
        //LowCashSelection.SetActive(false);
    }

    public void sound()
    {
        SoundManager.instance.PlayButtonClickSound();
    }

    public void soundController()
    {
        if (PlayerPrefs.GetInt("SoundController") == 0)
        {
            PlayerPrefs.SetInt("SoundController", 1);
            soundBtn.transform.gameObject.GetComponent<Image>().sprite = onSprite;
        }
        else
        {
            PlayerPrefs.SetInt("SoundController", 0);
            soundBtn.transform.gameObject.GetComponent<Image>().sprite = offSprite;
        }
        SoundManager.instance.PlayButtonClickSound();
    }

    #endregion
}
