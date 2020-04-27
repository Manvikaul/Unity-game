using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public int sceneIndex;

   void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag=="Player")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<actor>().SaveGame();
            SceneManager.LoadScene(sceneIndex);
        }
    }
   
}
