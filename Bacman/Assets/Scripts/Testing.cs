using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public GameObject tileMap;
    public GameObject pacMan;
    GameObject Scanner;
    public Sprite testsprite;
    // Start is called before the first frame update
    void Start()
    {
        this.Scanner = new GameObject("Scanner", typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(BoxCollider2D));
        Rigidbody2D rb = Scanner.GetComponent<Rigidbody2D>();
        rb.simulated = false;
        SpriteRenderer SpriteRenderer = Scanner.GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = testsprite;
        SpriteRenderer.color = Color.magenta;
    }

    // Update is called once per frame
    void Update()
    {
        Scanner.transform.position = pacMan.transform.position;
    }
}
