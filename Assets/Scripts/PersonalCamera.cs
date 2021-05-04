using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerTarget;
    void Update()
    {
        this.transform.LookAt(_playerTarget);
    }
}
