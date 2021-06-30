using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls four cubes on the map and how player can obtain them.
/// </summary>
public class CubeActivator : MonoBehaviour
{
    [SerializeField] private int _cubeNumber = 0;
    [SerializeField] private GameObject _puzzleGlow;
    private bool _activated = false;
    private bool _deactivated = false;
    private Player _playerScr;
    [SerializeField] private GameObject _puzzlePiece;
    private Puzzle _puzzle;
    private void Start()
    {
        _puzzle = _puzzlePiece.GetComponent<Puzzle>();
    }
    void Update()
    {
        if (_activated == true)
        {
            if (_playerScr != null && _playerScr.Death.IsDead)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                _playerScr.GetComponent<PlayerCubeCollection>().GetACube(_cubeNumber);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (_deactivated == false)
        {
            PlayerModelCollider playerModelCollider = other.GetComponent<PlayerModelCollider>();
            if (playerModelCollider != null)
            {
                _playerScr = playerModelCollider.Player;
                Activate();
            }
        }
    }
    void Activate()
    {
        _puzzleGlow.SetActive(true);
        _activated = true;
    }
    void OnTriggerExit(Collider other)
    {
        if (_deactivated == false)
        {
            if (other.GetComponent<PlayerModelCollider>())
            {
                _activated = false;
                _puzzleGlow.SetActive(false);
            }
        }
    }
}
