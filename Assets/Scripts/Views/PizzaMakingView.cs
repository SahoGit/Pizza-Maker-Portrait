using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Notionhub;

public class PizzaMakingView : MonoBehaviour {
	#region Variables, Constants & Initializers
	private bool beatingFlag = true;
	private bool progressFlag = true;
	private bool doughFlag = true;
	public GameObject progressBar;
	int index = -1;
	private int counter = 0;
	public Image powerImage;
	public RectTransform progressBarEndPoint, progressBarStartPoint;

	public GameObject mainBowl;
	public RectTransform bowlTableEndPoint, mainBowlOutsidePoint;
	public GameObject flour;
	public Image bowlFlour;
	public Sprite flourOpenPack;
	public RectTransform flourStartPosition, flourPackEndPoint, flourSecondPosition, flourMovingPosition;

	public GameObject milkPack;
	public RectTransform milkPackStartPoint, milkPackEndPoint, milkPackSecondPoint, milkPackMovingPoint; 
	public Sprite openMilkPack;
	public Image bowlMilk;

	public GameObject yeastPack;
	public RectTransform yeastPackStartPoint, yeastPackEndPoint, yeastPackSecondPoint, yeastPackMovingPoint; 
	public Sprite openYeastPack;
	public Image bowlYeast;

	public GameObject jug;
	public RectTransform jugStartPoint, jugEndPoint, jugSecondPoint, jugMovingPoint; 
	public Image waterLiquid;

	public GameObject oilBottle;
	public RectTransform oilBottleStartPoint, oilBottleEndPoint, oilBottleSecondPoint, oilBottleMovingPoint; 
	public Image oilLiquid;

	public GameObject egg;
	public RectTransform eggTableEndPoint, eggEndPoint, eggUpperPoint1;
	public Sprite eggCrack, eggBreak; 
	public GameObject eggHand;
	public RectTransform liquidEggEndPoint;

	public GameObject sugar;
	public RectTransform sugarPackStartPoint, sugarPackEndPoint, sugarPackSecondPoint, sugarPackMovingPoint; 
	public Sprite openSugarPack;
	public Image bowlSugar;


	public GameObject saltBottle;
	public RectTransform saltBottleStartPoint, saltBottleEndPoint, saltBottleSecondPoint, saltBottleMovingPoint; 
	public Image bowlSalt;

	public GameObject bowlItems;
	public Image mixture;
	public Sprite[] shakeMixtures;
	public GameObject beater;
	public RectTransform beaterEndPoint, beaterStartPoint;

	public Image blackScreen;

	public GameObject doughParent;
	public GameObject doughHand;
	public RectTransform doughHandEndPoint;
    public GameObject pointingHand;
    public RectTransform pointingHandEndPoint;
    public GameObject RollingPinActive;
	public Image smallDough, largeDough;
	public RectTransform rollingPinSidePoint;


	public RectTransform mainBowlLeftPoint;
	public GameObject Next;
    public GameObject Next_2;
    public GameObject goodJobScreen;
	public GameObject GoodJobText;
	public RectTransform GoodJobTextEndPoint;
	public GameObject NiceScreen;
	public GameObject NiceText;
	public RectTransform NiceTextEndPoint;
	public GameObject NiceScreen1;
	public GameObject NiceText1;
	public RectTransform NiceText1EndPoint;
	public GameObject NiceScreen2;
	public GameObject NiceText2;
	public RectTransform NiceText2EndPoint;

	public GameObject LoadinBg;
	public Image LoadingFilled;
	public GameObject fireworks;
    public GameObject TaskCompleteScreen;

    #endregion

    #region Lifecycle Methods
    void Start () {
		ShowAd ();
		GameManager.instance.currentScene = GameUtils.PIZZAMAKING_VIEW;
		Invoke ("SetViewContents", 0.1f);
  
    }

	// Update is called once per frame
	void Update () {

	}

	#endregion

	#region Utility Methods

	private void SetViewContents(){
		bowlComes ();
		//MainBowlGoesOut();
        //BeaterComesInn();
        //StartCoroutine (BeatingMixture ());
    }

	private void ShowAd(){
        //AdsSDKManager ads = GameObject.FindObjectOfType<AdsSDKManager>();
        //if (ads != null)
        //{
        //    ads.ShowAdd();
        //}
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

	private void MoveProgressBarComesInn(){
		powerImage.fillAmount = 0;
		MoveAction (progressBar,progressBarEndPoint,0.5f,iTween.EaseType.easeInOutBounce,iTween.LoopType.none);
	}

	private void MoveProgressBarGoesOut(){
		MoveAction (progressBar,progressBarStartPoint,0.5f,iTween.EaseType.easeInOutBack,iTween.LoopType.none);
	}

	private void colorIncreases(Image img, float val){
		if (img.color.a < 1) {
			img.color = new Vector4 (img.color.r,img.color.g,img.color.b, img.color.a + val);
		}
	}

	private void colorDecreases(Image img , float value){
		if (img.color.a > 0) {
			img.color = new Vector4 (img.color.r,img.color.g,img.color.b, img.color.a - value);
		}
	}
		
	private void BeaterShakeAction(GameObject beater) {
		Hashtable tweenParams = new Hashtable();
		tweenParams.Add ("amount", new Vector3 (0.02f, 0.02f, 0.02f));
		tweenParams.Add ("time", 1.0f);
		tweenParams.Add ("easetype", iTween.EaseType.easeInCubic);
		tweenParams.Add ("looptype", iTween.LoopType.pingPong);
		iTween.ShakePosition(beater, tweenParams);
	}

    private void PointingHandActive()
    {
        pointingHand.SetActive(true);
        MoveAction(pointingHand, pointingHandEndPoint, 0.9f, iTween.EaseType.linear, iTween.LoopType.loop);
    }
    private void PointingHandInactive()
    {
        pointingHand.SetActive(false);
    }

    private void bowlComes(){
		SoundManager.instance.PlaySwooshSound ();
		MoveAction (mainBowl, bowlTableEndPoint, 0.5f, iTween.EaseType.linear,iTween.LoopType.none);
		Invoke("FlourComesInn", 0.5f);
	}

	private void FlourComesInn(){
        //PointingHandActive();
		Invoke("PointingHandActive", 0.8f);
        SoundManager.instance.PlaySwooshSound();
        MoveAction (flour, flourPackEndPoint, 0.5f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
		Invoke("FlourPackActive", 0.5f);
	}

	private void FlourFinishTask()
	{
        SoundManager.instance.PlaySwooshSound();
        MoveAction(flour, flourPackEndPoint, 0.5f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
        flour.GetComponent<ActionManager>().enabled = false;
        flour.GetComponent<ApplicatorListener>().enabled = false;
		Invoke("FlourGoesOutside", 0.5f);
    }

	private void FlourGoesOutside()
	{
        SoundManager.instance.PlaySwooshSound();
        MoveAction(flour, flourStartPosition, 0.5f, iTween.EaseType.easeInBack, iTween.LoopType.none);
        Invoke("MilkBottleComesInn", 0.5f);
    }

	private void FlourPackActive(){
		flour.GetComponent<ActionManager> ().enabled = true;
		flour.GetComponent<BoxCollider2D> ().enabled = true;
		flour.GetComponent<ApplicatorListener> ().enabled = true;
	}

	private void floupUperPoint(){
		flour.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, -70f);
		Invoke ("flourFilling", 0.3f);
	}

	private void flourFilling(){
		iTween.Resume (flour);
		flour.transform.GetChild (0).gameObject.SetActive (true);
		SoundManager.instance.PlayFlourLoop (true);
		MoveAction (flour, flourMovingPosition, 0.5f, iTween.EaseType.linear, iTween.LoopType.pingPong);
		StartCoroutine (FadeIntAction(bowlFlour));
		Invoke ("flourStop", 4.0f);
	}

	private void flourStop(){
		flour.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, 0f);
		flour.transform.GetChild (0).gameObject.SetActive (false);
		SoundManager.instance.PlayFlourLoop (false);
		iTween.Stop (flour);
		MoveAction (flour, flourSecondPosition, 0.3f, iTween.EaseType.linear, iTween.LoopType.none);
		//Invoke ("flourDisappear",0.3f);
		Invoke("FlourFinishTask", 0.5f);
       // milkPack.GetComponent<BoxCollider2D>().enabled = true;
    }

	private void flourDisappear(){
		StartCoroutine (FadeOutAction(flour.gameObject.GetComponent<Image>()));
		Invoke ("MilkBottleComesInn", 0.5f);
	}

	private void MilkBottleComesInn(){
		milkPack.SetActive (true);
        MoveAction (milkPack, milkPackEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        Invoke ("MilkPackActive", 0.5f);
       
    }

    private void MilkFinishTask()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(milkPack, milkPackEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        flour.GetComponent<ActionManager>().enabled = false;
        flour.GetComponent<ApplicatorListener>().enabled = false;
        Invoke("MilkGoesOutside", 0.5f);
    }

    private void MilkGoesOutside()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(milkPack, milkPackStartPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        Invoke("YeastPackComesInn", 0.5f);
    }


	private void MilkPackActive()
	{
		milkPack.GetComponent<ActionManager>().enabled = true;
		milkPack.GetComponent<BoxCollider2D>().enabled = true;
		milkPack.GetComponent<ApplicatorListener>().enabled = true;
		milkPackSecondPoint.GetComponent<BoxCollider2D>().enabled = true;
	}

	private void MilkPackUperPoint(){
		milkPack.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, -70f);
		milkPack.transform.GetComponent<Image> ().sprite = openMilkPack;
		Invoke ("MilkFilling", 0.3f);
	}

	private void MilkFilling(){
		SoundManager.instance.PlayPourLoop (true);
		milkPack.transform.GetChild (0).gameObject.SetActive (true);
		MoveAction (milkPack, milkPackMovingPoint, 1.0f, iTween.EaseType.linear, iTween.LoopType.pingPong);
		StartCoroutine (FillAction(bowlMilk));
		Invoke ("MilkStop", 4.0f);
	}

	private void MilkStop(){
		SoundManager.instance.PlayPourLoop (false);
		milkPack.transform.GetChild (0).gameObject.SetActive (false);
		milkPack.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, 0f);
		iTween.Stop (milkPack);
		MoveAction (milkPack, milkPackMovingPoint, 0.3f, iTween.EaseType.linear, iTween.LoopType.none);
		//Invoke ("MilkPackDisappear",0.3f);
		Invoke("MilkFinishTask", 0.5f);
	}

	private void MilkPackDisappear(){
		StartCoroutine (FadeOutAction(milkPack.gameObject.GetComponent<Image>()));
		Invoke ("YeastPackComesInn", 0.5f);
	}

	private void YeastPackComesInn(){
		yeastPack.SetActive (true);
		MoveAction (yeastPack, yeastPackEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("YeastPackActive", 0.5f);
	}

    private void YeastPackFinishTask()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(yeastPack, yeastPackEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        flour.GetComponent<ActionManager>().enabled = false;
        flour.GetComponent<ApplicatorListener>().enabled = false;
        Invoke("YeastPackGoesOutside", 0.5f);
    }

    private void YeastPackGoesOutside()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(yeastPack, yeastPackStartPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        Invoke("jugComesInn", 0.5f);
    }

    private void YeastPackActive(){
		yeastPack.GetComponent<ActionManager> ().enabled = true;
		yeastPack.GetComponent<BoxCollider2D> ().enabled = true;
		yeastPack.GetComponent<ApplicatorListener> ().enabled = true;
		yeastPackSecondPoint.GetComponent<BoxCollider2D>().enabled = true;
    }

	private void YeastPackUperPoint(){
		yeastPack.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, -70f);
		yeastPack.transform.GetComponent<Image> ().sprite = openYeastPack;
		Invoke ("YeastFilling", 0.3f);
	}

	private void YeastFilling(){
		SoundManager.instance.PlayFlourLoop (true);
		yeastPack.transform.GetChild (0).gameObject.SetActive (true);
		MoveAction (yeastPack, yeastPackMovingPoint, 0.8f, iTween.EaseType.linear, iTween.LoopType.pingPong);
		StartCoroutine (FadeIntAction(bowlYeast));
		Invoke ("YeastStop", 4.0f);
	}

	private void YeastStop(){
		SoundManager.instance.PlayFlourLoop (false);
		yeastPack.transform.GetChild (0).gameObject.SetActive (false);
		yeastPack.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, 0f);
		iTween.Stop (yeastPack);
		MoveAction (yeastPack, yeastPackSecondPoint, 0.3f, iTween.EaseType.linear, iTween.LoopType.none);
		//Invoke ("YeastPackDisappear",0.3f);
		Invoke("YeastPackFinishTask", 0.5f);
	}

	private void YeastPackDisappear(){
		StartCoroutine (FadeOutAction(yeastPack.gameObject.GetComponent<Image>()));
		Invoke ("jugComesInn", 0.5f);
	}

	private void jugComesInn(){
		jug.SetActive (true);
		MoveAction (jug, jugEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("WaterJugActive", 0.5f);
	}

    private void jugFinishTask()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(jug, jugEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        flour.GetComponent<ActionManager>().enabled = false;
        flour.GetComponent<ApplicatorListener>().enabled = false;
        Invoke("jugGoesOutside", 0.5f);
    }

    private void jugGoesOutside()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(jug, jugStartPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        Invoke("NiceScreen2Active", 0.5f);
    }


    bool JugIsInAir = false;
	private void WaterJugActive(){
		jug.GetComponent<ActionManager> ().enabled = true;
		jug.GetComponent<BoxCollider2D> ().enabled = true;
		jug.GetComponent<ApplicatorListener> ().enabled = true;
		jugSecondPoint.GetComponent<BoxCollider2D>().enabled = true;
		JugIsInAir = false;
	}

	private void JugUpperPoint(){
		SoundManager.instance.PlayPourLoop (true);
		jug.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, -70f);
		jug.transform.GetChild (0).gameObject.SetActive (true);
		MoveAction (jug, jugMovingPoint, 0.8f, iTween.EaseType.linear, iTween.LoopType.pingPong);
		Invoke ("WaterFilling", 0.4f);
	}

	private void WaterFilling(){
		StartCoroutine (FillAction(waterLiquid));
		Invoke ("WaterStop", 4.0f);
	}

	private void WaterStop(){
		SoundManager.instance.PlayPourLoop (false);
		jug.transform.GetChild (0).gameObject.SetActive (false);
		jug.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, 0f);
		iTween.Stop (jug);
		MoveAction (jug, jugSecondPoint, 0.3f, iTween.EaseType.linear, iTween.LoopType.none);
		//Invoke ("JugDisappear", 0.3f);
		Invoke ("jugFinishTask", 0.3f);
	}

	private void JugDisappear(){
		StartCoroutine (FadeOutAction(jug.gameObject.GetComponent<Image>()));
		Invoke ("NiceScreen2Active", 0.5f);
	
	}

	private void NiceScreen2Active(){
		SoundManager.instance.PlayActionSound ();
		NiceScreen2.SetActive (true);
		MoveAction (NiceText2, NiceText2EndPoint, 0.5f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
		Invoke ("oilBottleComesInn", 2.0f);
	}


	private void oilBottleComesInn(){
		NiceScreen2.SetActive (false);
		oilBottle.SetActive (true);
		MoveAction (oilBottle, oilBottleEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        oilBottle.GetComponent<ActionManager>().enabled = true;
        oilBottle.GetComponent<BoxCollider2D>().enabled = true;
        Invoke ("oilBottleActive", 0.5f);
	}

    private void oilBottleFinishTask()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(oilBottle, oilBottleEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        flour.GetComponent<ActionManager>().enabled = false;
        flour.GetComponent<ApplicatorListener>().enabled = false;
        Invoke("oilBottleGoesOutside", 0.5f);
    }

    private void oilBottleGoesOutside()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(oilBottle, oilBottleStartPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        Invoke("SugarPackComesInn", 0.5f);
    }

    private void oilBottleActive(){
		oilBottle.GetComponent<ApplicatorListener> ().enabled = true;
        oilBottleSecondPoint.GetComponent<BoxCollider2D> ().enabled = true;

    }

	private void oilBottleUpperPoint(){
		SoundManager.instance.PlayPourLoop (true);
		oilBottle.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, -70f);
		oilBottle.transform.GetChild (0).gameObject.SetActive (true);
		Invoke ("OilFilling", 0.4f);
	}

	private void OilFilling(){
		StartCoroutine (FillAction(oilLiquid));
		Invoke ("OilStop", 4.0f);
	}

	private void OilStop(){
		SoundManager.instance.PlayPourLoop (false);
		oilBottle.transform.GetChild (0).gameObject.SetActive (false);
		oilBottle.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, 0f);
		//Invoke ("oilBottleDisappear",0.3f);
		Invoke ("oilBottleFinishTask", 0.3f);
	}

	private void oilBottleDisappear(){
		StartCoroutine (FadeOutAction(oilBottle.gameObject.GetComponent<Image>()));
		Invoke ("SugarPackComesInn", 0.5f);
	}

	private void EggComesInn(){
		NiceScreen.SetActive (false);
		egg.gameObject.SetActive (true);
        MoveAction (egg, eggTableEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        egg.GetComponent<BoxCollider2D>().enabled = true;
        Invoke ("EggActive", 0.5f);
    }

	private void EggActive(){
		egg.GetComponent<ActionManager> ().enabled = true;
		egg.GetComponent<BoxCollider2D> ().enabled = true;
		egg.GetComponent<ApplicatorListener> ().enabled = true;
		eggEndPoint.GetComponent<BoxCollider2D>().enabled = true;
    }

	private void eggHandActive(){
		eggHand.SetActive (true);
		egg.GetComponent<Button> ().enabled = true;
	}

	private void EggUperPoint(){
		MoveAction (egg, eggUpperPoint1, 0.3f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
		Invoke ("eggBecomeBroken", 0.3f);
	}

	private void eggBecomeBroken(){
		SoundManager.instance.PlayEggCrackSound ();
		egg.GetComponent<Image> ().sprite = eggBreak;
		egg.transform.GetChild (0).gameObject.SetActive (true);
		MoveAction (egg.transform.GetChild(0).gameObject, liquidEggEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("liquidEggActive", 0.5f);
	}


	private void liquidEggActive(){
		SoundManager.instance.PlayItemPlacingSound ();
		egg.transform.GetChild (0).gameObject.SetActive (false);
		liquidEggEndPoint.gameObject.SetActive (true);
		Invoke ("EggDisappear", 0.5f);
	}

	private void EggDisappear(){
		StartCoroutine (FadeOutAction(egg.gameObject.GetComponent<Image>()));
		Invoke ("SaltBottleComesInn", 0.5f);

	}

	private void SugarPackComesInn(){
		sugar.SetActive (true);
		MoveAction (sugar, sugarPackEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("SugarPackActive", 0.5f);
	}

    private void SugarPackFinishTask()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(sugar, sugarPackEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        flour.GetComponent<ActionManager>().enabled = false;
        flour.GetComponent<ApplicatorListener>().enabled = false;
        Invoke("SugarPackGoesOutside", 0.5f);
    }

    private void SugarPackGoesOutside()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(sugar, sugarPackStartPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        Invoke("NiceScreenActive", 0.5f);
    }

    private void SugarPackActive(){
		sugar.GetComponent<ActionManager> ().enabled = true;
		sugar.GetComponent<BoxCollider2D> ().enabled = true;
		sugar.GetComponent<ApplicatorListener> ().enabled = true;
        sugarPackSecondPoint.GetComponent<BoxCollider2D>().enabled = true;

    }

	private void SugarPackUpperPoint(){
		sugar.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, -70f);
		sugar.transform.GetComponent<Image> ().sprite = openSugarPack;
		Invoke ("SugarFilling", 0.3f);
	}

	private void SugarFilling(){
		SoundManager.instance.PlayFlourLoop (true);
		sugar.transform.GetChild (0).gameObject.SetActive (true);
		MoveAction (sugar, sugarPackMovingPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.pingPong);
		StartCoroutine (FadeIntAction(bowlSugar));
		Invoke ("SugarStop", 4.0f);
	}

	private void SugarStop(){
		SoundManager.instance.PlayFlourLoop (false);
		sugar.transform.GetChild (0).gameObject.SetActive (false);
		sugar.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, 0f);
		iTween.Stop (sugar);
		MoveAction (sugar, sugarPackSecondPoint, 0.3f, iTween.EaseType.linear, iTween.LoopType.none);
		//Invoke ("SugarDisappear",0.3f);
		Invoke ("SugarPackFinishTask", 0.3f);
	}

	private void SugarDisappear(){
		StartCoroutine (FadeOutAction(sugar.gameObject.GetComponent<Image>()));
		Invoke ("NiceScreenActive", 0.5f);
	}

	private void NiceScreenActive(){
		SoundManager.instance.PlayActionSound ();
		NiceScreen.SetActive (true);
		MoveAction (NiceText, NiceTextEndPoint, 0.5f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
		Invoke ("EggComesInn", 2.0f);
	}

	private void SaltBottleComesInn(){
		saltBottle.SetActive (true);
		MoveAction (saltBottle, saltBottleEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("SaltBottleActive", 0.5f);
	}

    private void SaltBottleFinishTask()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(saltBottle, saltBottleEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        flour.GetComponent<ActionManager>().enabled = false;
        flour.GetComponent<ApplicatorListener>().enabled = false;
        Invoke("SaltBottleGoesOutside", 0.5f);
    }

    private void SaltBottleGoesOutside()
    {
        SoundManager.instance.PlaySwooshSound();
        MoveAction(saltBottle, saltBottleStartPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
        Invoke("NiceScreen1Active", 0.5f);
    }

    private void SaltBottleActive(){
		saltBottle.GetComponent<ActionManager> ().enabled = true;
		saltBottle.GetComponent<BoxCollider2D> ().enabled = true;
		saltBottle.GetComponent<ApplicatorListener> ().enabled = true;
        saltBottleSecondPoint.GetComponent<BoxCollider2D>().enabled = true;

    }

	private void SaltBottleUpperPoint(){
		saltBottle.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, -140f);
		Invoke ("SaltFilling", 0.3f);
	}

	private void SaltFilling(){
		SoundManager.instance.PlaySaltLoop (true);
		saltBottle.transform.GetChild (0).gameObject.SetActive (true);
		MoveAction (saltBottle, saltBottleMovingPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.pingPong);
		StartCoroutine (FadeIntAction(bowlSalt));
		Invoke ("SaltBottleStop", 4.0f);
	}

	private void SaltBottleStop(){
		SoundManager.instance.PlaySaltLoop (false);
		saltBottle.transform.GetChild (0).gameObject.SetActive (false);
		saltBottle.gameObject.GetComponent<RectTransform> ().eulerAngles = new Vector3 (0f, 0f, 0f);
		iTween.Stop (saltBottle);
		MoveAction (saltBottle, saltBottleSecondPoint, 0.3f, iTween.EaseType.linear, iTween.LoopType.none);
		//Invoke ("SaltBottleDisappear",0.3f);
		Invoke ("SaltBottleFinishTask", 0.3f);
	}

	//private void SaltBottleDisappear(){
	//	StartCoroutine (FadeOutAction(saltBottle.gameObject.GetComponent<Image>()));
	//	Invoke ("NiceScreen1Active", 0.5f);
	//}

	private void NiceScreen1Active(){
		SoundManager.instance.PlayActionSound ();
		NiceScreen1.SetActive (true);
		MoveAction (NiceText1, NiceText1EndPoint, 0.5f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
		Invoke ("mixtureActive", 2.0f);
	}

	private void mixtureActive(){
		NiceScreen1.SetActive (false);
        mixture.GetComponent<BoxCollider2D> ().enabled = true;

        StartCoroutine (FadeIntAction (mixture));
		Invoke ("BeaterComesInn", 2.0f);
	}



	private void BeaterComesInn(){
		bowlItems.SetActive (false);
		SoundManager.instance.PlaySwooshSound ();
		beater.SetActive (true);
		MoveAction (beater, beaterEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke("BeaterListenerOn", 0.5f);
	}

	private void BeaterListenerOn(){
		beater.GetComponent<BoxCollider2D> ().enabled = true;
		beater.GetComponent<ApplicatorListener> ().enabled = true;
	}

	private void MainBowlGoesOut(){
		MoveAction (mainBowl, mainBowlLeftPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("blackScreenActive", 1.0f);
	
	}

	private void blackScreenActive(){
		blackScreen.gameObject.SetActive (true);
		doughParent.SetActive (true);
		StartCoroutine (FadeOutAction(blackScreen));
		Invoke ("DoughHandActive", 2.0f);
	}

	private void DoughHandActive(){
		blackScreen.gameObject.SetActive (false);
		doughHand.SetActive (true);
		MoveAction (doughHand, doughHandEndPoint, 1.0f, iTween.EaseType.linear, iTween.LoopType.loop);
	}

	#endregion

	#region Callback Methods

	public void flourBeginDrag(){
		PointingHandInactive();
        flour.GetComponent<ActionManager> ().enabled = false;
		iTween.Stop (flour);
	}

	public void OnCollisionOfFlourPack(){
		MoveAction (flour, flourSecondPosition, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("floupUperPoint", 0.5f);

	}

	public void MilkPackBeginDrag(){
		milkPack.GetComponent<ActionManager> ().enabled = false;
		iTween.Stop (milkPack);
	}


	public void OnCollisionMilkPack(){
		MoveAction (milkPack, milkPackSecondPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("MilkPackUperPoint", 0.5f);

	}

	public void YeastPackBeginDrag(){
		yeastPack.GetComponent<ActionManager> ().enabled = false;
		iTween.Stop (yeastPack);
	}


	public void OnCollisionYeastPack(){
		MoveAction (yeastPack, yeastPackSecondPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("YeastPackUperPoint", 0.5f);

	}

	public void OilBottleBeginDrag(){
		oilBottle.GetComponent<ActionManager> ().enabled = false;
		iTween.Stop (oilBottle);
	}

	public void OnCollisionoilBottle(){
		MoveAction (oilBottle, oilBottleSecondPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("oilBottleUpperPoint", 0.5f);

	}

	public void jugBeginDrag(){
		jug.GetComponent<ActionManager> ().enabled = false;
		iTween.Stop (jug);
		
        // if (JugIsInAir == false)
        //  {
		//	   JugIsInAir = true;
		//	   Invoke("OnCollisionWaterJug", 1f);
		//  }
	}

	public void OnCollisionWaterJug(){
		MoveAction (jug, jugSecondPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("JugUpperPoint", 0.5f);
	}

	public void EggBeginDrag(){
		egg.GetComponent<ActionManager> ().enabled = false;
		iTween.Stop (egg);
	}

	public void OnCollisionOfEgg(){
		MoveAction (egg, eggEndPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("eggHandActive", 0.5f);
	}

	public void OnClickEgg(){
		eggHand.SetActive (false);
		egg.GetComponent<Image> ().sprite = eggCrack;
		egg.GetComponent<Button> ().enabled = false;
		SoundManager.instance.PlayPingSound ();
		Invoke ("EggUperPoint", 0.3f);
	}

	public void SugarBeginDrag(){
		sugar.GetComponent<ActionManager> ().enabled = false;
		iTween.Stop (sugar);
	}

	public void OnCollisionOfSugarPack(){
		MoveAction (sugar, sugarPackSecondPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("SugarPackUpperPoint", 0.5f);

	}

	public void SaltBottleBeginDrag(){
		saltBottle.GetComponent<ActionManager> ().enabled = false;
		iTween.Stop (saltBottle);
	}

	public void OnCollisionOfSaltBottle(){
		MoveAction (saltBottle, saltBottleSecondPoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
		Invoke ("SaltBottleUpperPoint", 0.5f);

	}



	public void OnCollisionWithBeater(){
		print ("stay");
		if (progressFlag) {
			mixture.gameObject.GetComponent<Image> ().enabled = true;
			bowlItems.gameObject.SetActive (false);
			MoveProgressBarComesInn ();
			progressFlag = false;
		}

//		iTween.Resume (beater);
	//	BeaterShakeAction (beater);
		SoundManager.instance.PlayBeatingLoop(true);
		beatingFlag = true;
		StartCoroutine (BeatingMixture ());
		powerImage.fillAmount = powerImage.fillAmount + 0.004f;
		if (powerImage.fillAmount >= 1f) {
			SoundManager.instance.PlayBeatingLoop(false);
			beatingFlag = false;
			beater.GetComponent<ApplicatorListener> ().enabled = false;
			beater.GetComponent<BoxCollider2D> ().enabled = false;
			StartCoroutine (FadeOutAction(beater.GetComponent<Image>()));
			mixture.gameObject.GetComponent<Image> ().sprite = shakeMixtures[4];
			MoveProgressBarGoesOut ();
			Invoke ("MainBowlGoesOut", 1.0f);
		}
	}

	public void OnExitCollisionWithBeater(){
		print ("exit");
		SoundManager.instance.PlayBeatingLoop(false);
		beatingFlag = false;
		//iTween.Pause (beater);
		StopCoroutine (BeatingMixture());
	}

    public void OnCollisionWithRollingPins(){
		doughHand.SetActive (false);
		RollingPinActive.GetComponent<ApplicatorListener> ().enabled = true;
		Debug.Log("collison work or not");
	}

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "smallDough")
        {
            // Check if the other object has the tag "RollingPinActive"
            if (collision.gameObject.CompareTag("RollingPinActive"))
            {
                // Do something when "smallDough" collides with an object tagged "RollingPinActive"
                Debug.Log("smallDough collided with RollingPinActive");

                // You can add your own code or actions here
            }
        }
    }


    public void DoughRollingBeginDrag()
	{
		if (doughFlag)
		{
			Debug.Log("ProgressBar Active");
			MoveProgressBarComesInn();
			doughFlag = false;
		}

	}

	public void OnDragDoughRolling(){
		//Debug.Log(ApplicatorListener.instance.stopFlag);
		print ("come there");
		if (doughFlag)
		{
			MoveProgressBarComesInn();
			doughFlag = false;
			Debug.Log("check current doughFlag ");
			RollingPinActive.GetComponent<ApplicatorListener>().enabled = true;
		}
		SoundManager.instance.PlayRollingLoop (true);
        doughHand.SetActive(false);
		
        if (GameManager.instance.currentItem == "smallDough" && GameManager.instance.stopFlag)
		{
			Debug.Log("check current item is smallDoug ?" + smallDough);
            powerImage.fillAmount = powerImage.fillAmount + 0.005f;
			colorIncreases(largeDough, 0.005f);
			colorDecreases(smallDough, 0.005f);
			if (powerImage.fillAmount >= 1.0f)
			{
				SoundManager.instance.PlayRollingLoop(false);
				MoveProgressBarGoesOut();
				RollingPinActive.GetComponent<ApplicatorListener>().enabled = false;
				MoveAction(RollingPinActive, rollingPinSidePoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
				RollingPinActive.GetComponent<RectTransform>().eulerAngles = new Vector3(0f, 0f, 0f);
				Invoke("GoodJobScreenActive", 1.0f);
			}

		}

		///-----------second opeiotn -----///
		//powerImage.fillAmount = powerImage.fillAmount + 0.005f;
  //      colorIncreases(largeDough, 0.005f);
  //      colorDecreases(smallDough, 0.005f);
  //      if (powerImage.fillAmount >= 1.0f)
  //      {
  //          SoundManager.instance.PlayRollingLoop(false);
  //          MoveProgressBarGoesOut();
  //          RollingPinActive.GetComponent<ApplicatorListener>().enabled = false;
  //          MoveAction(RollingPinActive, rollingPinSidePoint, 0.5f, iTween.EaseType.linear, iTween.LoopType.none);
  //          RollingPinActive.GetComponent<RectTransform>().eulerAngles = new Vector3(0f, 0f, 0f);
  //          Invoke("GoodJobScreenActive", 1.0f);
  //      }
  //      if (Input.mousePosition.y > 1200.0f && Input.mousePosition.y < 2000.0f)
		//{
			

		//}

	}

	public void OnClickNext(){
		Next.SetActive (false);
		//fireworks.SetActive (false);
		SoundManager.instance.PlayButtonClickSound ();
        //TaskCompleteScreen.SetActive(true);
        //LoadingBgActive ();
    }
    public void onClickTaskCompleteNext()
    {
        AdsManager.Instance.HideMREC();
        fireworks.SetActive(false);
        TaskCompleteScreen.SetActive(false);
        SoundManager.instance.PlayButtonClickSound();
		WhichPizzaPrepared(GameManager.instance.SelectedPizzaFlavour);
		//LoadingBgActive();
	}

    private void GoodJobScreenActive(){
		goodJobScreen.SetActive (true);
		SoundManager.instance.PlayActionSound ();
        MoveAction (GoodJobText, GoodJobTextEndPoint, 0.5f, iTween.EaseType.easeInBounce, iTween.LoopType.none);
        //Invoke ("NextActive", 2.0f);
        Invoke("fireWorkActive", 2.0f);
    }

    private void TaskCompleteScreenActive()
    {
        //fireworks.SetActive(false);
        TaskCompleteScreen.SetActive(true);
        AdsManager.Instance.ShowMREC();
    }
    private void fireWorkActive()
    {
        goodJobScreen.SetActive(false);
        fireworks.SetActive(true);
        Invoke("TaskCompleteScreenActive", 2.0f);
    }

    private void NextActive(){
		//fireworks.SetActive (true);
		SoundManager.instance.PlayLevelCompletedSound ();
		goodJobScreen.SetActive (false);
		Invoke("NextBtnDelay", 2f);
	}

	void NextBtnDelay()
	{
		Next.SetActive(true);// add delay 5 secs
	}

	private void LoadingBgActive(){
		LoadinBg.SetActive (true);
		StartCoroutine (FillAction(LoadingFilled));
		Invoke ("LoadingFull", 4.0f);
		Invoke("callAds", 1.0f);
	}

	void callAds()
	{
        //MyAdsManager.instance.CallInterstitialAd(Adspref.GamePause);
        AdsManager.Instance.ShowInterstitial("Show AD on Loading Screen");
    }

	private void LoadingFull(){
		WhichPizzaPrepared(GameManager.instance.SelectedPizzaFlavour);
	}

	private void WhichPizzaPrepared(int tag){
		switch (tag) {
		case 0:   //BBQ & Veggie
			NavigationManager.instance.ReplaceScene (GameScene.BBQANDVEGGIECUTTINGVIEW);
			break;

		case 1:     //Fish & Veggie
			NavigationManager.instance.ReplaceScene (GameScene.FISHANDVEGGIECUTTINGVIEW);
			break;

		case 2:     //Pepperoni & Chilli
			NavigationManager.instance.ReplaceScene (GameScene.PEPPERONICHILLIVIEW);
			break;

		case 3:   //Bacon Lovers
			NavigationManager.instance.ReplaceScene (GameScene.BACONLOVERSVIEW);
			break;

		case 4:    //Primo Meats
			NavigationManager.instance.ReplaceScene (GameScene.PRIMOMEATVIEW);
			break;

		case 5: //Create Your Own
			NavigationManager.instance.ReplaceScene (GameScene.CREATEYOUROWNVIEW);
			break;

            case 6: //Beef & Veggie
                NavigationManager.instance.ReplaceScene(GameScene.BEEFANDVEGGIEVIEW);
                break;

            case 7: //Lobster Sauce
			NavigationManager.instance.ReplaceScene (GameScene.LOBSTERSAUCEVIEW);
			break;

            case 8: //Beef & Veggie
                NavigationManager.instance.ReplaceScene(GameScene.MULTIPLAYERVIEW);
                break;
        }

	}


	#endregion

	#region Coroutine Methods
	IEnumerator FillAction (Image img){
		if (img.fillAmount < 1) {
			img.fillAmount = img.fillAmount + 0.08f;
			yield return new WaitForSeconds (0.3f);
			StartCoroutine (FillAction (img));
		}  else if (img.color.a >= 1f) {
			StopCoroutine (FillAction (img));
		}
	}

	IEnumerator FillOutAction (Image img){
		if (img.fillAmount > 0) {
			img.fillAmount = img.fillAmount - 0.1f;
			yield return new WaitForSeconds (0.2f);
			StartCoroutine (FillOutAction (img));
		}  else if (img.color.a <= 0f) {
			StopCoroutine (FillOutAction (img));
		}
	}



	IEnumerator FadeOutAction (Image img){
		if (img.color.a >0) {
			img.color = new Vector4 (img.color.r,img.color.g,img.color.b, img.color.a - 0.03f);
			yield return new WaitForSeconds (0.01f);
			StartCoroutine (FadeOutAction (img));
		}  else if (img.color.a < 0) {
			StopCoroutine (FadeOutAction (img));
		}
	}

	IEnumerator FadeIntAction (Image img){
		if (img.color.a < 1) {
			img.color = new Vector4 (img.color.r,img.color.g,img.color.b, img.color.a + 0.005f);
			yield return new WaitForSeconds (0.001f);
			StartCoroutine (FadeIntAction (img));
		}  else if (img.color.a >= 1) {
			StopCoroutine (FadeIntAction (img));
		}
	}

	IEnumerator BeatingMixture(){
		if (beatingFlag) {
			yield return new WaitForSeconds (0.2f);
			mixture.sprite = shakeMixtures [counter];
			if (counter >= 4) {
				counter = 0;
			} else {
				counter++;
			}
			StartCoroutine (BeatingMixture ());
		} else {
			StopCoroutine (BeatingMixture());

		}
	}

	#endregion
}
