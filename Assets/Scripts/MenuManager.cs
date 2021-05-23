using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Canvas homeMenuScreen;
    public Canvas optionsMenuScreen;

    // Start is called before the first frame update
    void Start()
    {
        optionsMenuScreen.enabled = false;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void showOptionsMenu()
    {
        homeMenuScreen.enabled = false;
        optionsMenuScreen.enabled = true;
    }
}
