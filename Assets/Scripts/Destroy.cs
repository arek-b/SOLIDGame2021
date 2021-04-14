using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float _time = 1.35f;
    public GameObject _object;
    void Start()
    {
        Destroy(_object, _time);
    }
}
