using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private Image healthBar;

    void Start()
    {
        healthBar = GameObject.Find("HealthSlider").transform.GetChild(0).GetComponent<Image>();
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Destroy(gameObject);
            healthBar.fillAmount = healthBar.fillAmount + 0.60f;
        }
    }
}
