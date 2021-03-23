using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamSwitchTrigger : MonoBehaviour
{
    [SerializeField] private int _viewNumber = 0;
    [SerializeField] private GameObject _camWorkObject;
    private CameraWork _camWorkScr;
    void Start()
    {
        _camWorkScr = _camWorkObject.GetComponent<CameraWork>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
        _camWorkScr.CameraSwitch(_viewNumber);
    }
}
