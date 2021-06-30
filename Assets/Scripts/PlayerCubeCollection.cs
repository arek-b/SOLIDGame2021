using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script put on player, holds information of all cubes that he's gathered so far.
/// </summary>
public class PlayerCubeCollection : MonoBehaviour
{
    [SerializeField] private List<bool> _cubes;
    public void GetACube(int cubeNumber)
    {
        _cubes[cubeNumber] = true;
    }
}
