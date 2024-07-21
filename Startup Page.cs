using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupPage : MonoBehaviour
{
    private void Start()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Playgame()
    {
        int CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
        SceneManager.LoadScene("Level "+ CurrentLevel);
    }
}
