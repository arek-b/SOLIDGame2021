using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger that changes the current camera view.
/// </summary>
public class CamSwitchTrigger : MonoBehaviour
{
    [SerializeField] private int _viewNumber = 9;
    [SerializeField] private GameObject _camWorkObject;
    [SerializeField] private bool _instantSwitch = false;
    private CameraWork _camWorkScr;
    void Start()
    {
        _camWorkScr = _camWorkObject.GetComponent<CameraWork>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModelCollider>())
            _camWorkScr.CameraSwitch(_viewNumber, _instantSwitch);
    }
}
