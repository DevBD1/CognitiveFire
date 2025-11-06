using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 6f; // 3f
    public LayerMask interactableLayer;
    public Transform interactionSource; // Interaction ray'in cast'lanacagi nokta (player's eyes/chest) spawn bullet position gibi
    public GameObject interactionPrompt; // UI prompt to show when looking at an interactable
    private Camera mainCamera;
    private GameObject lastLookedAtObject = null;

    void Start()
    {
        mainCamera = Camera.main;

        if (mainCamera == null)
        {
            Debug.LogError("PlayerInteraction: Main Camera not found. Make sure your main camera is tagged with 'MainCamera'.");
        }

        if (interactionSource == null)
        {
            Debug.LogError("PlayerInteraction: Interaction Source is not assigned. Please assign a Transform (e.g., the player's head) to this field in the Inspector.");
        }
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (mainCamera == null || interactionSource == null)
        {
            return; // Zorunlu objeler atanmadiysa iptal
        }

        // --- Step 1: Find the point the player is looking at in the center of the screen ---
        Ray screenCenterRay = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit screenHit;
        Vector3 targetPoint;
        if (Physics.Raycast(screenCenterRay, out screenHit, 100f, ~LayerMask.GetMask("Player"))) // Raycast into the scene, ignore the player layer
        {
            targetPoint = screenHit.point;
        }
        else
        {
            targetPoint = screenCenterRay.GetPoint(100f); // If we hit nothing, just aim 100m in front
        }

        // --- Step 2: Cast a new ray from the player's interaction source towards the target point ---
        Vector3 direction = targetPoint - interactionSource.position;
        Ray interactionRay = new Ray(interactionSource.position, direction);
        RaycastHit interactionHit;

        // Draw a visible ray in the Scene view for debugging
        Debug.DrawRay(interactionSource.position, direction.normalized * interactionDistance, Color.green);
        GameObject currentLookedAtObject = null;

        if (Physics.Raycast(interactionRay, out interactionHit, interactionDistance))
        {
            currentLookedAtObject = interactionHit.collider.gameObject;
        }

        // Log a message only when the object under the cursor changes

        if (currentLookedAtObject != lastLookedAtObject)
        {
            if (currentLookedAtObject != null)
            {
                Debug.Log("Looking at: " + currentLookedAtObject.name + " on layer: " + LayerMask.LayerToName(currentLookedAtObject.layer));
            }
            else
            {
                Debug.Log("Stopped looking at any object.");
            }
        }

        // Handle interaction logic if we are looking at an object on the correct layer
        bool isInteractable = currentLookedAtObject != null && (interactableLayer.value & (1 << currentLookedAtObject.layer)) > 0;

        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(isInteractable);
        }

        if (isInteractable)
        {
            if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
            {
                Debug.Log("'E' key pressed. Attempting to interact with: " + currentLookedAtObject.name);

                Keycard keycard = currentLookedAtObject.GetComponent<Keycard>();

                if (keycard != null)
                {
                    Debug.Log("Keycard component found. Calling Pickup() on: " + currentLookedAtObject.name);
                    keycard.Pickup();
                }
                else
                {
                    Debug.LogWarning("Interactable object " + currentLookedAtObject.name + " does not have a Keycard component.");
                }
            }
        }

        // Update the tracker for the next frame
        lastLookedAtObject = currentLookedAtObject;
    }
}