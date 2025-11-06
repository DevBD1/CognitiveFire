using UnityEngine;
using UnityEngine.UI;

public class fhealthjbar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Slider slider; 
    public void UpgradeHealthBar(float currentValue,float maxValue)
    {
        slider.value = currentValue /maxValue;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
