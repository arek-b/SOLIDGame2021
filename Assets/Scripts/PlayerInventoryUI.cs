using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerInventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;

    public void SetText(string text)
    {
        itemNameText.text = text;
    }
}