using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    float timer = .15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<actor>().canMove = true;
            Destroy(gameObject);
        }
    }
}
