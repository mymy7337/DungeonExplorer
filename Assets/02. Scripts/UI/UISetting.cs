using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : MonoBehaviour
{
    public GameObject settingWindow;

    private PlayerController controller;

    private void Start()
    {
        controller = PlayerManager.Instance.Player.controller;

        controller.setting += Toggle;

        settingWindow.SetActive(false);
    }

    public void OnFPSButton()
    {

    }

    public void OnTPSButton()
    {

    }

    public void Toggle()
    {
        if (IsOpen())
        {
            settingWindow.SetActive(false);
        }
        else
        {
            settingWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return settingWindow.activeInHierarchy;
    }
}
