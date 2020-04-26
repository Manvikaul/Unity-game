using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    Animator anim;
    public float speed;
    public int dir;
    float dirTimer = 1.2f;
    public int health;
    public GameObject deathParticle;
    bool canAttack;
    float attackTimer = 2f;
    public float thrustPower;
    public GameObject projectile;
    float changeTimer = 0.2f;
    bool shouldChange;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        canAttack = false;
        shouldChange = false;
    }

    // Update is called once per frame
    void Update()
    {
        dirTimer -= Time.deltaTime;
        if (dirTimer <= 0)
        {
            dirTimer = 1.2f;
            switch (dir)
            {
                case 1: dir = 0;
                    break;
                case 2:dir = 1;
                    break;
                case 3:dir = 2;
                    break;
                case 0: dir = 3;
                    break;
                default: dir = 1;
                    break;

            }


        }
        Movement();

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = 2f;
            canAttack = true;
        }

        Attack();

        if (shouldChange)
        {
            changeTimer -= Time.deltaTime;
            if (changeTimer <= 0)
            {
                shouldChange = false;
                changeTimer = 0.2f;
            }
        }
    }

    void Attack()
    {
        if (!canAttack)
            return;

        canAttack = false;
        if (dir == 0)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
        }
        else if (dir == 1)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
        }
        else if (dir == 2)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
        }
        else if (dir == 3)
        {
            GameObject newProjectile = Instantiate(projectile, transform.position, transform.rotation);
            newProjectile.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
        }

    }

    void Movement()
    {
        if (dir == 0)
        {
            transform.Translate(0, speed * Time.deltaTime, 0);
            anim.SetInteger("dir", dir);
        }
        else if (dir == 1)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", dir);
        }
        else if (dir == 2)
        {
            transform.Translate(0, -speed * Time.deltaTime, 0);
            anim.SetInteger("dir", dir);
        }
        else if (dir == 3)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            anim.SetInteger("dir", dir);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Sword")
        {
            health--;
            col.gameObject.GetComponent<Sword>().CreateParticle();
            GameObject.FindGameObjectWithTag("Player").GetComponent<actor>().canAttack = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<actor>().canMove = true;
            Destroy(col.gameObject);
            if (health <= 0)
            {
                Instantiate(deathParticle, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            health--;
            if (!col.gameObject.GetComponent<actor>().iniframes)
            {
                col.gameObject.GetComponent<actor>().currentHealth--;
                col.gameObject.GetComponent<actor>().iniframes = true;
            }
            col.gameObject.GetComponent<actor>().currentHealth--;
            if (health <= 0)
            {
                Instantiate(deathParticle, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        if (col.gameObject.tag == "Wall")
        {
            if (shouldChange)
                return;

            if (dir == 0)
                dir = 2;
            else if (dir == 1)
                dir = 3;
            else if (dir == 2)
                dir = 0;
            else if (dir == 3)
                dir = 1;

            shouldChange = true;

        }
    }
}

