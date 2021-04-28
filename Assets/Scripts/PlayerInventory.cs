/// <summary>
/// Our player's single-item inventory.
/// </summary>
public class PlayerInventory
{
    private InventoryItem currentInventoryItem;
    public InventoryItem CurrentInventoryItem
    {
        get => currentInventoryItem;
        set
        {
            currentInventoryItem = value;
            UpdateUI();
        }
    }
    public bool IsEmpty => CurrentInventoryItem == null;

    private readonly PlayerInventoryUI ui;

    public PlayerInventory(PlayerInventoryUI ui)
    {
        this.ui = ui;
        UpdateUI();
    }

    public void Empty()
    {
        CurrentInventoryItem = null;
    }

    private void UpdateUI()
    {
        if (ui == null)
            return;

        if (currentInventoryItem == null)
            ui.SetText(string.Empty);
        else
            ui.SetText(currentInventoryItem.ItemName);
    }
}
