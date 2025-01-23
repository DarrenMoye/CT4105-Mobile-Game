using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject
{
    
    // Public variables exposed to the Unity menus.
    public float maxSpeed = 5;

    public float jumpTakeOffSpeed = 10;

    public float maxNumberOfJumps = 1;
    
    public float jumpLiftForAdditionalJumps = 0;

    public AudioSource jumpAudio;

    public bool joystickEnabled = false;

    public Joystick joystick;

    public GameObject joystickGameObject;

    public GameObject joystickJumpButtonGameObject;



    // Private variables so we have references to our components.
    private SpriteRenderer spriteRenderer;

    private Animator animator;

    private bool currentFlip = false;

    private int countJumps = 0;

    //manage joystick jumping
    private bool jumping = false;

    private bool firstJump = true;

    private bool nextJump = false;
    

    // Use this for initialization
    void Awake()
    {
        
        // Get references to the components we need later.
        spriteRenderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

    }

    public void doJump()
    {

        if( firstJump )
        {

            jumping = true;

            // Play audio when button is hit.
            if( jumpAudio )
            {
                
                jumpAudio.Play();

            }

        } else {

            jumping = true;

            grounded = false;

            nextJump = true;

        }

    }

    protected override void ComputeVelocity()
    {

        if ( !animator.GetBool( "died" ) ) 
        {

            // Zero our move vector to start with.
            Vector2 move = Vector2.zero;

            //use the keyboard controls
            if( !joystickEnabled )
            {

                joystickGameObject.SetActive( false );

                joystickJumpButtonGameObject.SetActive( false );

                // Get the value from the horizontal input access of the Input Manager.
                move.x = Input.GetAxis( "Horizontal" );

                if( maxNumberOfJumps != 0 )
                {
                    
                    //check if the player is on the ground and if so, reset number of jumps
                    if( countJumps >= 1 && grounded )
                    {
                    
                        velocity.y = 0;

                        countJumps = 0;

                    }

                    // Check if the Jump button has been pressed and whether are on the floor or not.
                    if ( Input.GetButtonDown( "Jump" ) && grounded ) {
                        
                        // Set the jump speed.
                        velocity.y = jumpTakeOffSpeed;

                        // Play audio when button is hit.
                        if( jumpAudio )
                        {
                            
                            jumpAudio.Play();

                        }

                    } else if ( Input.GetButtonUp( "Jump" ) ) {  

                        countJumps++; 

                        //if the jump key is pressed again then attempt an additional jump
                        if( countJumps > 1 && countJumps <= maxNumberOfJumps )
                        {
    
                            // Play audio when button is hit.
                            if( jumpAudio )
                            {
                                
                                jumpAudio.Play();

                            }

                            velocity.y = jumpTakeOffSpeed + ( velocity.y * 0.5f ) + jumpLiftForAdditionalJumps;

                        // If the velocity on the y axis is more than 0.
                        } /* else if ( velocity.y > 0 ) {

                            // Reset rigidbody type
                            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                            // Reducing the y velocity by half.
                            velocity.y *= 0.5f;

                        } */                          
                        
                    }

                }

            //use the on-screen joystick controls
            } else {

                joystickGameObject.SetActive( true );

                joystickJumpButtonGameObject.SetActive( true );

                // Get the value from the horizontal input access of the Input Manager.
                move.x = joystick.Horizontal;

                if( maxNumberOfJumps != 0 )
                {
                
                    //check if the player is on the ground and if so, reset number of jumps
                    if( countJumps >= 0 && !grounded && velocity.y <= 0.1 )
                    {
                    
                        velocity.y = 0;

                        countJumps = 0;

                        firstJump = true;

                        jumping = false;

                        nextJump = false;

                        //Debug.Log("Reset");

                    }

                    // Check if the Jump button has been pressed and whether are on the floor or not.
                    if ( firstJump && jumping && grounded ) {

                        //Debug.Log("First Jump");
                        
                        // Set the jump speed.
                        velocity.y = jumpTakeOffSpeed;

                        firstJump = false;

                        grounded = false;

                    } else if ( !firstJump && jumping && !grounded && nextJump ) {  

                        countJumps++; 

                        //if the jump key is pressed again then attempt an additional jump
                        if( countJumps > 1 && countJumps <= maxNumberOfJumps )
                        {
                            
                            //Debug.Log("Extra Jump");

                            // Play audio when button is hit.
                            if( jumpAudio )
                            {
                                
                                jumpAudio.Play();

                            }

                            velocity.y = jumpTakeOffSpeed + ( velocity.y * 0.5f ) + jumpLiftForAdditionalJumps;
                        
                        } else {

                            nextJump = false;

                            grounded = true;

                        // If the velocity on the y axis is more than 0.
                        }/*else if ( velocity.y > 0 ) {

                            // Reset rigidbody type
                            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

                            // Reducing the y velocity by half.
                            velocity.y *= 0.5f;
                    
                        } */                    
                        
                    }
                    
                } 

            }

            // Depending on which direction we are moving, flip the sprite accordingly.
            bool flipSprite = ( spriteRenderer.flipX ? ( move.x > 0.01f ) : ( move.x < 0.01f ) );

            if ( flipSprite )
            {

                spriteRenderer.flipX = !spriteRenderer.flipX;

            }

            if( !joystickEnabled )
            {
                
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

            } else {

                //if right 
                if( joystick.Horizontal > 0 )
                {

                    currentFlip = false;

                }

                //if left
                if( joystick.Horizontal < 0 )
                { 

                    currentFlip = true;

                }

            }
                    
            if ( move.x == 0 )
            {

                spriteRenderer.flipX = currentFlip;

            }

            // Set the values in our animator component.
            animator.SetBool( "grounded", grounded );

            // Reset rigidbody type
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            
            // Set speed of character movement to decrease.
            animator.SetFloat( "velocityX", Mathf.Abs( velocity.x ) / maxSpeed );

            // Finally apply the velocity and move the character.
            targetVelocity = move * maxSpeed;

        }

    }    

}