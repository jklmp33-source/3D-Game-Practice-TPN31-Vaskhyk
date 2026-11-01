using UnityEngine;

public class KillCounter : MonoBehaviour
{
    public int kills = 0;
    public int killsPerUpgrade = 10;

    [Header("UI")]
    public UpgradeUI upgradeUI;

    private int nextUpgradeAt = 10;

    void Start()
    {
        nextUpgradeAt = killsPerUpgrade;
    }

    public void AddKill()
    {
        kills++;

        if (kills >= nextUpgradeAt)
        {
            nextUpgradeAt += killsPerUpgrade;
            if (upgradeUI != null) upgradeUI.ShowUpgradeMenu();
        }
    }
}