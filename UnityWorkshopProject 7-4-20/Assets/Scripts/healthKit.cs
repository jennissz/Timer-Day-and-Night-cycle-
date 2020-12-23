using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthKit : MonoBehaviour
{

      HealthManager healthManager;

      

      bool healthAdded;
    // Start is called before the first frame update
    void Start()
    {
        healthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
        

        healthAdded = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        

        if (other.gameObject.tag == "Player" && healthAdded)
        {
            healthAdded = false;
            Debug.Log("Health added!");
            
            
            if (healthManager != null)
            {
                  healthManager.addHealth(50);

            }

            destroyPlayer();


        }
    }

    void destroyPlayer()
    {

         gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
         gameObject.GetComponentInChildren<Collider2D>().enabled = false;
         
    }

}
