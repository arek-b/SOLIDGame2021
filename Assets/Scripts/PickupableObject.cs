using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Object that can be physically picked up and moved by the player.
/// </summary>
public class PickupableObject : MonoBehaviour
{
    [SerializeField] private Vector3 offsetWhenPickedUp = new Vector3(0f, 1.35f, 1.35f);
    [SerializeField] private float pickingDuration = 1f;
    [SerializeField] private float dropForce = 200f;
    [SerializeField] private Rigidbody myRigidbody = null;
    [SerializeField] private Collider myCollider = null;

    private bool IsPickedUp => playerCurrentlyHolding != null;
    private Player playerCurrentlyHolding = null;

    private Transform originalParent;
    private Vector3 originalPosition;
    private Vector3 lastPosition;
    private const float MaxDistance = 10f;

    private void Awake()
    {
        originalParent = transform.parent;
        originalPosition = transform.position;
        lastPosition = originalPosition;
    }

    private void OnEnable()
    {
        PlayerRespawn.PlayerWillRespawn += PlayerRespawn_PlayerWillRespawn;
    }
    private void OnDisable()
    {
        PlayerRespawn.PlayerWillRespawn -= PlayerRespawn_PlayerWillRespawn;
    }

    private void Update()
    {
        // prevent glitching out
        if (Vector3.Distance(myRigidbody.position, lastPosition) > MaxDistance)
        {
            ResetPosition();
        }
        lastPosition = myRigidbody.position;
    }

    /// <summary>
    /// Resets the object when player is about to be respawned
    /// </summary>
    private void PlayerRespawn_PlayerWillRespawn()
    {
        if (!IsPickedUp)
            return;

        ResetPosition();
    }

    private void ResetPosition()
    {
        if (playerCurrentlyHolding != null)
        {
            BeDropped(playerCurrentlyHolding, resetPosition: true, addForce: false);
        }
        else
        {
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.angularVelocity = Vector3.zero;
            myRigidbody.position = originalPosition;
            transform.position = originalPosition;
        }
    }

    public void Interact(Player player)
    {
        if (IsPickedUp)
        {
            BeDropped(player, resetPosition: false, addForce: true);
        }
        else
        {
            BePickedUp(player);
        }
    }

    private void BePickedUp(Player player)
    {
        if (IsPickedUp)
            return;

        playerCurrentlyHolding = player;
        playerCurrentlyHolding.Animation.StartHoldingObject();
        myRigidbody.isKinematic = true;
        myRigidbody.useGravity = false;
        myCollider.isTrigger = true;
        transform.SetParent(player.transform);
        transform.DOLocalMove(offsetWhenPickedUp, pickingDuration);
    }

    private void BeDropped(Player player, bool resetPosition, bool addForce)
    {
        if (!IsPickedUp)
            return;

        playerCurrentlyHolding.Animation.StopHoldingObject();
        playerCurrentlyHolding = null;
        transform.SetParent(originalParent);
        if (resetPosition)
            transform.position = originalPosition;
        myRigidbody.isKinematic = false;
        myRigidbody.useGravity = true;
        myCollider.isTrigger = false;
        if (addForce)
            myRigidbody.AddForce((player.transform.forward * dropForce) + (player.transform.up * dropForce));
    }
}
