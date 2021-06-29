using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryList : MonoBehaviour
{
    [SerializeField] private List<GameObject> _puzzlePieces;
    public int puzzlePieceNumber = 0;
    public void GetPuzzlePiece(int puzzlePiece)
    {
        puzzlePieceNumber = puzzlePiece;
        for (int i = 0; i < _puzzlePieces.Count; i++)
        {
            _puzzlePieces[i].SetActive(false);
        }
        _puzzlePieces[puzzlePieceNumber].SetActive(true);
    }
}
