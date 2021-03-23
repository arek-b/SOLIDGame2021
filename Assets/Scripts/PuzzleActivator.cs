using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleActivator : MonoBehaviour
{
    [SerializeField] private GameObject _puzzlePiece;
    [SerializeField] private bool _timeRestart = false;
    [SerializeField] private float _restartTime = 5f;
    [SerializeField] private GameObject _puzzleGlow;
    private bool _activated = false;
    private Player _playerScr;
    private Puzzle _puzzle;
    private void Start()
    {
        _puzzle = _puzzlePiece.GetComponent<Puzzle>();
    }
    void Update()
    {
        if(_activated == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                _puzzle.Activate();
                if (_timeRestart == true)
                {
                    StartCoroutine(Reset());
                }
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _playerScr = other.GetComponent<Player>();
            _puzzleGlow.SetActive(true);
            _activated = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            _activated = false;
            _puzzleGlow.SetActive(false);
        }
    }
    IEnumerator Reset()
    {
        yield return new WaitForSeconds(_restartTime);
        _activated = false;
    }
}
