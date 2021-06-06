using UnityEngine;

public class DataStoreManager : MonoBehaviour
{
    public static DataStoreManager store;

    public bool pieMenuOpenedStatus = false;

    private void Awake()
    {
        store = this;
    }
}
