using UnityEngine;
using TMPro;
using System.Collections;

public class CountDownManager : MonoBehaviour
{
    private CommonConfig config = new CommonConfig();

    public float timeRemaining = 20;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timeText;

    private void Start()
    {
        timerIsRunning = true;

        timeText = gameObject.GetComponent<TextMeshProUGUI>();

        GameDataConfig game_config = DBOperationsController.element.LoadSaving();

        timeRemaining = (config.platformsCount + game_config.level) * timeRemaining;

        EventsManager.current.onTimeBoostAction += addTime;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;

                Hashtable parameters = new Hashtable();

                parameters["quitButton"] = false;
                parameters["closeButton"] = false;
                parameters["optionsButtons"] = true;
                parameters["message"] = "Sorry, timer is out, want to try again ?";

                EventsManager.current.popupAction(parameters, () => { Time.timeScale = 0; });
            }
        }
    }

    public void addTime(int number)
    {
        timeRemaining = timeRemaining + number;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
