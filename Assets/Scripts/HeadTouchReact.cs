using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This code gives a model an ability to react when player touches its head
/// </summary>
public class HeadTouchReact : MonoBehaviour {

    [SerializeField] ParticleSystem bloodParticles;
    [SerializeField] GameObject head;

    CharacterController CharCon;
    Animator animator;

    public float jumpSpeed = 15.0f;
    float verticalSpeed = 0.0f;
    float gravity = -9.8f;
    float terminalVelocity = -10.0f;
    public float minFall = -1.0f;

    int counter;

    private void Start()
    {
        animator = GetComponent<Animator>();
        CharCon = GetComponent<CharacterController>();
    }

    /// <summary>
    /// All the magic happens here.
    /// </summary>
    private void Update()
    {

        bool hitGround = false;
        RaycastHit hit;

        // Check if model hits the ground
        if (verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = CharCon.radius * 1.1f;
            hitGround = hit.distance <= check;
        }


        if (hitGround)
        {
            //if Ethan is on the ground we can make him jump
            animator.SetBool("OnGround", true);

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.transform.gameObject.GetInstanceID() == head.GetInstanceID())
                    {

                        //some blood effects on touch
                        bloodParticles.transform.position = hit.point;
                        bloodParticles.Emit(10);
                        //

                        counter++;
                        if(counter == 5)
                        {
                            // giving acceleration
                            verticalSpeed = jumpSpeed;
                            counter = 0;
                        }
                            
                    }

                }
            }
            else
            {
                //just some constant force like gravity
                verticalSpeed = minFall;
            }

        }
        else
        {
            // if Ethan is in the air we need to apply power of gravity on him
            animator.SetBool("OnGround", false);
            verticalSpeed += gravity * 5 * Time.deltaTime;
            if (verticalSpeed < terminalVelocity)
                verticalSpeed = terminalVelocity;

            //animation should be played too
            animator.SetFloat("Jump", verticalSpeed);
        }


        //applying movement
        CharCon.Move(new Vector3(0.0f, verticalSpeed * Time.deltaTime, 0.0f));

    }



}
