using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    // Public values exposed to the Unity menus.
    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    // Protected variables used in calculations.
    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected Vector2 velocity;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    // Constant values used in calculations.
    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    void OnEnable()
    {
        // Grab a reference to the 2D rigidbody.
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Set filter flags on start.
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
        // Zero our velocity and then call a function to calculate it.
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {
        // This function is overridden in PlayerPlatformerController.cs
    }

    void FixedUpdate()
    {
        // Apply gravity to our velocity.
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        // Reset the grounded flag.
        grounded = false;

        // Find the amount we can move this frame.
        Vector2 deltaPosition = velocity * Time.deltaTime;

        // Get the ground position.
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

        // Apply the movement amount along the ground axis.
        Vector2 move = moveAlongGround * deltaPosition.x;

        // Check the movement.
        Movement(move, false);

        // Apply any vertical movement required.
        move = Vector2.up * deltaPosition.y;

        // Apply the movement.
        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        // Find the distance we are moving.
        float distance = move.magnitude;

        // Check if it's more than the minimum move distance.
        if (distance > minMoveDistance)
        {
            // Cast a ray and see what it hits.
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            // Go through our hit entities.
            for (int i = 0; i < hitBufferList.Count; i++)
            {
                // Find the surface normal.
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    // We are on a surface with an up facing normal so set grounded to true.
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }


        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }

}