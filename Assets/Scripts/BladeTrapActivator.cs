using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Activates blades on start.
/// </summary>
public class BladeTrapActivator : MonoBehaviour
{
    [SerializeField] private List<GameObject> _blades;
    [SerializeField] private float _cutInterval = 0.2f;
    private int _constantOne = 1;
    private int _constantZero = 0;
    private int _bladeNum;
    void Start()
    {
        _bladeNum = _blades.Count; 
        StartCoroutine(ActivateBlade(_blades.Count-_constantOne));
    }
    void Another(int bladeNum) 
    {
        StartCoroutine(ActivateBlade(bladeNum));
    }
    IEnumerator ActivateBlade(int bladeNumber)
    {
        yield return new WaitForSeconds(_cutInterval);
        if (bladeNumber >= _constantZero)
        {
            _blades[bladeNumber].GetComponent<Animator>().enabled = true;
            _bladeNum--;
            Another(_bladeNum);
        }
    }
}
