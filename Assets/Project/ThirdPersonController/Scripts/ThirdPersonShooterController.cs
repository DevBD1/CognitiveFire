/* Note: We followed Code Monkey's (YouTube) tutorial on implementing a third-person shooter controller with aiming functionality.
 */
using UnityEngine;
using Cinemachine;
using CognitiveFire;
using UnityEngine.InputSystem;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera shooterCamera;
    [SerializeField] private GameObject crossair;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimingSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    private ThirdPersonController thirdPersonController;
    private CognitiveFireInputs cognitiveFireInputs;

    private void Awake()
    {
        cognitiveFireInputs = GetComponent<CognitiveFireInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            //Vector3 aimTarget = raycastHit.point;
            //Debug.DrawLine(ray.origin, aimTarget, Color.red);
            mouseWorldPosition = raycastHit.point;
        }

        if (cognitiveFireInputs.aim)
        {
            shooterCamera.gameObject.SetActive(true);
            shooterCamera.Priority = 20; // Higher priority to switch to shooter camera
            thirdPersonController.SetSensitivity(aimingSensitivity);
            crossair.SetActive(true); // Show crossair when aiming
            thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            shooterCamera.gameObject.SetActive(false);
            shooterCamera.Priority = 0; // Lower priority to switch back to default camera
            thirdPersonController.SetSensitivity(normalSensitivity);
            crossair.SetActive(false); // Hide crossair when not aiming
            thirdPersonController.SetRotateOnMove(true);
        }
    }
}