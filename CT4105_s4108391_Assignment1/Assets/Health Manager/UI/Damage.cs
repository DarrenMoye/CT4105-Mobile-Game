using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damage : MonoBehaviour
{
    private bool entered = false;
    
    public bool destroyEnemy = false;

    public float delayWhilstHit = 0.5f;

    public float knockUnits = 1.0f;

    public float knockPowerX = 0.5f;

    public float knockPowerY = 0.1f;

    public AudioSource damageAudio;

    public UnityEvent doDecreaseLife;

    private float originalJumpTakeOffSpeed;

    void Start() {

        originalJumpTakeOffSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed;
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if( !entered )
        {

            entered = true;

            if( HealthManagerScript.lives > 0 )
            {

                doDecreaseLife.Invoke();

                if( destroyEnemy )
                {

                    Destroy( gameObject );

                }

                if( damageAudio )
                {

                    AudioSource.PlayClipAtPoint( damageAudio.clip, transform.position );

                }

            }

        }

        if( collision.gameObject.tag == "Player" )
        {

            StartCoroutine( Knockback( knockUnits, knockPowerX, knockPowerY, collision.gameObject ) );

        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {

        entered = false;
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if( !entered )
        {

            entered = true;

            if( HealthManagerScript.lives > 0 )
            {

                doDecreaseLife.Invoke();

                if( destroyEnemy )
                {

                    Destroy( gameObject );

                }

                if( damageAudio )
                {
                    
                    AudioSource.PlayClipAtPoint( damageAudio.clip, transform.position );

                }

            }

        }

        if( collision.gameObject.tag == "Player" )
        {

            StartCoroutine( Knockback( knockUnits, knockPowerX, knockPowerY, collision.gameObject ) );

        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {

        entered = false;
        
    }

    public IEnumerator Knockback( float knockUnits, float knockPowerX, float knockPowerY, GameObject collision )
    {
    
        float timer = 0;
    
        while( ( ( knockUnits * 10 ) / 2 ) > timer ) 
        {

            timer += Time.deltaTime;

            Vector2 direction = new Vector2( this.transform.position.x - collision.transform.position.x, collision.transform.position.y * ( knockPowerY / 100 ) ).normalized;

            collision.gameObject.GetComponent<Rigidbody2D>().AddForce( -direction * ( knockPowerX * 10 ) );

            collision.gameObject.GetComponent<PlayerPlatformerController>().enabled = false;

            collision.gameObject.GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed = 0;

            collision.gameObject.GetComponent<Animator>().SetBool( "grounded", true );


        }

        yield return new WaitForSeconds( delayWhilstHit );

        collision.gameObject.GetComponent<PlayerPlatformerController>().jumpTakeOffSpeed = originalJumpTakeOffSpeed;

        collision.gameObject.GetComponent<PlayerPlatformerController>().enabled = true;
                                                                                                                                                                                                                                                                                                                                                                                                                                        
    } 

}