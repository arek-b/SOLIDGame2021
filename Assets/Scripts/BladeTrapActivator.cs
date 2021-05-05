using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrapActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _blades;
    [SerializeField] private float _cutInterval = 0.2f;
    private int _bladeNum;
    void Start()
    {
        _bladeNum = _blades.Count; 
        StartCoroutine(ActivateBlade(_blades.Count-1));
    }
    void Another(int bladeNum) 
    {
        StartCoroutine(ActivateBlade(bladeNum));
        /*for (int i = 0; i < _blades.Count; i++)
        {
            StartCoroutine(ActivateBlade(i));
        }*/
    }
    IEnumerator ActivateBlade(int bladeNumber)
    {
        yield return new WaitForSeconds(_cutInterval);
        if (bladeNumber >= 0)
        {
            _blades[bladeNumber].GetComponent<Animator>().enabled = true;
            _bladeNum--;
            Another(_bladeNum);
        }
    }
}
