using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for interacting with objects using the item currently
/// held by the player. The interaction happens via mousing over and clicking.
/// </summary>
public class PlayerItemInteraction : MonoBehaviour
{
    /// <summary>
    /// Reference to Player (required)
    /// </summary>
    [SerializeField] private Player player = null;
    /// <summary>
    /// Reference to current Camera (required)
    /// </summary>
    [SerializeField] private Camera mainCamera = null;
    /// <summary>
    /// Max raycast distance (from camera)
    /// </summary>
    private const float MaxRayDistance = 100f;
    /// <summary>
    /// How long should the player character rotate to face an object that was
    /// moused over? (Time in seconds)
    /// </summary>
    private const float LookDuration = 0.5f;
    /// <summary>
    /// Last valid interactable object that the player moused over. Used for
    /// checking if mousing over the same object we moused over in last frame.
    /// </summary>
    private InteractWithItem lastSubject = null;
    /// <summary>
    /// Was the object under mouse pointer interactable on current frame?
    /// </summary>
    private bool currentSubjectWasOK = false;

    private void Update()
    {
        currentSubjectWasOK = false;

        if (player.Inventory.IsEmpty)
            return;

        InventoryItem currentItem = player.Inventory.CurrentInventoryItem;
        if (Input.GetMouseButtonDown(0))
        {
            player.Inventory.Empty();
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit, MaxRayDistance))
            return;

        InteractWithItem currentSubject = hit.collider.gameObject.GetComponent<InteractWithItem>();

        if (currentSubject == null)
            return;

        if (currentSubject.ItemType != currentItem.ItemType)
            return;

        if (!currentSubject.PlayerCanInteract())
            return;

        // code within this if-statement shouldn't be called every frame
        currentSubjectWasOK = true;
        if (currentSubject != lastSubject)
        {
            currentSubject.ShowInteractionCue();
            player.Navigation.LookAt(currentSubject.transform, duration: LookDuration);
        }
        lastSubject = currentSubject;

        if (!Input.GetMouseButtonDown(0))
            return;

        currentItem.TriggerAnimationOnPlayer(player, delay: LookDuration);
        currentSubject.Interact();
    }

    private void LateUpdate()
    {
        // need to reset last subject for when we mouse over the same subject again
        if (!currentSubjectWasOK)
            lastSubject = null;
    }
}
