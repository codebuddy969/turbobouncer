using UnityEngine;

public class Fire : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            AudioManager.instance.Play("ignition");
            AudioManager.instance.Play("fire");
            EventsManager.current.ignitePlayer(true);
            Destroy(gameObject);
        }
    }
}
