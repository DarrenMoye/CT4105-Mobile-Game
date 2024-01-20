using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioClip : MonoBehaviour
{

    public AudioSource audioSource;

    public Vector3 customColliderScale = new Vector3( 1.1f, 1.1f, 1.1f );

    public bool forcePlayOnce = false;

    // Start is called before the first frame update
    void Start()
    {

        AddPolygonCollider2D( gameObject, transform.parent.GetComponent<SpriteRenderer>().sprite );

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
       
        if( collision.gameObject.tag == "Player")
        {
           
           playAudioClip();

        }
           

    }

    void OnTriggerEnter2D( Collider2D collision )
    {

        if( collision.gameObject.tag == "Player")
        {
          
           playAudioClip();

        }

    }

    private void playAudioClip()
    {
        if ( forcePlayOnce )
        {

            if( !GameObject.Find( "One shot audio" )  )
            {

                AudioSource.PlayClipAtPoint( audioSource.clip, transform.position );

            }

        } else {

            AudioSource.PlayClipAtPoint( audioSource.clip, transform.position );

        }

    }

    public void AddPolygonCollider2D( GameObject go, Sprite sprite )
    {

        gameObject.transform.localScale = customColliderScale;       

        PolygonCollider2D polygon = gameObject.AddComponent<PolygonCollider2D>();

        polygon.isTrigger = true;
 
        int shapeCount = sprite.GetPhysicsShapeCount();

        polygon.pathCount = shapeCount;

        var points = new List<Vector2>(64);

        for (int i = 0; i < shapeCount; i++)
        {

            sprite.GetPhysicsShape(i, points);

            polygon.SetPath(i, points);

        }

     }

}
