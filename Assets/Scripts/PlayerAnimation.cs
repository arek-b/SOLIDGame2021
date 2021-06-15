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

    private bool isWalking = false;
    private const string IsWalkingBoolName = "IsWalking";
    private const string PickUpObjectTriggerName = "PickUpObject";
    private const string HoldingObjectFloatName = "HoldingObjectBlend";
    private const float HoldingObjectFloatDeactivated = 0f;
    private const float HoldingObjectFloatActivated = 1f;
    private const float WalkingSpeed = 1.5f;

    private void Start()
    {
        if (agent == null)
            enabled = false;

        if (animator == null)
            enabled = false;
    }

    private void Update()
    {
        if (agent.velocity.magnitude >= WalkingSpeed && !isWalking)
        {
            isWalking = true;
            animator.SetBool(IsWalkingBoolName, true);
        }

        if (agent.velocity.magnitude < WalkingSpeed && isWalking)
        {
            isWalking = false;
            animator.SetBool(IsWalkingBoolName, false);
        }
    }

    public void StartHoldingObject()
    {
        animator.SetTrigger(PickUpObjectTriggerName);
        animator.SetFloat(HoldingObjectFloatName, HoldingObjectFloatActivated);
    }

    public void StopHoldingObject()
    {
        animator.SetFloat(HoldingObjectFloatName, HoldingObjectFloatDeactivated);
    }
}
