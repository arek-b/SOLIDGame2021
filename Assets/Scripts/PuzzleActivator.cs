using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls when and how the player can interact with the puzzle.
/// </summary>
public class PuzzleActivator : MonoBehaviour
{
    [SerializeField] private GameObject _puzzlePiece;
    [SerializeField] private bool _timeRestart = false;
    [SerializeField] private float _restartTime = 5f;
    [SerializeField] private GameObject _puzzleGlow;
    [SerializeField] private GameObject _missingSomethingGlow;
    [SerializeField] private ItemProvider _itemProvider = null;
    [SerializeField] private PickupableObject _pickupableObject = null;
    [SerializeField, Tooltip("Useful when puzzle is an object that can be used/activated multiple times.")]
        private bool _alwaysActive = false;
    private bool _activated = false;
    private bool _deactivated = false;
    private Player _playerScr;
    private Puzzle _puzzle;
    [SerializeField] private bool _puzzlePiecesRequired = false;
    [SerializeField] private int _puzzlePieceNumber = 0;
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
                _puzzle.Activate();

                if (!_alwaysActive)
                {
                    _deactivated = true;
                }
                
                if (_timeRestart == true)
                {
                    StartCoroutine(Reset());
                }

                if (_itemProvider != null && _playerScr != null)
                {
                    _playerScr.GetComponent<PlayerInventoryList>().GetPuzzlePiece(_puzzlePieceNumber);
                    _itemProvider.GiveItem(_playerScr);
                }

                if (_pickupableObject != null && _playerScr != null)
                {
                    _pickupableObject.Interact(_playerScr);
                }
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

                if (_puzzlePiecesRequired == true)
                {
                    if( _puzzlePieceNumber == _playerScr.GetComponent<PlayerInventoryList>().puzzlePieceNumber)
                    {
                        Activate();
                    }
                    else
                    {
                        _missingSomethingGlow.SetActive(true);
                    }
                }
                else
                {
                    Activate();
                }
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
                _missingSomethingGlow.SetActive(false);
                _puzzleGlow.SetActive(false);
            }
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(_restartTime);
        _activated = false;
        _deactivated = false;
    }
}
