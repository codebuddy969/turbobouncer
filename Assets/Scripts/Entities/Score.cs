using UnityEngine;

public class Score : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Player")
        {
            Destroy(gameObject);
        }
    }
}
