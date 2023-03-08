using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
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
        if (this.name == "TeleporterLeft")
        {
            Debug.Log("left");
            col.gameObject.transform.position = new Vector2(19.5f, 11.5f);
        }
        if (this.name == "TeleporterRight")
        {
            Debug.Log("right");
            col.gameObject.transform.position = new Vector2(-0.5f, 11.5f);
        }
    }
}
