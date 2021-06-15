using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportDoor : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _entryPoint;
    [SerializeField] private Transform _exitPoint;
    [SerializeField] private GameObject _enteringCanvasObj;
    [SerializeField] private GameObject _exitingCanvasObj;
    [SerializeField] private GameObject _obstacleWall;
    [SerializeField] private float _teleportingTime = 2f;
    void OnTriggerEnter(Collider other)
    {
        PlayerModelCollider player = other.GetComponent<PlayerModelCollider>();
        if (player != null)
        {
            _playerTransform = player.Player.transform; 
            //_playerTransform = other.transform;
            _enteringCanvasObj.SetActive(true);
            _obstacleWall.SetActive(false);
            player.Player.Respawn.RespawnAt(_exitPoint.transform.position);
            StartCoroutine(Teleport());
        }
    }
    IEnumerator Teleport()
    {
        //_playerTransform = _entryPoint;
        _obstacleWall.SetActive(true);
        yield return new WaitForSeconds(_teleportingTime);
        _enteringCanvasObj.SetActive(false);
        if (!_exitingCanvasObj.activeSelf)
            _exitingCanvasObj.SetActive(true);

        Animator animator = _exitingCanvasObj.transform.parent.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Fade");
        }
    }
}
