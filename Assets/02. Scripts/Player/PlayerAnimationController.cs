using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMove");
    private static readonly int IsRunning = Animator.StringToHash("IsRun");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void Move(Vector3 obj)
    {
        animator.SetBool(IsMoving, obj.magnitude > 0.5f);
    }
}
