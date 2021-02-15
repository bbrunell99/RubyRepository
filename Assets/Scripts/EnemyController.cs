using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;

    float timer;
    int direction = 1;
    Animator animator;
    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime; // time before reversing in the opposite direction
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if(!broken)
        {
            return;
        }
        Debug.Log("Timer = " + timer);
        timer -= Time.deltaTime; // decrement timer and check to see if it is less than 0, if so, reset timer and change direction
        if(timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }
   
    void FixedUpdate()
    {
        if(!broken)
        {
            return;
        }
        Vector2 position = rigidbody2D.position;
        if(vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
     

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if(player != null)
        {
            player.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        animator.SetTrigger("Fixed");
        broken = false;
        rigidbody2D.simulated = false;
      
    }
}
