using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManagerScript : MonoBehaviour {

    // 4 public game objects in static stores health amount
    public GameObject life1, life2, life3, gameOver;
    
    public string deathAnimatorParameter;

    public string nameOfCharacterControllerScript;

    public float delayBeforeRestart = 2.0f;

    public AudioSource deathAudio;

    // Use for initialisation, enable lives and then switch off game over
    public static int lives;

    
    void Start () 
    {

        lives = 3;

        life1.gameObject.SetActive (true);
        
        life2.gameObject.SetActive (true);

        life3.gameObject.SetActive (true);

        gameOver.gameObject.SetActive (false);

    }
    //update method to constrain the amount of lives
    void Update () 
    { 

        if (lives > 3) lives = 3;

        // health variable in switch method
        switch (lives) {

        case 3:

            life1.gameObject.SetActive (true);

            life2.gameObject.SetActive (true);

            life3.gameObject.SetActive (true);

            break;

        case 2:

            life1.gameObject.SetActive (true);

            life2.gameObject.SetActive (true);

            life3.gameObject.SetActive (false);

            break;

        case 1:

            life1.gameObject.SetActive (true);

            life2.gameObject.SetActive (false);

            life3.gameObject.SetActive (false);

            break;

        case 0:

            GameOver();
            
            break;

        }

        if( lives < 0 )
        {

            GameOver();

        }

    }
    
    public void IncreaseLife()
    {

        lives += 1;

    }

    public void DecreaseLife( int damage )
    {

        lives -= damage;

    }

    public void GameOver()
    {

        StartCoroutine( Died( delayBeforeRestart ) );

        enabled = false;

    }

    IEnumerator Died( float waitTime )
    {

        life1.gameObject.SetActive( false );

        life2.gameObject.SetActive( false );

        life3.gameObject.SetActive( false );

        gameOver.gameObject.SetActive( true );

        if( deathAudio )
        {
         
            deathAudio.Play();

        }

        GameObject.FindWithTag( "Player" ).GetComponent<Animator>().SetTrigger( deathAnimatorParameter );

        (GameObject.FindWithTag( "Player" ).GetComponent( nameOfCharacterControllerScript ) as MonoBehaviour ).enabled = false;

        GameObject.FindWithTag( "Player" ).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

        yield return new WaitForSeconds( waitTime );

        Scene scene = SceneManager.GetActiveScene();

        SceneManager.LoadScene( scene.name );

    }

}



