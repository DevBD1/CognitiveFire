
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class Level01Controller : MonoBehaviour
{
    // --- Initial Trigger ---
    [Header("Initial Trigger")]
    public Door firstCorridorDoor;
    public TriggerVolume windowTrigger;

    // --- First Corridor ---
    [Header("First Corridor")]
    public List<GameObject> firstCorridorNPCs;
    public Door secondCorridorDoor;

    // --- Second Corridor ---
    [Header("Second Corridor")]
    public Keycard keycardForCore;
    public Door coreDoor;
    public TriggerVolume coreDoorTrigger;
    public bool hasCoreKeycard = false;

    // --- The Core ---
    [Header("The Core")]
    public List<GameObject> coreNPCs;
    public bool hasFinalKeycard = false;

    // --- Final Room ---
    [Header("Final Room")]
    public GameObject weapon;
    public TriggerVolume finalTrigger;
    public Image endgameImage;

    void Start()
    {
        // --- Initial Trigger ---
        if (windowTrigger != null)
        {
            windowTrigger.OnPlayerEnter.AddListener(UnlockFirstCorridorDoor);
        }

        // --- Keycards ---
        if (keycardForCore != null)
        {
            keycardForCore.OnKeycardPickup.AddListener(PickupCoreKeycard);
        }

        if (coreDoorTrigger != null)
        {
            coreDoorTrigger.OnPlayerEnter.AddListener(TryOpenCoreDoor);
        }

        // --- Final Trigger ---
        if (finalTrigger != null)
        {
            finalTrigger.OnPlayerEnter.AddListener(EndLevel);
            finalTrigger.gameObject.SetActive(false);
        }

        if (endgameImage != null)
        {
            endgameImage.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (firstCorridorDoor.IsOpen() && !secondCorridorDoor.IsOpen())
        {
            CheckFirstCorridorNPCs();
        }

        if (coreDoor.IsOpen())
        {
            CheckCoreNPCs();
        }
    }

    // --- Initial Trigger ---
    void UnlockFirstCorridorDoor()
    {
        if (firstCorridorDoor != null && !firstCorridorDoor.IsOpen())
        {
            firstCorridorDoor.OpenDoor();
            windowTrigger.OnPlayerEnter.RemoveListener(UnlockFirstCorridorDoor);
        }
    }

    // --- First Corridor ---
    void CheckFirstCorridorNPCs()
    {
        firstCorridorNPCs.RemoveAll(item => item == null);
        if (firstCorridorNPCs.Count == 0)
        {
            UnlockSecondCorridorDoor();
        }
    }

    void UnlockSecondCorridorDoor()
    {
        if (secondCorridorDoor != null && !secondCorridorDoor.IsOpen())
        {
            secondCorridorDoor.OpenDoor();
        }
    }

    // --- Second Corridor ---
    void PickupCoreKeycard()
    {
        hasCoreKeycard = true;
    }

    void TryOpenCoreDoor()
    {
        if (hasCoreKeycard && coreDoor != null && !coreDoor.IsOpen())
        {
            coreDoor.OpenDoor();
            coreDoorTrigger.OnPlayerEnter.RemoveListener(TryOpenCoreDoor);
        }
    }

    // --- The Core ---
    void CheckCoreNPCs()
    {
        coreNPCs.RemoveAll(item => item == null);
        if (coreNPCs.Count == 0 && !hasFinalKeycard)
        {
            hasFinalKeycard = true;
            Debug.Log("Final Keycard Obtained!");
        }
    }

    void EndLevel()
    {
        if (endgameImage != null)
        {
            endgameImage.gameObject.SetActive(true);
        }
        Debug.Log("Level Complete!");
    }
}
