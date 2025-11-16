using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Start Button test");
        //TODO
    }
    public void OpenOptions()
    {
        Debug.Log("選項按鈕被點擊！(尚未實作)");
        //TODO
    }
    public void QuitGame()
    {
        Debug.Log("Exit Game！");
        Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}