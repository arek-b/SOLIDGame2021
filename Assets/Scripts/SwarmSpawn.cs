using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A spawner.
/// </summary>
public class SwarmSpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> _swarmPool;
    [SerializeField] private GameObject _swarmContainmentUnit;
    [SerializeField] private GameObject _swarmUnit;
    [SerializeField] private Transform _swarmSpawn;
    private GameObject _rodent;
    private int _rodentAmount;
    private GameObject _rodentRequester;
    private void Start()
    {
        _swarmPool = GenerateSwarm(30);
        _rodentAmount = 30;
        //_rodentRequester = RequestPrefab();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            //RequestPrefab();
            _rodent = _swarmPool[_rodentAmount];
            _rodent.SetActive(true);
            _rodent.transform.position = _swarmSpawn.position;
            _rodent.transform.rotation = _swarmSpawn.rotation;
            _rodentAmount--;
            /*
            _rodent = Instantiate(_swarmUnit) as GameObject;
            _rodent.transform.position = _swarmSpawn.position;
            _rodent.transform.rotation = _swarmSpawn.rotation;
            */
        }
    }
    List <GameObject> GenerateSwarm(int p_amountOfSwarm)
    {
        for(int i = 0; i < p_amountOfSwarm; i++)
        {
            GameObject rodent = Instantiate(_swarmUnit);
            rodent.transform.parent = _swarmContainmentUnit.transform;
            rodent.SetActive(false);
            _swarmPool.Add(rodent);
        }
        return _swarmPool;
    }
    /*public GameObject RequestPrefab()
    {
        foreach(var prefab in _swarmPool)
        {
            if(prefab.activeInHierarchy == false)
            {
                prefab.SetActive(true);
                return prefab;
            }
        }
        _swarmPool = GenerateSwarm(1);
        GameObject newPrefab = _swarmPool[_swarmPool.Count - 1];
        newPrefab.SetActive(true);
        return newPrefab;
    }*/
}
