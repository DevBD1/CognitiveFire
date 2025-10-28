
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class bulletprojectile : MonoBehaviour
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
        float speed = 10f;
        brigidbody.velocity = transform.forward * speed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<bultar>() != null)
        {
            //hittarg
            Instantiate(vfxHitG, transform.position, Quaternion.identity);
        }
        else
        {
            //hitsomelse
            Instantiate(vfxHitG, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}


