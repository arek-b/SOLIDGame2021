﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains configuration and behavior of a puzzle.
/// </summary>
public class Puzzle : MonoBehaviour
{
    private bool _activated = false;
    [SerializeField] private List<GameObject> _revealedObjects;
    [SerializeField] private bool _reveal = false;
    [SerializeField] private bool _revealTimed = false;
    [SerializeField] private float _revealTime = 5f;
    [SerializeField] private List<GameObject> _hiddenObjects;
    [SerializeField] private bool _hide = false;
    [SerializeField] private bool _hideTimed = false;
    [SerializeField] private float _hideTime = 5f;
    [SerializeField] private List<GameObject> _animatedObjects;
    [SerializeField] private bool _animate = false;
    [SerializeField] private bool _animateTimed = false;
    [SerializeField] private float _animateTime = 5f;
    [SerializeField] private bool _keyUnlocked = true;
    [SerializeField] private List<bool> _keys;
    private int _keyCount = 0;
    public void Activate()
    {
        if (_keyUnlocked == true)
        {
            if (_reveal == true && _revealTimed == false)
            {
                UnCover(_revealedObjects);
            }
            else if (_reveal == true && _revealTimed == true)
            {
                StartCoroutine(UnCoverTimeOut(_revealedObjects, _revealTime));
            }
            if (_hide == true && _hideTimed == false)
            {
                UnCover(_hiddenObjects);
            }
            else if (_hide == true && _hideTimed == true)
            {
                StartCoroutine(UnCoverTimeOut(_hiddenObjects, _hideTime));
            }
            if (_animate == true && _animateTimed == false)
            {
                Animate(_animatedObjects);
            }
            else if (_animate == true && _animateTimed == true)
            {
                StartCoroutine(AnimateTimeOut(_animatedObjects, _animateTime));
            }
        }
    }
    public void SetKey()
    {
        _keys[_keyCount] = true;
        _keyCount++;
        if(_keyCount > _keys.Count)
        {
            _keyCount = _keys.Count;
        }
        for (int i = 0; i < _keys.Count; i++)
        {
            if (_keys[i] == false)
            {
                Debug.Log("You need more to unlock!");
            }
            else if (_keyCount >= _keys.Count)
            {
                _keyUnlocked = true;
                Activate();
            }
        }
    }
    public void UnsetKey()
    {
        _keyCount--;
        _keys[_keyCount] = false;
        if(_keyCount <= 0)
        {
            _keyCount = 0;
        }
        if(_keyUnlocked == true)
        {
            //Deanimate
        }
    }
    public void UnCover(List<GameObject> gameObjects)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (_hide == true)
            {
                gameObjects[i].SetActive(false);
            }
            if (_reveal == true)
            {
                gameObjects[i].SetActive(true);
            }
        }
        Debug.Log("Hejo!");
    }
    public void Animate(List<GameObject> gameObjects)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].GetComponent<Animator>().enabled = true;
        }
    }
    IEnumerator UnCoverTimeOut(List<GameObject> gameObjects, float time)
    {
        yield return new WaitForSeconds(time);
        UnCover(gameObjects);
    }
    IEnumerator AnimateTimeOut(List<GameObject> gameObjects, float time)
    {
        yield return new WaitForSeconds(time);
        Animate(gameObjects);
    }
}
