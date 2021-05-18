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

    private void Update()
    {
        // for testing
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K pressed - killing the Player");
            CommitMurder();
        }
    }

    public void CommitMurder()
    {
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        IsDead = true;
        player.Navigation.NavMeshAgent.ResetPath();
        player.DeathUI.ShowText();
        yield return new WaitForSeconds(respawnDelayDuration);
        player.DeathUI.HideText();
        player.Respawn.RespawnAt(checkpointSystem.LatestCheckpoint.SpawnPoint.position);
        IsDead = false;
    }
}
