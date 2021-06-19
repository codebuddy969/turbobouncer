using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    private Image energyBar;

    void Start()
    {
        energyBar = GameObject.Find("EnergySlider").transform.GetChild(0).GetComponent<Image>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            EventsManager.current.turboJumpNotificationShow();
            EventsManager.current.jumpMultiplierChange();
            Destroy(gameObject);
        }
    }
}
