using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crab : MonoBehaviour
{
    public int health;
    public GameObject particleEffect;
    SpriteRenderer spriteRenderer;
    int direction;
    float timer = 1.5f;
    public float speed;
    public Sprite facingUp;
    public Sprite facingDown;
    public Sprite facingRight;
    public Sprite facingLeft;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        direction = Random.Range(0,4);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            timer = 1.5f;
            direction = Random.Range(0, 4);
        }
        Movement();
    }

    public void Movement()
    {
        if(direction==0)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            spriteRenderer.sprite = facingDown;
        }
        else if(direction==1)
        {
            transform.Translate( -speed * Time.deltaTime,0, 0);
            spriteRenderer.sprite = facingLeft;
        }
        else if (direction == 2)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            spriteRenderer.sprite = facingRight;
        }
        else if (direction == 3)
        {
            transform.Translate(0,speed * Time.deltaTime, 0);
            spriteRenderer.sprite = facingUp;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Sword")
        {
            health--;
            if (health <= 0)
            {
                Instantiate(particleEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
                col.GetComponent<Sword>().CreateParticle();
                GameObject.FindGameObjectWithTag("Player").GetComponent<actor>().canAttack = true;
                Destroy(col.gameObject);
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            health--;
            if(!col.gameObject.GetComponent<actor>().iniframes)
            {
                col.gameObject.GetComponent<actor>().currentHealth--;
                col.gameObject.GetComponent<actor>().iniframes = true;
            }
            col.gameObject.GetComponent<actor>().currentHealth--;
            if(health<=0)
            {
                Instantiate(particleEffect, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        if(col.gameObject.tag=="Wall")
        {
            direction--;
            if (direction <= 0)
                direction = 3;
        }
    }
}
