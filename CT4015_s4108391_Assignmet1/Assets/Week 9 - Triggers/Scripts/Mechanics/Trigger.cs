using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{

    [SerializeField] public GameObject gameObjectToTrigger;
    [SerializeField] public string triggerAnimatorParamater;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        
        if( collision.gameObject.tag == "Player")
        {
            if( gameObjectToTrigger )
            {

                gameObjectToTrigger.GetComponent<Animator>().SetTrigger( triggerAnimatorParamater );

            }

        }      

    }

    private void OnTriggerStay2D( Collider2D collision )
    {
    
        if( collision.gameObject.tag == "Player")
        {

            if( gameObjectToTrigger )
            {
                gameObjectToTrigger.GetComponent<Animator>().SetTrigger( triggerAnimatorParamater );

            }

        }

    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if( collision.gameObject.tag == "Player")
        {
            if( gameObjectToTrigger )
            {

                gameObjectToTrigger.GetComponent<Animator>().ResetTrigger( triggerAnimatorParamater ); 

            }

        }  

    }

     private void OnCollisionEnter2D( Collision2D collision )
    {
        if( collision.gameObject.tag == "Player")
        {

            if( gameObjectToTrigger )
            {

                gameObjectToTrigger.GetComponent<Animator>().SetTrigger( triggerAnimatorParamater );
                
            }

        }

    }

    private void OnCollisionStay2D( Collision2D collision )
    {
        if( collision.gameObject.tag == "Player")
        {
            if( gameObjectToTrigger )
            {

                gameObjectToTrigger.GetComponent<Animator>().SetTrigger( triggerAnimatorParamater );

            }

        }

    }

    private void OnCollisionExit2D( Collision2D collision )
    {
                      
        if( collision.gameObject.tag == "Player")
        {

            if( gameObjectToTrigger )
            {

                gameObjectToTrigger.GetComponent<Animator>().ResetTrigger( triggerAnimatorParamater ); 

            }

        }   

    }

}
