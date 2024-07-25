using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class LevelController : MonoBehaviour
{
    public bool isLocked;
    public int levelId;
    public string levelName;
    public Text levelNameText;
    public Text levelHeading;
    public GameObject BackImg;
    public GameObject Lock;
    public GameObject WatchAdBtn;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(levelId);
        levelNameText.text = levelName;
        if (PlayerPrefs.GetInt("LevelPlayed") == 0)
        {
            PlayerPrefs.SetInt("LevelPlayed", 1);
        }
        if (PlayerPrefs.GetInt("LevelPlayed") >= levelId || PlayerPrefs.GetInt("Level" + levelId) == 1)
        {
            levelHeading.text = "Played";
            WatchAdBtn.SetActive(false);
            BackImg.SetActive(false);
            Lock.SetActive(false);
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            levelHeading.text = "";
        }
    }

  

    public void playLevel(int levelIndex)
    {
        string levelNameString = EventSystem.current.currentSelectedGameObject.GetComponent<LevelController>().levelName;
        GameManager.instance.SelectedPizzaFlavour = levelIndex;
        PlayerPrefs.SetString("OrderName", levelNameString);
        Debug.Log(levelNameString);
        if (PlayerPrefs.GetInt("ComingFromSplash") == 1)
        {
            NavigationManager.instance.ReplaceScene(GameScene.CLEANINGVIEW);
        } else
        {
            NavigationManager.instance.ReplaceScene(GameScene.ORDERTAKINGVIEW);
        }
    }

    public void watchAd(int levelIndex)
    {

        //levelHeading.text = "Unlocked";
        //WatchAdBtn.SetActive(false);
        //BackImg.SetActive(false);
        //Lock.SetActive(false);
        //gameObject.GetComponent<Button>().interactable = true;
        //PlayerPrefs.SetInt("Level" + levelId, 1);
        AdsManager.Instance.ShowRewarded(() =>
        {
            levelHeading.text = "Unlocked";
            WatchAdBtn.SetActive(false);
            BackImg.SetActive(false);
            Lock.SetActive(false);
            gameObject.GetComponent<Button>().interactable = true;
            PlayerPrefs.SetInt("Level" + levelId, 1);
        }, "Level Unlock");
        //if (PlayerPrefs.GetInt("ComingFromSplash") == 1)
        //{
        //    NavigationManager.instance.ReplaceScene(GameScene.CLEANINGVIEW);
        //} else
        //{
        //    NavigationManager.instance.ReplaceScene(GameScene.ORDERTAKINGVIEW);
        //}
    }

    void checkLevelUnLocked()
    {
        
        if(PlayerPrefs.GetInt("LevelPlayed") == levelId)
        {
            isLocked = false;
        } else
        {
            isLocked = true;
        }
    }
}
