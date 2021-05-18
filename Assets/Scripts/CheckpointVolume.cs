using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A volume (collider) representing a checkpoint.
/// Requires a collider with isTrigger checked.
/// </summary>
public class CheckpointVolume : MonoBehaviour
{
    [SerializeField] private int checkpointNumber = 0;
    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private Collider collider = null;

    public Transform SpawnPoint => spawnPoint;

    public int CheckpointNumber => checkpointNumber;

    private int timesReached = 0;
    private bool reached = false;
    public bool Reached
    {
        get => reached;

        set
        {
            reached = value;
            if (value)
                timesReached++;
            if (value && timesReached == 1)
                CheckpointReached?.Invoke(this);

            Debug.Log($"Checkpoint {CheckpointNumber} reached");
        }
    }

    public delegate void eventCheckpointReached(CheckpointVolume checkpoint);
    public static event eventCheckpointReached CheckpointReached;

    private void Awake()
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning($"Spawn point not set for checkpoint with number {CheckpointNumber}. Using {nameof(CheckpointVolume)}'s transform instead.");
            spawnPoint = transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Reached)
            return;

        if (other.GetComponent<PlayerModelCollider>() == null)
            return;

        Reached = true;
    }

    private void OnValidate()
    {
        if (collider == null)
            collider = GetComponent<Collider>();

        if (collider != null)
            collider.isTrigger = true;
    }
}
