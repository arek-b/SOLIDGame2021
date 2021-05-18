using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

/// <summary>
/// Class reponsible for navigating our player character to the point clicked by the player.
/// </summary>
public class PlayerNavigation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private Player player = null;
    private const float MaxRayDistance = 100f;

    public NavMeshAgent NavMeshAgent => agent;

    private void Update()
    {
        if (player.Death.IsDead)
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, MaxRayDistance, layerMask: ~0, QueryTriggerInteraction.Ignore))
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
