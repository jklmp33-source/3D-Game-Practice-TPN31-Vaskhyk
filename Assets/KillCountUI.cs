using UnityEngine;
using TMPro;

public class KillCountUI : MonoBehaviour
{
    [Header("References")]
    public KillCounter killCounter;
    public TextMeshProUGUI killsText;

    void Start()
    {
        if (killCounter == null)
            killCounter = FindObjectOfType<KillCounter>();
    }

    void Update()
    {
        if (killCounter == null || killsText == null) return;
        killsText.text = $"Убийств: {killCounter.kills}";
    }
}