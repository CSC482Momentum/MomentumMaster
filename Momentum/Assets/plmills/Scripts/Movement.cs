using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using System;
/**
* Orginal Author: Preston Mills. 
* After the original version, this script was changed a lot by Alex Sawicki. 
* It incorperates a lot of the logic from the first person rigidbody controller. 
*/
public class Movement : MonoBehaviour
{

    public float walkingSpeed;
    public float groundedDrag = 5;
    public float aerialDrag = 0;
    public float drag;
    Vector3 movement;
    public Camera cam;
    public MovementSettings movementSettings = new MovementSettings();
    public MouseLook mouseLook = new MouseLook();
    public AdvancedSettings advancedSettings = new AdvancedSettings();

    public Rigidbody m_RigidBody;
    private CapsuleCollider m_Capsule;
    private float m_YRotation;
    private Vector3 m_GroundContactNormal;
    public bool m_PreviouslyGrounded, m_Jumping, m_IsGrounded;
    public bool m_Jump;
    private void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

        // get the rotation before it's changed
        float oldYRotation = transform.eulerAngles.y;

        mouseLook.LookRotation(transform, cam.transform);

        if (m_IsGrounded || advancedSettings.airControl)
        {
            // Rotate the rigidbody velocity to match the new direction that the character is looking
            Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
            m_RigidBody.velocity = velRotation * m_RigidBody.velocity;
        }
        /*rotX = transform.rotation.x;
        rotY = transform.rotation.y;
        rotZ = transform.rotation.z;
        rotW = transform.rotation.w;*/
    }
    [Serializable]
    public class MovementSettings
    {
        public float ForwardSpeed = 8.0f;   // Speed when walking forward
        public float BackwardSpeed = 4.0f;  // Speed when walking backwards
        public float StrafeSpeed = 4.0f;    // Speed when walking sideways
        public float RunMultiplier = 2.0f;   // Speed when sprinting
        public KeyCode RunKey = KeyCode.LeftShift;
        public float JumpForce = 30f;
        public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
        [HideInInspector]
        public float CurrentTargetSpeed = 8f;

#if !MOBILE_INPUT
        private bool m_Running;
#endif

        public void UpdateDesiredTargetSpeed(Vector2 input)
        {
            if (input == Vector2.zero) return;
            if (input.x > 0 || input.x < 0)
            {
                //strafe
                CurrentTargetSpeed = StrafeSpeed;
            }
            if (input.y < 0)
            {
                //backwards
                CurrentTargetSpeed = BackwardSpeed;
            }
            if (input.y > 0)
            {
                //forwards
                //handled last as if strafing and moving forward at the same time forwards speed should take precedence
                CurrentTargetSpeed = ForwardSpeed;
            }
#if !MOBILE_INPUT
            if (!CrossPlatformInputManager.GetButton("Walk"))
            {
                CurrentTargetSpeed *= RunMultiplier;
                m_Running = true;
            }
            else
            {
                m_Running = false;
            }
#endif
        }

#if !MOBILE_INPUT
        public bool Running
        {
            get { return m_Running; }
        }
#endif
    }
    [Serializable]
    public class AdvancedSettings
    {
        public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
        public float stickToGroundHelperDistance = 0.5f; // stops the character
        public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input. Default from stock: 20.
        public bool airControl; // can the user control the direction that is being moved in the air
        [Tooltip("set it to 0.1 or more if you get stuck in wall")]
        public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
    }



    // Use this for initialization
    void Start()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();
        mouseLook.Init(transform, cam.transform);
    }
    private void Update()
    {
        m_RigidBody.drag = drag;
        //print("Current drag: attached to player" + drag);
       // print("\nCurrent RigidBody drag: " + m_RigidBody.drag);

        RotateView();

        if (CrossPlatformInputManager.GetButtonDown("Jump") && !m_Jump)
        {
            m_Jump = true;
        }
        //} else {

    }
    //SyncState();
    private void GroundCheck()
    {
        m_PreviouslyGrounded = m_IsGrounded;
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                               ((m_Capsule.height / 2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            m_IsGrounded = true;
            m_GroundContactNormal = hitInfo.normal;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundContactNormal = Vector3.up;
        }
        if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
        {
            m_Jumping = false;
        }
    }
    private Vector2 GetInput()
    {

        Vector2 input = new Vector2
        {
            x = CrossPlatformInputManager.GetAxis("Horizontal"),
            y = CrossPlatformInputManager.GetAxis("Vertical")
        };
        movementSettings.UpdateDesiredTargetSpeed(input);
        return input;
    }
    private void StickToGroundHelper()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                               ((m_Capsule.height / 2f) - m_Capsule.radius) +
                               advancedSettings.stickToGroundHelperDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
            {
                m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
            }
        }
    }

    private float SlopeMultiplier()
    {
        float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
        return movementSettings.SlopeCurveModifier.Evaluate(angle);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float v = CrossPlatformInputManager.GetAxisRaw("Vertical");
        // Set the movement vector based on the axis input.
        movement.Set (h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * walkingSpeed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        transform.position = transform.position + movement;
        */
        //Original movement code.
        /*
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + Camera.main.transform.forward * walkingSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + Camera.main.transform.forward * -1 * walkingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Camera.main.transform.right * walkingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + Camera.main.transform.right * -1 * walkingSpeed * Time.deltaTime;
        }
        //transform.Translate(Vector3.forward * Time.deltaTime);*/

        //adapted RigidbodyFirstPersonController movement code

        GroundCheck();
        Vector2 input = GetInput();

        if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded))
        {
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
            desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

            desiredMove.x = desiredMove.x * movementSettings.CurrentTargetSpeed;
            desiredMove.z = desiredMove.z * movementSettings.CurrentTargetSpeed;
            desiredMove.y = desiredMove.y * movementSettings.CurrentTargetSpeed;
            if (m_RigidBody.velocity.sqrMagnitude <
                (movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed))
            {
                //m_RigidBody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
                m_RigidBody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
                //transform.GetComponent<PhotonView>().RPC("applyForce", PhotonTargets.All, desiredMove * SlopeMultiplier());
                //print("here");
            }
        }

        if (m_IsGrounded)
        {
            //drag = groundedDrag;
            drag = groundedDrag;
            //m_RigidBody.drag = 5f;

            if (m_Jump)
            {
                //drag = aerialDrag;
                drag = aerialDrag;
                //m_RigidBody.drag = 0f;
                //m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                //transform.GetComponent<PhotonView>().RPC("applyForce", PhotonTargets.All, new Vector3(0f, movementSettings.JumpForce, 0f));
                m_Jumping = true;
            }
            if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
            {
                m_RigidBody.Sleep();
            }
        }
        else
        {
            //drag = aerialDrag;
            drag = aerialDrag;
            //m_RigidBody.drag = 0f;
            if (m_PreviouslyGrounded && !m_Jumping)
            {
                StickToGroundHelper();
            }
        }
        m_Jump = false;

    }
}

