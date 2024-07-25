using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdShow : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        AdsManager.Instance.ShowInterstitial("Ad show on Task complete screen");
    }

   
}
