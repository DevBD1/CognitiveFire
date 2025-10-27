using UnityEngine;
using Cinemachine;
using CognitiveFire;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera shooterCamera;

    private CognitiveFireInputs cognitiveFireInputs;

    private void Awake()
    {
        cognitiveFireInputs = GetComponent<CognitiveFireInputs>();
    }

    private void Update()
    {
        if (cognitiveFireInputs.aim)
        {
            shooterCamera.gameObject.SetActive(true);
            shooterCamera.Priority = 20; // Higher priority to switch to shooter camera
        }
        else
        {
            shooterCamera.gameObject.SetActive(false);
            shooterCamera.Priority = 0; // Lower priority to switch back to default camera
        }
    }
}