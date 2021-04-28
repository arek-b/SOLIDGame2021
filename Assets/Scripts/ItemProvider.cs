using System.Collections;
using UnityEngine;

/// <summary>
/// Gives the Player a specified item. Use in combination with PuzzleActivator.
/// </summary>
public class ItemProvider : MonoBehaviour
{
    [SerializeField] private InventoryItem item;

    public InventoryItem Item { get => item; }

    public void GiveItem(Player player)
    {
        player.Inventory.CurrentInventoryItem = item;
    }
}