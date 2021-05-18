using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for keeping track of latest checkpoint.
/// There should be only one CheckpointSystem on the scene.
/// </summary>
public class CheckpointSystem : MonoBehaviour
{
    private CheckpointVolume latestCheckpoint;
    public CheckpointVolume LatestCheckpoint => latestCheckpoint;

    private void OnEnable()
    {
        CheckpointVolume.CheckpointReached += CheckpointVolume_CheckpointReached;
    }

    private void OnDisable()
    {
        CheckpointVolume.CheckpointReached -= CheckpointVolume_CheckpointReached;
    }

    private void CheckpointVolume_CheckpointReached(CheckpointVolume checkpoint)
    {
        latestCheckpoint = checkpoint;
    }
}
