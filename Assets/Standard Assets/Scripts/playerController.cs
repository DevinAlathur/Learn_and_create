using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public CharacterController controller;
    public static Vector3 direction;
    public float forwardSpeed;
    public float laneDistance = 4;
    private float desiredLane = 1; // 0 - 1 - 2

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float jumpForce;
    public float gravity;

    public Animator animat;
    public float maxSpeed;
    private bool isSliding;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!playerManager.isGameStarted)
            return;
        animat.SetBool("isGameStarted", true);
       
        direction.z = forwardSpeed;
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
       animat.SetBool("isGrounded", isGrounded);

        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += 0.1f * Time.deltaTime;
        }
        if (controller.isGrounded)
        {
            //direction.y = -2;
            /*if (SwipeController.swipeUp)
            {
                jump();
            }
            */
            if (Input.GetKeyDown(KeyCode.UpArrow) || SwipeController.swipeUp) //Key control using keyboard (up)
            {
                jump();
            }
        }
        else
        {
            direction.y += gravity * Time.deltaTime;
        }


       /* if (SwipeController.swipeDown && !isSliding) 
        {
            StartCoroutine(Slide());
        }
        */
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isSliding || SwipeController.swipeDown && !isSliding) //Key control using keyboard (down)
        {
            StartCoroutine(Slide());
        }

        /*if (SwipeController.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }

        } */

        if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeController.swipeRight)  //Key control using keyboard (right)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }

        }

        /*if (SwipeController.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }*/

        if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeController.swipeLeft) //Key control using keyboard (left)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }
        else if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }

        //transform.position = Vector3.Lerp(transform.position, targetPosition , 70 * Time.deltaTime);
        //controller.center = controller.center;

        //transform.position = targetPosition;
        if (transform.position == targetPosition)
            return;
        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.magnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);


    }   

    private void FixedUpdate()
    {
        if (!playerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            playerManager.gameOver = true;
            FindObjectOfType<AudioManager>().PlaySound("Game Over");
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animat.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.5f, 0);
        controller.height = 1;


        yield return new WaitForSeconds(1.3f);

        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;
        animat.SetBool("isSliding", false);
        isSliding = false;
    }
}
