using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Exits game on enter.
/// </summary>
public class ExitGame : MonoBehaviour
{
    [SerializeField] private float _timeToExit = 5f;
    [SerializeField] private GameObject _finishedGame;
    void OnTriggerEnter(Collider other)
    {
        PlayerModelCollider playerModelCollider = other.GetComponent<PlayerModelCollider>();
        if (playerModelCollider != null)
        {
            _finishedGame.SetActive(true);
            StartCoroutine(ShutDown(_timeToExit));
        }
    }
    IEnumerator ShutDown(float time)
    {
        yield return new WaitForSeconds(time);
        Application.Quit();
    }
}
