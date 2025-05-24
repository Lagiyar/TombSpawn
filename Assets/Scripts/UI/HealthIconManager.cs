using UnityEngine;
using UnityEngine.UI;

public class HealthIconManager : MonoBehaviour
{
    public Image[] healthIcons;
    public int maxHealthIcons = 4;
    private int currentHealthIcons;

    public void SetHealth(int health)
    {
        currentHealthIcons = Mathf.Clamp(health, 0, maxHealthIcons);
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < healthIcons.Length; i++)
        {
            healthIcons[i].gameObject.SetActive(i < currentHealthIcons);
        }
    }
}
