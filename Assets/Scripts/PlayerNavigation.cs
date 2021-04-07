using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class PlayerNavigation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Camera mainCamera = null;
    private const float MaxRayDistance = 100f;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, MaxRayDistance))
            return;

        agent.SetDestination(hit.point);
    }

    public void LookAt(Transform target, float duration)
    {
        Vector3 position = transform.position - target.position;
        position.y = 0;
        Quaternion rotation = Quaternion.LookRotation(position);
        transform.DORotateQuaternion(rotation, duration);
    }
}
