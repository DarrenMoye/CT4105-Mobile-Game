using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    
    // Public variables exposed to the Unity menus.
    public float maxSpeed = 5;

    public float jumpTakeOffSpeed = 10;

    public AudioSource jumpAudio;

    // Private variables so we have references to our components.
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private bool currentFlip = false;
    

    // Use this for initialization
    void Awake()
    {
        
        // Get references to the components we need later.
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

    }

    protected override void ComputeVelocity()
    {

        if ( !animator.GetBool( "died" ) ) 
        {

            // Zero our move vector to start with.
            Vector2 move = Vector2.zero;

            // Get the value from the horizontal input access of the Input Manager.
            move.x = Input.GetAxis( "Horizontal" );

            // Check if the Jump button has been pressed and whether are on the floor or not.
            if ( Input.GetButtonDown( "Jump" ) && grounded )
            {

                // Set the jump speed.
                velocity.y = jumpTakeOffSpeed;

            } else if ( Input.GetButtonUp( "Jump" ) ) {

                // Play audio when button is hit.
                if(jumpAudio)
                {
                    
                    jumpAudio.Play();

                }

                // If the velocity on the y axis is more than 0.
                if ( velocity.y > 0 )
                {

                    GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                    // Reducing the y velocity by half.
                    velocity.y = velocity.y * 0.5f;

                }

            }

            // Depending on which direction we are moving, flip the sprite accordingly.
            bool flipSprite = ( spriteRenderer.flipX ? ( move.x > 0.01f ) : ( move.x < 0.01f ) );

            if ( flipSprite )
            {

                spriteRenderer.flipX = !spriteRenderer.flipX;

            }

            //if right key is pressed
            if ( Input.GetAxis( "Horizontal" ) > 0 )
            {

                currentFlip = false;

            }

            //if left key is pressed
            if ( Input.GetAxis( "Horizontal" ) < 0 )
            { 

                currentFlip = true;

            }
            
            if ( move.x == 0 )
            {

                spriteRenderer.flipX = currentFlip;

            }

            // Set the values in our animator component.
            animator.SetBool( "grounded", grounded );
            
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            animator.SetFloat( "velocityX", Mathf.Abs( velocity.x ) / maxSpeed );

            // Finally apply the velocity and move the character.
            targetVelocity = move * maxSpeed;

        }

    }    

}