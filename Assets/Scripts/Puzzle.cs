using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private bool _activated = false;
    [SerializeField] private bool _reveal = false;
    [SerializeField] private List<GameObject> _revealedObjects;
    [SerializeField] private bool _hide = false;
    [SerializeField] private List<GameObject> _hiddenObjects;
    [SerializeField] private bool _animate = false;
    [SerializeField] private List<GameObject> _animatedObjects;
    public void Activate()
    {
        if (_reveal == true)
        {
            UnCover(_revealedObjects, _reveal);
        }
        if (_hide == true)
        {
            UnCover(_hiddenObjects, _hide);
        }
        if(_animate == true)
        {
            Animate(_animatedObjects);
        }
    }
    public void UnCover(List<GameObject> gameObjects, bool uncover)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            if (_hide == true)
                gameObjects[i].SetActive(false);
            else if (_reveal == true)
                gameObjects[i].SetActive(true);
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
}
