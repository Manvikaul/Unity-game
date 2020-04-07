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
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
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
        if(currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
        getHealth();
    }

    void Movement()
    {
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
