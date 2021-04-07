using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemInteraction : MonoBehaviour
{
    [SerializeField] private Player player = null;
    [SerializeField] private Camera mainCamera = null;
    private const float MaxRayDistance = 100f;
    private const float LookDuration = 0.5f;
    private InteractWithItem lastSubject = null;
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
