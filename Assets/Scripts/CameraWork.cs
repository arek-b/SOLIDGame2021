using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for tweening between camera views.
/// </summary>
public class CameraWork : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _shader;
    [SerializeField] private List<Transform> _views;
    [SerializeField] private float _transitionSpeed;
    private Transform _currentView;
    private int _viewNum = 9;
    void Start()
    {
        _shader.SetActive(true);
        _currentView = _views[_viewNum];
    }
    public void CameraSwitch(int viewNumber)
    {
        _currentView = _views[viewNumber];
    }
    void LateUpdate()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position, _currentView.position, Time.deltaTime * _transitionSpeed);
        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(_camera.transform.rotation.eulerAngles.x, _currentView.transform.rotation.eulerAngles.x, Time.deltaTime * _transitionSpeed),
            Mathf.LerpAngle(_camera.transform.rotation.eulerAngles.y, _currentView.transform.rotation.eulerAngles.y, Time.deltaTime * _transitionSpeed),
            Mathf.LerpAngle(_camera.transform.rotation.eulerAngles.z, _currentView.transform.rotation.eulerAngles.z, Time.deltaTime * _transitionSpeed));
        _camera.transform.eulerAngles = currentAngle;
    }
}
