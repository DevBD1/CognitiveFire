
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Ilumisoft.HealthSystem;


public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private Transform vfxHitG;
    [SerializeField] private Transform vfxHitR;
    private Rigidbody brigidbody;
    private Transform owner;
    [SerializeField] private int damage = 20;
private Collider myCol;

    private void Awake()
    {
        brigidbody = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();
        
    }
    public void Initialize(Transform owner) { this.owner = owner; if (myCol == null) myCol = GetComponent<Collider>(); foreach (var c in owner.GetComponentsInChildren<Collider>()) { if (c) Physics.IgnoreCollision(myCol, c, true); } }

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
            Destroy(gameObject);
        }
        else
        {
            // Non-target hit program logic
            Instantiate(vfxHitR, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}


