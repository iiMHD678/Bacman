using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Bacman
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 100f;
        public float roundedPosX;
        public float roundedPosY;
        public int moveRight = 0;
        public int moveUp = 0;

        //Om playerPos = HitTurnObj
        public bool canMoveRight = false;
        public bool canMoveLeft = false;
        public bool canMoveUp = false;
        public bool canMoveDown = false;

        //Om tangent blivit nedtryckt
        public bool rightKeyIsPushed = false;
        public bool leftKeyIsPushed = false;
        public bool upKeyIsPushed = false;
        public bool downKeyIsPushed = false;

        static List<GameObject> TurnObjectsToCheck = new List<GameObject>();
        public Rigidbody2D rb;
        


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            canMoveRight = true;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("right"))
            {
                rightKeyIsPushed = true;
                leftKeyIsPushed = false;
                upKeyIsPushed = false;
                downKeyIsPushed = false;
            }
            if (Input.GetKeyDown("left"))
            {
                leftKeyIsPushed = true;
                rightKeyIsPushed = false;
                upKeyIsPushed = false;
                downKeyIsPushed = false;
            }
            if (Input.GetKeyDown("up"))
            {
                leftKeyIsPushed = false;
                rightKeyIsPushed = false;
                upKeyIsPushed = true;
                downKeyIsPushed = false;
            }
            if (Input.GetKeyDown("down"))
            {
                leftKeyIsPushed = false;
                rightKeyIsPushed = false;
                upKeyIsPushed = false;
                downKeyIsPushed = true;
            }

            //Avrunda position till två decimaler
            roundedPosY = Mathf.Round(transform.position.y * 10) / 10;
            roundedPosX = Mathf.Round(transform.position.x * 10) / 10;


            foreach (GameObject TurnObject in TurnObjectsToCheck)
            {
                if (roundedPosY == TurnObject.transform.position.y && roundedPosX == TurnObject.transform.position.x && rightKeyIsPushed)
                {
                    canMoveRight = true;
                    canMoveLeft = false;
                    canMoveUp = false;
                    canMoveDown = false;
                }

                else if (roundedPosY == TurnObject.transform.position.y && roundedPosX == TurnObject.transform.position.x && leftKeyIsPushed)
                {
                    canMoveLeft = true;
                    canMoveRight = false;
                    canMoveUp = false;
                    canMoveDown = false;
                }

                else if (roundedPosY == TurnObject.transform.position.y && roundedPosX == TurnObject.transform.position.x && upKeyIsPushed)
                {
                    canMoveUp = true;
                    canMoveRight = false;
                    canMoveLeft = false;
                    canMoveDown = false;
                }

                else if (roundedPosY == TurnObject.transform.position.y && roundedPosX == TurnObject.transform.position.x && downKeyIsPushed)
                {
                    canMoveDown = true;
                    canMoveRight = false;
                    canMoveLeft = false;
                    canMoveUp = false;
                }
            }
            

            // Changes direction according to above block
            if (canMoveRight == true)
            {
                moveRight = 1;
                moveUp = 0;
            }
            else if (canMoveLeft == true)
            {
                moveRight = -1;
                moveUp = 0;
            }
            else if (canMoveUp == true)
            {
                moveUp = 1;
                moveRight = 0;
            }
            else if (canMoveDown == true)
            {
                moveUp = -1;
                moveRight = 0;
            }



            rb.velocity = new Vector2(moveRight * speed * Time.deltaTime, moveUp * speed * Time.deltaTime);
        }

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.gameObject.tag == "TurnObject")
        //    {
        //        TurnObjectsToCheck.Add(collision.gameObject);
        //    }
        //    foreach (var item in TurnObjectsToCheck)
        //    {
        //        System.Console.WriteLine(item);
        //    }
        //}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "TurnObject")
            {
                TurnObjectsToCheck.Add(collision.gameObject);
            }
            foreach (var item in TurnObjectsToCheck)
            {
                System.Console.WriteLine(item);
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "TurnObject")
            {
                TurnObjectsToCheck.Remove(collision.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "TurnObject")
            {
                TurnObjectsToCheck.Remove(collision.gameObject);
            }
        }
    }
}
