using UnityEngine;
using System.Collections;
public class Win : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Player")
        {
            AudioManager.instance.Play("level-passed");

            Destroy(gameObject);

            int counter = DataStoreManager.store.starsCounter;

            GameDataConfig game_config = DBOperationsController.element.LoadSaving();

            Hashtable parameters = new Hashtable();

            parameters["count"] = counter;
            parameters["level"] = game_config.level;

            EventsManager.current.winPopupAction(parameters, () => { Time.timeScale = 0; });

            game_config.level += 1;
            game_config.score += counter;

            DBOperationsController.element.CreateSaving(game_config);
        }
    }
}
