using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public PlayerAnimationController animationController;
    public Equipment equip;

    public ItemDataSO itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        PlayerManager.Instance.Player = this;
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        animationController = GetComponent<PlayerAnimationController>();
        equip = GetComponent<Equipment>();
    }
}
