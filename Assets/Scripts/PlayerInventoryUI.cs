using System.Collections;
using UnityEngine;
using TMPro;

/// <summary>
/// Displays information about current inventory state on the screen.
/// </summary>
public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;

    public void SetText(string text)
    {
        itemNameText.text = text;
    }
}