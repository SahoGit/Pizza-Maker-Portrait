using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerActive : MonoBehaviour
{
    public void OnEnable()
    {
        AdsManager.Instance.ShowMREC();
    }

  

   public  void OnDisable()
   {
        AdsManager.Instance.HideMREC();
   }
}
