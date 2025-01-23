using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{

    bool isOnMovingPlatform = false;

    private Animator m_Anim;

    void Start()
    {

        m_Anim = GetComponent<Animator>();

    }

    void Update()
    {

    }

    void OnCollisionEnter2D( Collision2D collision )
    {

        string tag = collision.gameObject.tag;

        if ( tag == "Moving_Platform" )
        {

            transform.parent = collision.gameObject.transform;

            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            isOnMovingPlatform = true;

        }

        if ( tag == "Falling_Platform" )
        {

            StartCoroutine( FallingPlatform( collision.gameObject ) );

        }

    }


    void OnCollisionStay2D( Collision2D collision )
    {

        string tag = collision.gameObject.tag;

        if ( tag == "Moving_Platform" )
        {


            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            if ( isOnMovingPlatform && m_Anim.GetFloat( "velocityX" ) > 0 )
            {
                
                transform.parent = null;

                isOnMovingPlatform = false;

            }  else if ( isOnMovingPlatform && m_Anim.GetBool( "grounded" ) == false ) {

                transform.parent = null;

                isOnMovingPlatform = false;     

            } else {

                transform.parent = collision.gameObject.transform;

                isOnMovingPlatform = true;

            }

        }

    }

    void OnCollisionExit2D( Collision2D collision )
    {

        if ( isOnMovingPlatform )
        {

            transform.parent = null;

            isOnMovingPlatform = false;

        }

    }

    IEnumerator FallingPlatform( GameObject collision)
    {

        yield return new WaitForSeconds(collision.GetComponent<FallingPlatform>().delay ); 

        collision.GetComponent<WobblePlatform>().enabled = false;

        collision.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

        collision.GetComponent<BoxCollider2D>().enabled = false;

    }

    void OnTriggerEnter2D( Collider2D collision)
    {

        try
        {

            if (collision.gameObject.name == "Enter")
            {

                transform.position = collision.gameObject.transform.parent.gameObject.transform.Find("Exit").transform.position;

            }

        } catch ( Exception e ) {

            print( e );

        }
    
    }

}
