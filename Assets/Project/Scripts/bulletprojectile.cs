
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitG;
    [SerializeField] private Transform vfxHitR;
    private Rigidbody brigidbody;

    private void Awake()
    {
        brigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        float speed = 40f;
        brigidbody.linearVelocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponentInParent<BulletTarget>();
        if (target!=null)
        {
            target.TakeDamage(25);
            // Target hit program logic
            Instantiate(vfxHitG, transform.position, Quaternion.identity);
        }
        else
        {
            // Non-target hit program logic
            Instantiate(vfxHitR, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}


