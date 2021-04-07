using System.Collections;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private ItemTypes itemType = default;
    [SerializeField] private string itemName = default;
    [Header("Settings for player's Animator")]
    [SerializeField] private AnimatorParameterTypes animatorParameterType = default;
    [SerializeField] private string parameterName = default;
    private enum AnimatorParameterTypes { Trigger, Bool }
    public ItemTypes ItemType { get => itemType; }
    public string ItemName => itemName == string.Empty ? itemType.ToString() : itemName;

    public void TriggerAnimationOnPlayer(Player player, float delay)
    {
        if (player.Animator == null)
            return;

        if (parameterName == default)
            return;

        StartCoroutine(DelayedTriggerAnimationOnPlayer(player, delay));
    }

    private IEnumerator DelayedTriggerAnimationOnPlayer(Player player, float delay)
    {
        yield return new WaitForSeconds(delay);

        switch(animatorParameterType)
        {
            case AnimatorParameterTypes.Trigger:
                player.Animator.SetTrigger(parameterName);
                break;
            case AnimatorParameterTypes.Bool:
                player.Animator.SetBool(parameterName, value: true);
                break;
        }
    }
}