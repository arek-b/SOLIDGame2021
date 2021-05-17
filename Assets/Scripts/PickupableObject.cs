﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Object that can be physically picked up and moved by the player.
/// </summary>
public class PickupableObject : MonoBehaviour
{
    [SerializeField] private Vector3 offsetWhenPickedUp = new Vector3(0f, 2f, 0f);
    [SerializeField] private float pickingDuration = 1f;
    [SerializeField] private float dropForce = 200f;
    [SerializeField] private Rigidbody myRigidbody = null;
    [SerializeField] private Collider myCollider = null;

    private bool isPickedUp = false;

    Transform originalParent;

    private void Awake()
    {
        originalParent = transform.parent;
    }

    public void Interact(Transform playerTransform)
    {
        if (isPickedUp)
            BeDropped(playerTransform);
        else
            BePickedUp(playerTransform);
    }

    private void BePickedUp(Transform playerTransform)
    {
        if (isPickedUp)
            return;

        isPickedUp = true;
        myRigidbody.isKinematic = true;
        myCollider.enabled = false;
        transform.SetParent(playerTransform);
        transform.DOLocalMove(offsetWhenPickedUp, pickingDuration);
    }

    private void BeDropped(Transform playerTransform)
    {
        if (!isPickedUp)
            return;

        isPickedUp = false;
        transform.SetParent(originalParent);
        myRigidbody.isKinematic = false;
        myCollider.enabled = true;
        myRigidbody.AddForce((playerTransform.forward * dropForce) + (playerTransform.up * dropForce));
    }
}
