
using UnityEngine;
using UnityEngine.Events;

public class Keycard : MonoBehaviour
{
    public UnityEvent OnKeycardPickup;

    public void Pickup()
    {
        OnKeycardPickup.Invoke();
        Destroy(gameObject);
    }
}
