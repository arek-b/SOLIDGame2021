using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCubeCollection : MonoBehaviour
{
    [SerializeField] private List<bool> _cubes;
    public void GetACube(int cubeNumber)
    {
        _cubes[cubeNumber] = true;
    }
}
