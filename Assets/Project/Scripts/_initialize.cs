using UnityEngine;

namespace CognitiveFire
{
    // This namespace can be used to group related classes and avoid naming conflicts
    public class MainScript : MonoBehaviour
    {
        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            Debug.Log("CognitiveFire is awakening.");
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            Debug.Log("CognitiveFire is starting.");
        }

        // Update is called once per frame
        private void Update()
        {

        }

        // Called every fixed framerate frame
        private void FixedUpdate()
        {

        }

        // Called every frame after all Update functions have been called
        private void LateUpdate()
        {

        }
    }

}
