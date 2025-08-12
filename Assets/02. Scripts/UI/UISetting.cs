using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : MonoBehaviour
{
    public GameObject uiSetting;

    public void OnFPSButton()
    {

    }

    public void OnTPSButton()
    {

    }

    public void OnExitButton()
    {
        uiSetting.SetActive(false);
    }
}
