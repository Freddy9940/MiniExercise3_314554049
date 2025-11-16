using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int playerGold = 200;
    public int playerHP = 20;    
    public string levelDestination;

    void Awake()
    {
    }
  void Start()
    {
        HUDController.instance.levelManager = this;
        HUDController.instance.UpdateGoldText();
        HUDController.instance.UpdateHPText();
    }

    
    void Update()
    {
        
    }

    public void AddGold(int value)
    {
        playerGold += value;
    }
}
