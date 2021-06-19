using UnityEngine;
using System.Collections;
public class Win : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            Destroy(gameObject);

            Hashtable parameters = new Hashtable();

            parameters["count"] = 11;

            EventsManager.current.winPopupAction(parameters, () => { Time.timeScale = 0; });


        }
    }
}
