using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector3 pointA = new Vector3(0, 0, 0);

    private Vector3 pointB = new Vector3(0, 0, 0);

    [SerializeField] public float speed = 1f;

    public enum Orientation { Left, Right, Up, Down };

    [SerializeField] public Orientation orientation;

    [SerializeField] public float units = 1f;

    private float time;

    void Start()
    {
        pointA = transform.position;

        switch( orientation.ToString() )
        {

            case "Left":

                pointB = new Vector3( pointA.x + ( units * -1 ), pointA.y, pointA.z );

                break;

            case "Right":

                pointB = new Vector3( pointA.x + ( units * 1 ), pointA.y, pointA.z );

                break;

            case "Up":

                pointB = new Vector3( pointA.x, pointA.y + ( units * 1 ), pointA.z );

                break;

            case "Down":

                pointB = new Vector3( pointA.x, pointA.y + ( units * -1 ), pointA.z );

                break;

            default:

                pointB = new Vector3( pointA.x + ( units * 1 ), pointA.y, pointA.z );

                break;

        }

    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime * ( speed / units );

        // Moves the object to target position
        transform.position = Vector3.Lerp( pointA, pointB, time );

        // Flip the points once it has reached the target
        if ( time >= 1 )
        {
            var b = pointB;

            var a = pointA;

            pointA = b;

            pointB = a;

            time = 0;

        }

    }

}
