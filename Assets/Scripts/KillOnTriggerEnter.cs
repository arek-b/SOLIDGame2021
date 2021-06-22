using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnTriggerEnter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerModelCollider playerModel = other.GetComponent<PlayerModelCollider>();
        if (playerModel == null)
            return;

        playerModel.Player.Death.Die();
    }
}
