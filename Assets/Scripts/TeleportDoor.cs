using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private GameObject _enteringCanvasObj;
    [SerializeField] private GameObject _exitingCanvasObj;
    [SerializeField] private float _teleportingTime = 2f;
    void OnTriggerEnter(Collider other)
    {
        PlayerModelCollider player = other.GetComponent<PlayerModelCollider>();
        if (player != null)
        {
            _playerTransform = other.transform;
            _enteringCanvasObj.SetActive(true);
            StartCoroutine(Teleport());
        }
    }
    IEnumerator Teleport()
    {
        _playerTransform = _entryPoint;
        yield return new WaitForSeconds(_teleportingTime);
        _enteringCanvasObj.SetActive(false);
        _exitingCanvasObj.GetComponent<Animator>().SetTrigger("Fade");
        _playerTransform = _exitPoint;
    }
}
