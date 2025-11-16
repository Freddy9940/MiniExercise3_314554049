using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerSelectionUI : MonoBehaviour
{
    public TowerBuilder towerBuilder;
    
    public void ExitHUD()
    {
        Debug.Log("ExitHUD called");
        this.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    public void SelectTower0()
    {
        towerBuilder.towerIndex = 0;
        towerBuilder.pos.y = 1;
        towerBuilder.BuildTower();
    }

    public void SelectTower1()
    {
       towerBuilder.towerIndex = 1;
       towerBuilder.BuildTower();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
