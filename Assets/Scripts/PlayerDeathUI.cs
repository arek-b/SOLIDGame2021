using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Displays text on Player's death.
/// </summary>
public class PlayerDeathUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        text.gameObject.SetActive(false);
    }

    public void ShowText()
    {
        text.gameObject.SetActive(true);

    }

    public void HideText()
    {
        text.gameObject.SetActive(false);

    }
}
