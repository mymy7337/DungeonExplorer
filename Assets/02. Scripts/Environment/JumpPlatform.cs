using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JumpPlatform : MonoBehaviour
{
    public float jumpPower;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(JumpLaunch());
        }
    }

    private IEnumerator JumpLaunch()
    {
        yield return new WaitForSeconds(5);
        PlayerManager.Instance.Player.controller.JumpLaunch(jumpPower);
    }
}
