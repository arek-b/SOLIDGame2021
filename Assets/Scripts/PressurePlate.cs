using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private GameObject _puzzlePiece;
    [SerializeField] private GameObject _tileAnimate;
    private bool _pressed = false;
    void OnTriggerEnter(Collider other)
    {
            if (other.GetComponent<Heavy>())
            {
                _puzzlePiece.GetComponent<Puzzle>().SetKey();
                _tileAnimate.GetComponent<Animator>().SetBool("On", true);
            }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Heavy>())
        {
            _puzzlePiece.GetComponent<Puzzle>().UnsetKey();
            _tileAnimate.GetComponent<Animator>().SetBool("On", false);
        }
    }
}
