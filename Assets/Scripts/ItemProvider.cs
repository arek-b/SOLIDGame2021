using System.Collections;
using UnityEngine;

public class ItemProvider : MonoBehaviour
{
    [SerializeField] private InventoryItem item;

    public InventoryItem Item { get => item; }

    public void GiveItem(Player player)
    {
        player.Inventory.CurrentInventoryItem = item;
    }
}