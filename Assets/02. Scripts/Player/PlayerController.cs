using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;
    private bool isRun;
    private bool isCourch;
    public float crouchSpeed;
    private bool canMove = true;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    Rigidbody _rigidbody;
    CapsuleCollider _collider;

    public Action inventory;
    public Action setting;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    private void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;

        PlayerManager.Instance.Player.animationController.Move(dir);
        if(dir != Vector3.zero)
            PlayerManager.Instance.Player.animationController.Run(isRun);
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed && canMove)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }

    public void OnRunInput(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {

            isRun = true;
            curMovementInput *= 2;
        }

        else if (context.phase == InputActionPhase.Canceled)
        {
            isRun = false;
            curMovementInput /= 2;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            PlayerManager.Instance.Player.animationController.Jump();
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    public void OnCrouchInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curMovementInput.magnitude == 0)
        {
            isCourch = !isCourch;
            canMove = !isCourch;
            float yPos = !isCourch ? 2f : 1.3f;
            PlayerManager.Instance.Player.animationController.Crouch(isCourch);
            cameraContainer.localPosition = new Vector3(0, yPos, 0);
            if (isCourch)
            {
                _collider.center = new Vector3(0, 0.6f, 0);
                _collider.height = 1.5f;
            }
            else
            {
                _collider.center = new Vector3(0, 1f, 0);
                _collider.height = 2.2f;
            }
        }
    }

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    public void OnSettingButton(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {
            setting?.Invoke();
            ToggleCursor();
        }
    }    

    private void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.5f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void JumpLaunch(float power)
    {
        _rigidbody.AddForce(Vector2.up * power, ForceMode.Impulse);
    }
}
