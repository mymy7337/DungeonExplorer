using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private Vector2 moveDirection = new Vector2(0, 1);
    public float moveSpeed;
    private float lastTurnTime;
    public float turnRate;

    Vector3 samplePoint;
    Vector3 boatVel;

    private BoardingPad boardingPad;

    Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        boardingPad = GetComponentInChildren<BoardingPad>();
    }

    void FixedUpdate()
    {
        Move();
        if(boardingPad.isBoard)
        {
            samplePoint = PlayerManager.Instance.Player.controller._rigidbody.worldCenterOfMass;
            boatVel = _rigidbody.GetPointVelocity(samplePoint);
        }
        else
        {
            boatVel = Vector3.zero;
        }

        PlayerManager.Instance.Player.controller.SetPlatformVelocity(boatVel);
    }

    private void Move()
    {
        Vector3 dir = -transform.forward * moveDirection.y;
        dir *= moveSpeed;

        if (Time.time - lastTurnTime > turnRate)
        {
            lastTurnTime = Time.time;
            dir = Vector3.zero;
            transform.localEulerAngles = transform.localEulerAngles + new Vector3(0, -90, 0);
        }
        _rigidbody.velocity = dir;
    }
}
