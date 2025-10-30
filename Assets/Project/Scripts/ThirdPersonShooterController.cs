/* Note: We followed Code Monkey's (YouTube) tutorial on implementing a third-person shooter controller with aiming functionality.
 */
using UnityEngine;
using Cinemachine;
using CognitiveFire;
using UnityEngine.InputSystem;

public enum ShootingMode
{
    Raycast,
    Projectile
}

public class ThirdPersonShooterController : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera shooterCamera;
    [SerializeField] private GameObject crossair;
    [SerializeField] private float normalSensitivity;

    [SerializeField] private float aimingSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform debugTransform;
    [SerializeField] private Transform bpj; //pfBulletProjectile
    [SerializeField] private Transform sbp; //spawnBulletPosition

    [SerializeField] private Transform vfxHitG;
    [SerializeField] private Transform vfxHitR;

    [SerializeField] private ShootingMode shootingMode = ShootingMode.Raycast;


    private Animator animator;

    private ThirdPersonController thirdPersonController;
    private CognitiveFireInputs cognitiveFireInputs;

    private void Awake()
    {
        cognitiveFireInputs = GetComponent<CognitiveFireInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            shootingMode = shootingMode == ShootingMode.Raycast ? ShootingMode.Projectile : ShootingMode.Raycast;
            Debug.Log($"Shooting Mode: {shootingMode}");
        }

        Vector3 mouseWorldPosition = Vector3.zero;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        //Ray ray = new Ray(sbp.position, Camera.main.transform.forward);
        Transform hitTransform = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            debugTransform.position = raycastHit.point;
            //Vector3 aimTarget = raycastHit.point;
            //Debug.DrawLine(ray.origin, aimTarget, Color.red);
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (cognitiveFireInputs.aim)
        {
            shooterCamera.gameObject.SetActive(true);
            shooterCamera.Priority = 20; // Higher priority to switch to shooter camera
            thirdPersonController.SetSensitivity(aimingSensitivity);
            crossair.SetActive(true); // Show crossair when aiming
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1),1f,Time.deltaTime*10f));

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
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1),0f,Time.deltaTime*10f));
        }

        if (cognitiveFireInputs.shoot)
        {
            // --- Raycast Mode ---
            if (hitTransform != null)
            {
                Debug.Log($"Ray hit {hitTransform.name}");
                Vector3 hitPoint = raycastHit.point;
                Quaternion hitRot = Quaternion.LookRotation(raycastHit.normal);

                if (hitTransform.GetComponent<BulletTarget>() != null)
                {
                    // Target hit program logic
                    Instantiate(vfxHitG, hitPoint, hitRot);
                }
                else
                {
                    // Non-target hit program logic
                    Instantiate(vfxHitR, hitPoint, hitRot);
                }
            }
            if (shootingMode == ShootingMode.Projectile)
            {
                // --- Projectile Mode ---
                Vector3 aimDir = (mouseWorldPosition - sbp.position).normalized;
                Instantiate(bpj, sbp.position, Quaternion.LookRotation(aimDir, Vector3.up));
            }
            cognitiveFireInputs.shoot = false;
        }
    }
}