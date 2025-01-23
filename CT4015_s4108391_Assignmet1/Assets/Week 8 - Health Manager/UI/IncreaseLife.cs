using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IncreaseLife : MonoBehaviour {

    private bool entered = false;

    public bool destroyLife = true;

    public UnityEvent doIncreaseLife;

    void OnTriggerEnter2D (Collider2D col)
    {

        if( !entered )
        {

            entered = true;

            if( HealthManagerScript.lives < 3 )
            {

                doIncreaseLife.Invoke();

                if( destroyLife )
                {

                    Destroy( gameObject );

                }

            }

        }                

    }

    void OnTriggerExit2D(Collider2D col)
    {

        entered = false;
        
    }

}
