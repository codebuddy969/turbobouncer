using UnityEngine;
using UnityEngine.UI;
public class Bomb : MonoBehaviour
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
            AudioManager.instance.Play("explosion");

            gameObject.GetComponent<SphereCollider>().enabled = false;
            gameObject.transform.Find("Sphere002").GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.Find("Explosion").GetComponent<ParticleSystem>().Play();
            healthBar.fillAmount -= 0.40f;
            Destroy(gameObject, 3.0f);
        }
    }
}
