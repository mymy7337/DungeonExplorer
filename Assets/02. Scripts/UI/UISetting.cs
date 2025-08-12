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
        Vector3 fpsPos = new Vector3(0, 2, 0.3f);
        controller.ChangeCameraPosition(fpsPos);
    }

    public void OnTPSButton()
    {
        Vector3 tpsPos = new Vector3(0, 3, -3);
        controller.ChangeCameraPosition(tpsPos);
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
