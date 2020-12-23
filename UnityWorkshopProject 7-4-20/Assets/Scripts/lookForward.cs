using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookForward : MonoBehaviour
{
    public Transform sightStart, sightEnd;
    private bool collision;
    public string layer = "Solid";
    public bool needsCollision = true;

   

    // Update is called once per frame
    void Update()
    {
        collision = Physics2D.Linecast(sightStart.position, sightEnd.position,
              1 << LayerMask.NameToLayer(layer));
        Debug.DrawLine(sightStart.position, sightEnd.position, Color.green);

        if (collision == needsCollision)
        {
            transform.localScale = new Vector3(transform.localScale.x == 1 ? -1 : 1, 1, 1);
        }

    }
}
