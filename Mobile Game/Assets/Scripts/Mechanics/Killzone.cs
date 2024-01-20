using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone : MonoBehaviour
{
    private void OnTriggerEnter2D ( Collider2D collision )
    {

        if ( collision.tag == "Player" )
        {

            //HealthScript.lives = 0;

            Scene scene = SceneManager.GetActiveScene();

            SceneManager.LoadScene( scene.name );

        }

    }

}