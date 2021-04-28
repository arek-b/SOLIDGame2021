using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// A class responsible for controlling animations on player's model.
/// </summary>
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;
    [SerializeField] private Animator animator = null;

    private const string IsWalkingBoolName = "IsWalking";
    private bool isWalking = false;

    private void Start()
    {
        if (agent == null)
            enabled = false;

        if (animator == null)
            enabled = false;
    }

    private void Update()
    {
        if (agent.velocity.magnitude > 0 && !isWalking)
        {
            isWalking = true;
            animator.SetBool(IsWalkingBoolName, true);
        }

        if (agent.velocity.magnitude == 0 && isWalking)
        {
            isWalking = false;
            animator.SetBool(IsWalkingBoolName, false);
        }
    }
}
