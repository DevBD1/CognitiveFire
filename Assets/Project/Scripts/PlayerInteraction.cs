
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactionDistance = 3f;
    public LayerMask interactionLayer;
    public Level01Controller levelController;

    private CognitiveFire.CognitiveFireInputs _input;

    private void Start()
    {
        _input = GetComponent<CognitiveFire.CognitiveFireInputs>();
    }

    void Update()
    {
        if (_input.interact)
        {
            Debug.Log("Interact key pressed.");
            Interact();
            _input.interact = false;
        }
    }

    void Interact()
    {
        Debug.Log("Performing interaction raycast.");
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactionLayer))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            Door door = hit.collider.GetComponent<Door>();
            if (door != null && levelController != null)
            {
                Debug.Log("Found Door component.");
                if (door == levelController.coreDoor)
                {
                    if (levelController.hasCoreKeycard)
                    {
                        door.OpenDoor();
                    }
                    else
                    {
                        Debug.Log("Core door requires keycard.");
                    }
                }
                else
                {
                    door.OpenDoor();
                }
            }

            Keycard keycard = hit.collider.GetComponent<Keycard>();
            if (keycard != null)
            {
                Debug.Log("Found Keycard component.");
                keycard.OnKeycardPickup.Invoke();
                keycard.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.Log("Interaction raycast did not hit anything on the interaction layer.");
        }
    }
}
