using UnityEngine;

public class Fire : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
        {
            EventsManager.current.ignitePlayer();
            Destroy(gameObject);
        }
    }
}
