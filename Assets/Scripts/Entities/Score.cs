using UnityEngine;

public class Score : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            AudioManager.instance.Play("coin");
            DataStoreManager.store.starsCounter += 1;
            Destroy(gameObject);
        }
    }
}
