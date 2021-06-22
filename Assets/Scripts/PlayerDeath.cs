using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class guilty of Player's death.
/// </summary>
public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private float respawnDelayDuration = 5f;
    [SerializeField] private CheckpointSystem checkpointSystem = null;
    [SerializeField] private Player player = null;

    public bool IsDead { get; private set; } = false;
    public bool IsBeingPushed { get; private set; } = false;

    private void Update()
    {
        // for testing
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K pressed - killing the Player");
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(DeathCoroutine());
    }

    public void GetPushedAndDie(Vector3 direction, float force)
    {
        if (IsBeingPushed)
            return;

        IsBeingPushed = true;
        player.Navigation.NavMeshAgent.enabled = false;
        player.Rigidbody.isKinematic = false;
        player.Rigidbody.AddForce(direction * force, ForceMode.Impulse);
        StartCoroutine(DeathCoroutine(delay: 0.75f));
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
