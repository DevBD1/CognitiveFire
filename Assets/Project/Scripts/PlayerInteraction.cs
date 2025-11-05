
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
            Interact();
            _input.interact = false;
        }
    }

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance, interactionLayer))
        {
            Door door = hit.collider.GetComponent<Door>();
            if (door != null && levelController != null)
            {
                if (door == levelController.coreDoor)
                {
                    if (levelController.hasCoreKeycard)
                    {
                        door.OpenDoor();
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
                keycard.OnKeycardPickup.Invoke();
                keycard.gameObject.SetActive(false);
            }
        }
    }
}
