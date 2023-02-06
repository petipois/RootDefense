using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Baddie : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    private float health, maxHealth = 100f;
    [SerializeField]
    private int damageAmount = 5, value = 10;
    public Image healthImage;
    public GameObject fx;
    public TextMeshProUGUI damageText;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        healthImage.fillAmount = health/maxHealth;
    }

    public void AssignTarget(Base target, float speed)
    {
        if (target)
        {

            rb.AddForce((target.transform.position - transform.position).normalized * speed, ForceMode2D.Impulse);
        }
     
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }
    void Die()
    {        
        BaseGameManager.instance.AddGold(value);
        AudioManager.instance.PlaySound(2);
        Instantiate(fx, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Base"))
        {
            collision.gameObject.GetComponent<Base>().Damage(damageAmount);
            Destroy(gameObject);
        }

    }
}
