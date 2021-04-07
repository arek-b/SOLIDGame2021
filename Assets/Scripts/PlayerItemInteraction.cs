using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    [SerializeField] private Player player = null;
    [SerializeField] private Camera mainCamera = null;
    private const float MaxRayDistance = 100f;
    private const float LookDuration = 0.5f;

    private void Update()
    {
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

        InteractWithItem objectThatInteractsWithItem = hit.collider.gameObject.GetComponent<InteractWithItem>();

        if (objectThatInteractsWithItem == null)
            return;

        if (objectThatInteractsWithItem.ItemType != currentItem.ItemType)
            return;

        if (!objectThatInteractsWithItem.PlayerCanInteract())
            return;

        objectThatInteractsWithItem.ShowInteractionCue();
        player.Navigation.LookAt(objectThatInteractsWithItem.transform, duration: LookDuration);

        if (!Input.GetMouseButtonDown(0))
            return;

        currentItem.TriggerAnimationOnPlayer(player, delay: LookDuration);
        objectThatInteractsWithItem.Interact();

        // Todo: rotate the player and play animation
    }
}
