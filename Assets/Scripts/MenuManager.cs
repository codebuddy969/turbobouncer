using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    GameDataConfig config;

    public Canvas homeMenuScreen;
    public Canvas optionsMenuScreen;

    public Slider musicSlider;
    public Slider effectsSlider;

    void Start()
    {
        config = DBOperationsController.element.LoadSaving();

        musicSlider.value = config.musicLevel;
        effectsSlider.value = config.effectsLevel;

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

    public void musicValueChange(Slider slider)
    {
        config.musicLevel = slider.value;

        DBOperationsController.element.CreateSaving(config);
    }

    public void fxValueChange(Slider slider)
    {
        config.effectsLevel = slider.value;

        DBOperationsController.element.CreateSaving(config);
    }
}
