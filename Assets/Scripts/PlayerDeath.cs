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

    public void CommitMurder()
    {
        StartCoroutine(DeathCoroutine());
    }

    private IEnumerator DeathCoroutine()
    {
        player.PlayerDeathUI.ShowText();
        yield return new WaitForSeconds(respawnDelayDuration);
        player.PlayerDeathUI.HideText();
        player.PlayerRespawn.RespawnAt(checkpointSystem.LatestCheckpoint.SpawnPoint.position);
    }
}
