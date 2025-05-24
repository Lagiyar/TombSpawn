using UnityEngine;
using TMPro;

public class CollectSoulUI : MonoBehaviour
{
    [SerializeField] private TMP_Text soulText;
    private int totalSouls = 0;

    void Start()
    {
        UpdateSoulText();
        Soul.OnSoulCollected += HandleSoulCollected;
    }

    void OnDestroy()
    {
        Soul.OnSoulCollected -= HandleSoulCollected;
    }

    private void HandleSoulCollected(int value)
    {
        totalSouls += value;
        UpdateSoulText();
    }

    private void UpdateSoulText()
    {
        if (soulText != null)
            soulText.text = $"{totalSouls}";
    }
}
