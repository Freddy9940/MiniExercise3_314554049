using UnityEngine;
using TMPro;
public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI goldText;
    public TextMeshProUGUI HPText;
    public static HUDController instance;
    public LevelManager levelManager;

    void Awake()
    {
        instance = this;
        if (levelManager == null)
        {
            levelManager = FindFirstObjectByType<LevelManager>();
        }
    }
    void Start()
    {
        UpdateGoldText();
        UpdateHPText();
    }

    void Update()
    {
        
    }

    public void UpdateGoldText()
    {
        goldText.text = "GOLD : " + levelManager.playerGold.ToString();
    }

    public void UpdateHPText()
    {
        HPText.text = "HP : " + levelManager.playerHP.ToString();
    }
}
