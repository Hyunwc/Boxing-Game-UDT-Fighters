using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [Header("Health")]
    public float startHealth = 100;
    private float health;
    public Image healthBar;

    public GameObject attackCollider;
    public float damage = 10;
    // Start is called before the first frame update
    void Start()
    {
        health = startHealth;
        healthBar.fillAmount = health / startHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
            // �� ������Ʈ�� Player �±׸� ������ ������
            if (other.CompareTag("Player"))
            {
                // ���� ���� �ȿ� ���Ա� ������ �� ������Ʈ�� HP�� ����
                Enemy enemy = other.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
            }
        
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        healthBar.fillAmount = health / startHealth;
       
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        // ���� �׾��� �� ����Ǵ� �ڵ�
        Destroy(gameObject);
    }
}
