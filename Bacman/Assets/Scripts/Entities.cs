using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bacman
{
    public class Entities : MonoBehaviour
    {
        public float speed { get; set; }
        public float roundedPosX { get; set; }
        public float roundedPosY { get; set; }
        public int MoveX { get; set; }
        public int MoveY { get; set; }

        //Om tangent blivit nedtryckt
        public bool rightKeyIsPushed { get; set; }
        public bool leftKeyIsPushed { get; set; }
        public bool upKeyIsPushed { get; set; }
        public bool downKeyIsPushed { get; set; }

        //Om playerPos = HitTurnObj
        public bool canMoveRight { get; set; }
        public bool canMoveLeft { get; set; }
        public bool canMoveUp { get; set; }
        public bool canMoveDown { get; set; }

        public GameObject[] TurnObject { get; set; }
        public Rigidbody2D rb { get; set; }
        public Entities()
        {
            this.rightKeyIsPushed = false;
            this.leftKeyIsPushed = false;
            this.upKeyIsPushed = false;
            this.downKeyIsPushed = false;
            this.speed = 400f;
            this.MoveX = 0;
            this.MoveY = 0;
            this.canMoveRight = false;
            this.canMoveLeft = false;
            this.canMoveUp = false;
            this.canMoveDown = false;
            

        }

        // Start is called before the first frame update
        void Findturns()
        {
            TurnObject = GameObject.FindGameObjectsWithTag("TurnObject");
            this.rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Movement()
        {
            //Avrunda position till två decimaler
            this.roundedPosY = Mathf.Round(transform.position.y * 10) / 10;
            this.roundedPosX = Mathf.Round(transform.position.x * 10) / 10;

            if (Input.GetKeyDown("right"))
            {
                this.rightKeyIsPushed = true;
                this.leftKeyIsPushed = false;
                this.upKeyIsPushed = false;
                this.downKeyIsPushed = false;
            }
            if (Input.GetKeyDown("left"))
            {
                this.leftKeyIsPushed = true;
                this.rightKeyIsPushed = false;
                this.upKeyIsPushed = false;
                this.downKeyIsPushed = false;
            }
            if (Input.GetKeyDown("up"))
            {
                this.leftKeyIsPushed = false;
                this.rightKeyIsPushed = false;
                this.upKeyIsPushed = true;
                this.downKeyIsPushed = false;
            }
            if (Input.GetKeyDown("down"))
            {
                this.leftKeyIsPushed = false;
                this.rightKeyIsPushed = false;
                this.upKeyIsPushed = false;
                this.downKeyIsPushed = true;
            }

            //Check all TurnObjects
            foreach (GameObject hitObject in TurnObject)
            {
                if (this.roundedPosY == hitObject.transform.position.y && roundedPosX == hitObject.transform.position.x && rightKeyIsPushed)
                {
                    this.canMoveRight = true;
                    this.canMoveLeft = false;
                    this.canMoveUp = false;
                    this.canMoveDown = false;
                }

                if (this.roundedPosY == hitObject.transform.position.y && roundedPosX == hitObject.transform.position.x && leftKeyIsPushed)
                {
                    this.canMoveLeft = true;
                    this.canMoveRight = false;

                    this.canMoveUp = false;
                    this.canMoveDown = false;
                }

                if (this.roundedPosY == hitObject.transform.position.y && roundedPosX == hitObject.transform.position.x && upKeyIsPushed)
                {
                    this.canMoveUp = true;
                    this.canMoveRight = false;
                    this.canMoveLeft = false;
                    this.canMoveDown = false;
                }

                if (this.roundedPosY == hitObject.transform.position.y && roundedPosX == hitObject.transform.position.x && downKeyIsPushed)
                {
                    this.canMoveDown = true;
                    this.canMoveRight = false;
                    this.canMoveLeft = false;
                    this.canMoveUp = false;
                }

                // Changes direction according to above block
                if (this.canMoveRight == true)
                {
                    this.MoveX = 1;
                    this.MoveY = 0;
                }
                if (this.canMoveLeft == true)
                {
                    this.MoveX = -1;
                    this.MoveY = 0;
                }
                if (this.canMoveUp == true)
                {
                    this.MoveY = 1;
                    this.MoveX = 0;
                }
                if (this.canMoveDown == true)
                {
                    this.MoveY = -1;
                    this.MoveX = 0;
                }



                this.rb.velocity = new Vector2(this.MoveX * this.speed * Time.deltaTime, this.MoveY * speed * Time.deltaTime);
            }
        }
    }

    class Player : Entities
    {
        
        public GameObject Pacman { get; set; }
        Player()
        {
            this.Pacman = new GameObject("Pacman", typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(CircleCollider2D));
            Instantiate(this.Pacman);
        }
        
    }

    class Ghost : Entities
    {

    }
}
