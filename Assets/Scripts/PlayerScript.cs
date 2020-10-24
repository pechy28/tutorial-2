using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

   private Rigidbody2D rd2d;

   public float speed = 0.8f;
   public Text score;
   private int scoreValue = 0;

   public Text lives;
   private int livesValue = 3;
   public Vector2 jump;
   public Text winText;
   public Text loseText;

   public bool isGrounded;
   public AudioClip musicClipOne;
   public AudioClip musicClipTwo;
   public AudioClip musicClipThree;
   public AudioSource musicSource;
   Animator anim;
   private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        jump = new Vector2(0.0f, 1.0f);
        lives.text = livesValue.ToString();
        winText.text = "";
        loseText.text = "";
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {

        anim.SetInteger("State", 0);

       if(Input.GetKey(KeyCode.W) && isGrounded)
       {
           rd2d.AddForce(jump, ForceMode2D.Impulse);
            anim.SetInteger("State", 2);
       }

       if(Input.GetKey(KeyCode.S))
       {
           rd2d.AddForce(new Vector2(0, -2), ForceMode2D.Impulse);
           anim.SetInteger("State", 0);
       }

         if(Input.GetKey(KeyCode.D))
            {
                if(facingRight == false)
                {
                    Flip();
                }
                rd2d.AddForce(new Vector2(2, 0),ForceMode2D.Impulse);
                anim.SetInteger("State", 1);
            }
        
         if(Input.GetKey(KeyCode.A))
            {
                if(facingRight == true)
                {
                    Flip();
                }
                rd2d.AddForce(new Vector2 (-2, 0),ForceMode2D.Impulse);
                anim.SetInteger("State", 1);
            }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);

            if(scoreValue == 10)
            {
                winText.text = "You Win!! Game made by Mikaeyla Gensler.";
                musicSource.clip = musicClipTwo;
                musicSource.Play();
            }

            if(scoreValue == 5)
            {
                transform.position = new Vector2(110,17);
                livesValue = 3;
                lives.text = livesValue.ToString();
            }
        }

        if(collision.collider.tag == "Enemy")
        {
            livesValue -=1;
            lives.text = livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(livesValue == 0)
        {
            loseText.text = "You Lose! Game made by Mikaeyla Gensler.";
            Destroy(gameObject);
            musicSource.clip = musicClipThree;
            musicSource.Play();
        }
        
        if(collision.collider.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    

}
