using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeBeahvior : MonoBehaviour
{
    [SerializeField, Tooltip("Set by animation")] private bool isGoingRight = false;
    const float PushForce = 1500f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerModelCollider playerModel = other.gameObject.GetComponent<PlayerModelCollider>();
        if (playerModel == null)
            return;

        playerModel.Player.Death.GetPushedAndDie(isGoingRight ? Vector3.right : Vector3.left, PushForce);
    }
}
