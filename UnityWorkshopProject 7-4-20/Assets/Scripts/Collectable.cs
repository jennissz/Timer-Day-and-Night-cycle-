using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collectable : MonoBehaviour
{


    HealthManager healthManager;



    // Start is called before the first frame update
    void Start()
    {
        healthManager = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == "Player")
        {
            if (healthManager != null)
            {
                healthManager.collected();
            }

            Destroy(gameObject);

        }
    }
}
