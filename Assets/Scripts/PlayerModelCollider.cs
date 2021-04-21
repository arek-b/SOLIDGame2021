using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script should be used when Player's collider is on a different
/// GameObject than the Player script (but is a child of Player). Useful
/// for OnTriggerEnter() etc.
/// </summary>
public class PlayerModelCollider : MonoBehaviour
{
    public Player Player { get; private set; }

    private void Awake()
    {
        Player = GetComponentInParent<Player>();
    }
}