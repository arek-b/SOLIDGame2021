using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for respawning the Player at given place.
/// </summary>
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Player player;

    public delegate void eventPlayerWillRespawn();
    public static event eventPlayerWillRespawn PlayerWillRespawn;

    public void RespawnAt(Vector3 worldPosition)
    {
        PlayerWillRespawn?.Invoke();
        player.Navigation.NavMeshAgent.Warp(worldPosition);
        player.Navigation.NavMeshAgent.ResetPath();
    }
}
