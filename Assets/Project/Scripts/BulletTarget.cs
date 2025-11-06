using UnityEngine;

public class BulletTarget : MonoBehaviour
{
     [SerializeField] int maxHealth=100; int health;
    void Awake() { health = maxHealth; }
     public void TakeDamage(int amt){ health-=amt; if(health<=0) Destroy(gameObject); } 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
