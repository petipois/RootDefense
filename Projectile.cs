using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    public GameObject fx;
    [SerializeField]
    public float strength = 5;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3f);
    }
    public void UpgradeStrength(float amount)
    {
        strength += amount;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //play explosion clip

        if (collision.gameObject.CompareTag("Baddie"))
        {

            AudioManager.instance.PlaySound(3);
            collision.gameObject.GetComponent<Baddie>().TakeDamage(strength);
        }
        Destroy(gameObject);
    }
    
    public void Launch(Vector2 dir, float speed)
    {
        rb.AddForce(dir.normalized * speed, ForceMode2D.Impulse);
    }
}
