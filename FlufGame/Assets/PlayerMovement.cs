using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public GameObject gameOver;
    public Animator anim;
    public Sprite[] allSkins;

    public AudioClip sjump, sthud;

    bool gameRunning = true;
    

    public float speed;
    public float jumpForce;
    public float moveInput;

    private Rigidbody2D rb;

    public bool isGrounded,isOnRightWall, isOnLeftWall;
    public Transform playerFeet, playerRightArm, playerLeftArm;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    bool isJumping;
    bool isWalljumping;
    bool canjump;


    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameRunning = true;

        LoadFluf.clearHazards = false;
    }


    void Update()
    {
        if (isJumping)
            anim.SetBool("isJumping", true);
            
        if(isGrounded&&gameRunning)
            anim.SetBool("isJumping", false);

        isOnRightWall = Physics2D.OverlapCircle(playerRightArm.position, checkRadius, whatIsGround); //check if on right wall
        isOnLeftWall = Physics2D.OverlapCircle(playerLeftArm.position, checkRadius, whatIsGround); //check if on left wall
        isGrounded = Physics2D.OverlapCircle(playerFeet.position, checkRadius,whatIsGround); //check if on ground

        if(isOnLeftWall||isOnRightWall)
        {
            isJumping = false;//reset jump var when touching a wall
            isWalljumping = false; //unlocks horizontal movement on a wall
        }

        if (isGrounded)
        {
            canjump = true; //store jump variable
            isJumping = false;
            isWalljumping = false;
        }

            


            if ((canjump || isOnRightWall || isOnLeftWall) && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) &&!isJumping)//original jump
        {
            if (isOnRightWall&&!isGrounded) //right wall jump
            {
                rb.velocity = new Vector2(-1.25f,1.25f) * jumpForce; //jump at an angle
                isWalljumping = true;
                
            }
            else if (isOnLeftWall&&!isGrounded) //left wall jump
            {
                rb.velocity = new Vector2(1.25f, 1.25f) * jumpForce; //jump at an angle
                isWalljumping = true;
            }
            else//normal jump
            {
                rb.velocity = Vector2.up * jumpForce;
                if (LoadFluf.start)
                {
                    if (!GetComponent<AudioSource>().isPlaying)
                    {
                        GetComponent<AudioSource>().clip = sjump;
                        GetComponent<AudioSource>().Play();
                    }
                }
            }
            isJumping = true;
            jumpTimeCounter = jumpTime;
            canjump = false;
        }
   

        if ((Input.GetKey(KeyCode.Space)||Input.GetMouseButton(0))&&isJumping)//when holding jump key
            {
            if (jumpTimeCounter > 0)
            {
                if (!isWalljumping)//cant hold to gain height off of a wall jump
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
            }
            else
                isJumping = false;
            }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
            isJumping = false;

        if (!isWalljumping) //locks horizontal movement while wall jumping
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        if(Input.GetKeyDown(KeyCode.R))//skip level
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SetOutline(string sex)
    {
        
    }

    public void SetSkin(string skin)
    {
        
        if (skin == "Avatar")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[0];
        if (skin == "Barney")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[1];
        if (skin == "Black & White")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[2];
        if (skin == "Blossom")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[3];
        if (skin == "Blue Blood")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[4];
        if (skin == "Brown")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[5];
        if (skin == "Bully")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[6];
        if (skin == "Camo")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[7];
        if (skin == "Candy Floss")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[8];
        if (skin == "Cheetah")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[9];
        if (skin == "Cow")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[10];
        if (skin == "Darkness")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[11];
        if (skin == "Gold")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[12];
        if (skin == "Grey")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[13];
        if (skin == "Grinch")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[14];
        if (skin == "Guernica")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[15];
        if (skin == "Hawk")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[16];
        if (skin == "Highlighter")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[17];
        if (skin == "Hirst")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[18];
        if (skin == "Holographic")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[19];
        if (skin == "Honeycomb")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[20];
        if (skin == "Ice Cream")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[21];
        if (skin == "Kanagawa")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[22];
        if (skin == "Kandinsky")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[23];
        if (skin == "Marshmellow")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[24];
        if (skin == "Moody Blue")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[25];
        if (skin == "Orange")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[26];
        if (skin == "Parrot")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[27];
        if (skin == "Peppermint")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[28];
        if (skin == "Phoenix")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[29];
        if (skin == "Raspberry Ripple")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[30];
        if (skin == "Red")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[31];
        if (skin == "Retro")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[32];
        if (skin == "Sandy")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[33];
        if (skin == "Silver")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[34];
        if (skin == "Sky")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[35];
        if (skin == "Smoothie")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[36];
        if (skin == "Starry Night")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[37];
        if (skin == "Tie Dye")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[38];
        if (skin == "Tiger")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[39];
        if (skin == "Winter")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[40];
        if (skin == "Zebra")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[41];
        if (skin == "Zombie (Black)")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[42];
        if (skin == "Zombie (Green)")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[43];
        if (skin == "Zombie (Grey)")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[44];
        if (skin == "Zombie (Red)")
            gameObject.GetComponent<SpriteRenderer>().sprite = allSkins[45];
 



    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            GetComponent<AudioSource>().clip = sthud;
            GetComponent<AudioSource>().Play();
            Instantiate(gameOver,GameObject.FindGameObjectWithTag("Canvas").transform);
            jumpForce = 0;
            gameRunning = false;
            anim.SetBool("isJumping", true);
            LoadFluf.start = false;
            Destroy(gameObject.GetComponent<BoxCollider2D>());
        }
    }



}
