
using UnityEngine;
using System.Collections.Generic;

public class Level01Controller : MonoBehaviour
{
    // --- Initial Trigger ---
    [Header("Initial Trigger")]
    public GameObject firstCorridorDoor;
    public TriggerVolume windowTrigger;

    // --- First Corridor ---
    [Header("First Corridor")]
    public List<GameObject> firstCorridorNPCs;
    public GameObject secondCorridorDoor;

    // --- Second Corridor ---
    [Header("Second Corridor")]
    public GameObject coreDoor;

    // --- The Core ---
    [Header("The Core")]
    public List<GameObject> coreNPCs;

    // --- Final Room ---
    [Header("Final Room")]
    public GameObject weapon;
    public TriggerVolume finalTrigger;

    private bool isFirstCorridorDoorOpened = false;
    private bool isSecondCorridorDoorOpened = false;
    private bool isCoreDoorOpened = false;

    void Start()
    {
        // --- Initial Trigger ---
        if (windowTrigger != null)
        {
            windowTrigger.OnPlayerEnter.AddListener(OpenFirstCorridorDoor);
        }

        // --- Doors ---
        if (firstCorridorDoor != null)
        {
            firstCorridorDoor.SetActive(true);
        }
        if (secondCorridorDoor != null)
        {
            secondCorridorDoor.SetActive(true);
        }
        if (coreDoor != null)
        {
            coreDoor.SetActive(true);
        }

        // --- Final Trigger ---
        if (finalTrigger != null)
        {
            finalTrigger.OnPlayerEnter.AddListener(EndLevel);
            finalTrigger.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isFirstCorridorDoorOpened && !isSecondCorridorDoorOpened)
        {
            CheckFirstCorridorNPCs();
        }

        if (isCoreDoorOpened)
        {
            CheckCoreNPCs();
        }
    }

    // --- Initial Trigger ---
    void OpenFirstCorridorDoor()
    {
        if (firstCorridorDoor != null && !isFirstCorridorDoorOpened)
        {
            firstCorridorDoor.SetActive(false);
            isFirstCorridorDoorOpened = true;
            windowTrigger.OnPlayerEnter.RemoveListener(OpenFirstCorridorDoor);
        }
    }

    // --- First Corridor ---
    void CheckFirstCorridorNPCs()
    {
        firstCorridorNPCs.RemoveAll(item => item == null);
        if (firstCorridorNPCs.Count == 0)
        {
            OpenSecondCorridorDoor();
        }
    }

    void OpenSecondCorridorDoor()
    {
        if (secondCorridorDoor != null && !isSecondCorridorDoorOpened)
        {
            secondCorridorDoor.SetActive(true);
            isSecondCorridorDoorOpened = true;
        }
    }

    // --- Second Corridor ---
    void OpenCoreDoor()
    {
        if (coreDoor != null && !isCoreDoorOpened)
        {
            coreDoor.SetActive(true);
            isCoreDoorOpened = true;
        }
    }

    // --- Final Room ---
    void EnableFinalTrigger()
    {
        if (finalTrigger != null)
        {
            finalTrigger.gameObject.SetActive(true);
        }
    }

    void EndLevel()
    {
        Debug.Log("Level Complete!");
    }
}
