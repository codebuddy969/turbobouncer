using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    private Image energyBar;

    void Start()
    {
        energyBar = GameObject.Find("EnergySlider").transform.GetChild(0).GetComponent<Image>();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
