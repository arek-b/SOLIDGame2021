using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A helper class that remembers and restores player's default world position.
/// </summary>
public class PlayerDefaultPosition : MonoBehaviour
{
    [SerializeField] private Vector3 savedWorldPosition = Vector3.zero;

    public void SetCurrentPositionAsDefault()
    {
        savedWorldPosition = transform.position;
    }

    public void RestorePosition()
    {
        transform.position = savedWorldPosition;
    }
}
