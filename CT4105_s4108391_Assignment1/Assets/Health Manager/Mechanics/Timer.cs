using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{

    public float timer = 30f;

    int seconds;

    // Start is called before the first frame update
    void Start()
    {

        seconds = 0;

        timer += 1;

    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        seconds = ( int )( timer );

        GameObject.Find( "Seconds" ).GetComponent<Text>().text = seconds.ToString();

        if ( seconds < 1 )
        {

            SceneManager.LoadScene( SceneManager.GetSceneAt(0).name );

        }

    }

}
