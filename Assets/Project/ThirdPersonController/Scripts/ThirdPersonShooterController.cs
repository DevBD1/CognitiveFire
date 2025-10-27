/* Note: We followed Code Monkey's (YouTube) tutorial on implementing a third-person shooter controller with aiming functionality.
 */
using UnityEngine;
using Cinemachine;
using CognitiveFire;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera shooterCamera;
    [SerializeField] private GameObject crossair;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimingSensitivity;

    private ThirdPersonController thirdPersonController;
    private CognitiveFireInputs cognitiveFireInputs;

    private void Awake()
    {
        cognitiveFireInputs = GetComponent<CognitiveFireInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        if (cognitiveFireInputs.aim)
        {
            shooterCamera.gameObject.SetActive(true);
            shooterCamera.Priority = 20; // Higher priority to switch to shooter camera
            thirdPersonController.SetSensitivity(aimingSensitivity);
            crossair.SetActive(true); // Show crossair when aiming
        }
        else
        {
            shooterCamera.gameObject.SetActive(false);
            shooterCamera.Priority = 0; // Lower priority to switch back to default camera
            thirdPersonController.SetSensitivity(normalSensitivity);
            crossair.SetActive(false); // Hide crossair when not aiming
        }
    }
}