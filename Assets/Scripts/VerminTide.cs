using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Vermins that kill our player.
/// </summary>
public class VerminTide : MonoBehaviour
{
    [SerializeField] private float respawnDelayDuration = 5f;
    [SerializeField] private CheckpointSystem checkpointSystem = null;
    [SerializeField] private Player player = null;

    public bool IsDead { get; private set; } = false;
    public bool IsBeingPushed { get; private set; } = false;
    public void OnParticleCollision(GameObject other)
    {
            PlayerModelCollider playerModelCollider = other.GetComponent<PlayerModelCollider>();
        if (playerModelCollider != null)
        {
            StartCoroutine(DeathCoroutine(delay: 0.75f));
        }
    }
    private IEnumerator DeathCoroutine(float delay = 0)
    {
        if (delay > 0)
        {
            yield return new WaitForSeconds(delay);
        }
        IsDead = true;
        if (player.Navigation.NavMeshAgent.enabled && player.Navigation.NavMeshAgent.isOnNavMesh)
        {
            player.Navigation.NavMeshAgent.ResetPath();
        }
        player.DeathUI.ShowText();
        yield return new WaitForSeconds(respawnDelayDuration);
        player.DeathUI.HideText();
        IsBeingPushed = false;
        player.Navigation.NavMeshAgent.enabled = true;
        player.Rigidbody.isKinematic = true;
        player.Respawn.RespawnAt(checkpointSystem.LatestCheckpoint.SpawnPoint.position);
        player.Rigidbody.isKinematic = true;
        player.Navigation.NavMeshAgent.enabled = true;
        IsDead = false;
    }
}
