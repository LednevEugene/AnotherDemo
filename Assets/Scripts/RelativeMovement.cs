using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// This is my 3d person character controller instead of one from Standard Assets.
/// Because for some reason it didn't work.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;

    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;

    float verticalSpeed;

    CharacterController CharCon;
    Animator Animator;
    ControllerColliderHit collision;

    bool IsJoystickOn;
    Vector2 joystickCenter;
    public int joystickRadius = 100;

    float horInput;
    float vertInput;

    float timeIdling;

    private void Start()
    {
        verticalSpeed = minFall;
        CharCon = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;

#if UNITY_EDITOR
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");


        //Basicly this is virtual invisible joystick to control the character
        // It needs to create anchor on first touch and then reads touch.deltaPosion like 2 input axis
#else
        
        if(Input.touchCount > 0)
        {
            if (IsJoystickOn)
            {
                var currentPosition = Input.GetTouch(0).position;
                var delta = currentPosition - joystickCenter;

                horInput = (delta.x % joystickRadius) / joystickRadius;
                vertInput = (delta.y % joystickRadius) / joystickRadius;
            }
            else
            {
                IsJoystickOn = true;
                joystickCenter = Input.GetTouch(0).position;
            }
        }
        else if(Input.touchCount == 0)
        {
            IsJoystickOn = false;
            horInput = 0;
            vertInput = 0;
        }
#endif

        if (horInput != 0 || vertInput != 0)
        {

            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);

            //don't forget about animation
            Animator.SetFloat("Forward", movement.z );
            Animator.SetFloat("Turn", Mathf.Atan2(movement.x, movement.z));

            //fun math to apply rotation relative to camera point of view
            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;

            var direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        else
        {
            Animator.SetFloat("Forward", 0f);
            Animator.SetFloat("Turn", 0f);       
        }


     



        bool hitGround = false;
        RaycastHit hit;
        //Check if character hits the ground
        if (verticalSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            float check = CharCon.radius * 1.2f;
            hitGround = hit.distance <= check;
        }

        if (hitGround)
        {
            //If character is on the ground he can jump
            Animator.SetBool("OnGround", true);
            if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 2)
            {
                //Giving accelaration for jump
                verticalSpeed = jumpSpeed;

                //Set timer to zero when jump
                timeIdling = 0f;
            }
            else
            {
                verticalSpeed = minFall;
            }
        }
        else
        {

            verticalSpeed += gravity * 5 * Time.deltaTime;
            if (verticalSpeed < terminalVelocity)
                verticalSpeed = terminalVelocity;

            if (CharCon.isGrounded && collision != null)
            {
                Animator.SetBool("OnGround", true);
                Animator.SetFloat("Jump", minFall);

                //// This little piece of code I found in a book called "Unity in action" and it gives a character proper reaction to slopes 
                if (Vector3.Dot(movement, collision.normal) < 0)
                {
                    movement = collision.normal * moveSpeed;
                }
                else
                {
                    movement += collision.normal * moveSpeed;
                }
                ////
            }
            else
            {
                //
                Animator.SetBool("OnGround", false);
                Animator.SetFloat("Jump", verticalSpeed);
                //
            }
        }

        //Combining computed movement in one vector
        movement.y = verticalSpeed;
        movement *= Time.deltaTime;


        // it's just a little kludge. It makes controlls better. You won't move anywhere when just rotating.
        if(movement.x == 0)
        {
            movement.z = 0;
        }
        //


        ///// This is timer to trigger Crouch animation
        if (movement.x != 0f && movement.z != 0f)
            timeIdling = 0;
        else
            timeIdling += Time.deltaTime;

        if (timeIdling > 2.0f)
        {
            Animator.SetBool("Crouch", true);
        }
        else
        {
            Animator.SetBool("Crouch", false);
        }
        /////



        CharCon.Move(movement);
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        collision = hit;
    }

}
