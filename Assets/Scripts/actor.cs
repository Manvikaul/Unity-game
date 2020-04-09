using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actor : MonoBehaviour
{
    public float speed;
    Animator anim;
    public Image[] heart;
    public int maxHealth;
    public int currentHealth;
    public GameObject sword;
    public float thrustPower;
    public bool canMove;
    public bool canAttack;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        canAttack = true;
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        getHealth();
    }

    void getHealth()
    {
        for(int i=0;i<heart.Length;i++)
        {
            heart[i].gameObject.SetActive(false);
        }
        for(int i=0;i<currentHealth;i++)
        {
            heart[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
        if(currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
        getHealth();
    }

    void Attack()
    {
        if (!canAttack)
            return;
        canMove = false;
        canAttack = false;
        GameObject newSword = Instantiate(sword, transform.position, sword.transform.rotation);

        if(currentHealth==maxHealth)
        {
            newSword.GetComponent<Sword>().special=true;
            canMove = true;
            thrustPower = 500;
        }

        #region //Sword Direction
        int swordDir = anim.GetInteger("dir");
        anim.SetInteger("attackDir", swordDir);
        if(swordDir==0)
        {  
            newSword.transform.Rotate(0, 0, 0);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * thrustPower);
        }
        else if(swordDir==1)
        { 
            newSword.transform.Rotate(0, 0, 180);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.up * -thrustPower);
        }
        else if (swordDir == 2)
        { 
            newSword.transform.Rotate(0, 0, 90);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * -thrustPower);
        }
        else if (swordDir == 3)
        {
            newSword.transform.Rotate(0, 0, -90);
            newSword.GetComponent<Rigidbody2D>().AddForce(Vector2.right * thrustPower);
        }
        #endregion
        
    }
    void Movement()
    {
        if (!canMove)
            return;

        if(Input.GetKey(KeyCode.W))
        { transform.Translate(0, speed * Time.deltaTime, 0); anim.SetInteger("dir", 0); anim.speed = 1; }

        else if (Input.GetKey(KeyCode.S))
        { transform.Translate(0, -speed * Time.deltaTime, 0); anim.SetInteger("dir", 1); anim.speed = 1; }

        else if (Input.GetKey(KeyCode.A))
        { transform.Translate( -speed * Time.deltaTime,0, 0); anim.SetInteger("dir", 2); anim.speed = 1; }

        else if (Input.GetKey(KeyCode.D))
        { transform.Translate( speed * Time.deltaTime,0, 0); anim.SetInteger("dir", 3); anim.speed = 1; }

        else
        { anim.speed = 0; }
    }
}
