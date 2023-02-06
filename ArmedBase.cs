using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedBase : Base
{
    [SerializeField]
    private Projectile projectile;
    [SerializeField]
    private float moveSpeed, speed = 3f;
    Rigidbody2D rb;
   // Vector2 movement;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(BaseGameManager.instance.hasStarted())
        {
            
            if(possibleTarget())
            {
                /*     movement.x = Input.GetAxisRaw("Horizontal");
                     movement.y = Input.GetAxisRaw("Vertical");
                     Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                     Vector2 lookDir = mousePos - rb.position;
                     float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                     rb.rotation = angle;
                */
                if (Input.GetButtonDown("Fire1"))
                {
                    if(!BaseGameManager.instance.roundCompleted())
                    {
                        Fire();
                    }
                }
            }else
            {
                BaseGameManager.instance.GameOver();
            }
        }
      
    }
    void Fire()
    {
        Projectile projInstance = Instantiate(projectile, transform.position, Quaternion.identity);
        projInstance.Launch(Camera.main.ScreenToWorldPoint(Input.mousePosition - transform.position), speed);
 
    }
 /*   private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }*/
}
