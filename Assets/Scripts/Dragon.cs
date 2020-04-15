using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    Animator anim;
    public float speed;
    int dir;
    float dirTimer = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        dir = Random.Range(0, 4);

    }

    // Update is called once per frame
    void Update()
    {
        dirTimer -= Time.deltaTime;
        if(dirTimer<=0)
        {
            dirTimer = 0.7f;
            dir = Random.Range(0, 4);
        }
        Movement();
    }

    void Movement()
    {
        if(dir==0)
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
}
