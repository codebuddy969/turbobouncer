using UnityEngine;

public class Fire : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            EventsManager.current.ignitePlayer(true);
            Destroy(gameObject);
        }
    }
}
