using UnityEngine;
using System.Collections;

public class playerhealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    [SerializeField] private fhealthjbar healthBar; // inspector'a sürükle

    [Header("Settings")]
    public float invulnerableTime = 0.5f; // ardışık çarpışmaları engellemek için
    private bool isInvulnerable = false;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null) healthBar.UpgradeHealthBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int dmg)
    {
        if (isInvulnerable) return;

        currentHealth -= dmg;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (healthBar != null) healthBar.UpgradeHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityRoutine());
        }
    }

    private IEnumerator InvulnerabilityRoutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerableTime);
        isInvulnerable = false;
    }

    private void Die()
    {
        Debug.Log("Player died!");
        // Örnek: hareket kontrolünü devre dışı bırak
        var tpc = GetComponent<StarterAssets.ThirdPersonController>() ?? GetComponent<CognitiveFire.ThirdPersonController>() as MonoBehaviour;
        if (tpc != null) tpc.enabled = false;

        // İstersen: animasyon, respawn, game over UI vs. ekle
        // gameObject.SetActive(false);
    }
}

