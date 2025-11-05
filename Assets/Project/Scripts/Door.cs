
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            isOpen = true;
            // Add animation or logic to open the door here
            // For now, we will just deactivate the object
            gameObject.SetActive(false);
        }
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}
