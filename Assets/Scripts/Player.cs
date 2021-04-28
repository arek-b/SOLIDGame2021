using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main player class. Mostly just contains public references to other classes related to the player.
/// </summary>
public class Player : MonoBehaviour
{
    public PlayerInventory Inventory { get; private set; }
    private PlayerInventoryUI InventoryUI { get; set; }
    public PlayerNavigation Navigation { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Navigation = GetComponent<PlayerNavigation>();
        InventoryUI = GetComponent<PlayerInventoryUI>();
        Inventory = new PlayerInventory(InventoryUI);
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Navigation != null && Inventory != null)
            Navigation.enabled = Inventory.IsEmpty;
    }
}
