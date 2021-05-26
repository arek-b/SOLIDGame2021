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
    public PlayerItemInteraction ItemInteraction { get; private set; }
    public PlayerDeath Death { get; private set; }
    public PlayerDeathUI DeathUI { get; private set; }
    public PlayerRespawn Respawn { get; private set; }
    public PlayerAnimation Animation { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Navigation = GetComponent<PlayerNavigation>();
        InventoryUI = GetComponent<PlayerInventoryUI>();
        Inventory = new PlayerInventory(InventoryUI);
        ItemInteraction = GetComponent<PlayerItemInteraction>();
        Death = GetComponent<PlayerDeath>();
        DeathUI = GetComponent<PlayerDeathUI>();
        Respawn = GetComponent<PlayerRespawn>();
        Animation = GetComponent<PlayerAnimation>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Navigation != null && Inventory != null)
            Navigation.enabled = Inventory.IsEmpty;
    }
}
