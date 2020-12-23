using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerDestroy : MonoBehaviour
{
    //Collider2D enemy;
   
    bool playerIsHit;

    HealthManager healthManager;

    // Start is called before the first frame update
    void Start()
    {

       
        playerIsHit = false;

       

        healthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
    }
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !playerIsHit)
        {

            playerIsHit = true;
            if (healthManager !=null)
            {
                 healthManager.TakeDamage(20);
            }
            
           
        }
        StartCoroutine (PlayerisHit());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !playerIsHit)
        {

            playerIsHit = true;
            if (healthManager != null)
            {
                healthManager.TakeDamage(20);
            }


        }
        StartCoroutine(PlayerisHit());

    }

    IEnumerator PlayerisHit()
    {
        yield return new WaitForSeconds(1);
        playerIsHit = false;
    }

    


}
