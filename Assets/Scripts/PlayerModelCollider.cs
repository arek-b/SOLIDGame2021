using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelCollider : MonoBehaviour
{
    public Player Player { get; private set; }

    private void Awake()
    {
        Player = GetComponentInParent<Player>();
    }
}