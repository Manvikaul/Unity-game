using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crab : MonoBehaviour
{
    public int health;
    public GameObject particleEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
