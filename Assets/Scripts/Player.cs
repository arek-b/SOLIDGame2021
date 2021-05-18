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
    public PlayerItemInteraction PlayerItemInteraction { get; private set; }
    public PlayerDeath PlayerDeath { get; private set; }
    public PlayerDeathUI PlayerDeathUI { get; private set; }
    public PlayerRespawn PlayerRespawn { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Navigation = GetComponent<PlayerNavigation>();
        InventoryUI = GetComponent<PlayerInventoryUI>();
        Inventory = new PlayerInventory(InventoryUI);
        PlayerItemInteraction = GetComponent<PlayerItemInteraction>();
        PlayerDeath = GetComponent<PlayerDeath>();
        PlayerDeathUI = GetComponent<PlayerDeathUI>();
        PlayerRespawn = GetComponent<PlayerRespawn>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Navigation != null && Inventory != null)
            Navigation.enabled = Inventory.IsEmpty;
    }
}
