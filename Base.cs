using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Base : MonoBehaviour
{
    [SerializeField]
    private float health, maxHealth = 100f;
    private bool isAlive = true;
    public Image healthBar;
    SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {
        health = maxHealth;
    }
    public void Damage(int amount)
    {
        health -= amount;
        AudioManager.instance.PlaySound(1);
        if(health<=0)
        { Destroyed(); }
    }
    void Destroyed()
    {
        isAlive = false;
        sr.color = new Color(0, 0, 0, 0.25f);
    }
    public bool possibleTarget()
    {
        if (isAlive)
        {
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / maxHealth;
    }
    public void ResetHealth()
    {
        health = maxHealth;
        isAlive = true;
        sr.color = Color.white;
    }
}
