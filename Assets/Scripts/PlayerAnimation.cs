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
    private readonly int isWalkingBool = Animator.StringToHash("IsWalking");
    private readonly int pickUpObjectTrigger = Animator.StringToHash("PickUpObject");
    private readonly int holdingObjectFloat = Animator.StringToHash("HoldingObjectBlend");
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
            animator.SetBool(isWalkingBool, true);
        }

        if (agent.velocity.magnitude < WalkingSpeed && isWalking)
        {
            isWalking = false;
            animator.SetBool(isWalkingBool, false);
        }
    }

    public void StartHoldingObject()
    {
        animator.SetTrigger(pickUpObjectTrigger);
        animator.SetFloat(holdingObjectFloat, HoldingObjectFloatActivated);
    }

    public void StopHoldingObject()
    {
        animator.SetFloat(holdingObjectFloat, HoldingObjectFloatDeactivated);
    }
}
