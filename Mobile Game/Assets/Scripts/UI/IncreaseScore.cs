using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class IncreaseScore : MonoBehaviour
{
    // Start is called before the first frame update

    public int scoreValue = 0;

    private int score;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
 
    private void OnTriggerEnter2D( Collider2D collision )
    {

        score = int.Parse( GameObject.Find("ScoreManagerScript").GetComponent<ScoreManagerScript>().scoreText.GetComponent<Text>().text ) + scoreValue;

        if( collision.gameObject.tag == "Player")
        {
            
            GameObject.Find("ScoreManagerScript").GetComponent<ScoreManagerScript>().scoreText.GetComponent<Text>().text = score.ToString();

            Destroy( gameObject );

        }      

    }

}
