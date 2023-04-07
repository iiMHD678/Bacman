using Bacman;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    protected Entities player;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.AddComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
