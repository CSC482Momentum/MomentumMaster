using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;


/*
    ****************************************************************************************

    PLEASE DEAR GOD DO NOT CHANGE ANYTHING IN HERE!
    It will almost certainly break networking in some obscure way
    So please, don't change anything without consulting the Network Team first.

    ****************************************************************************************
    */
namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (Rigidbody))]
    [RequireComponent(typeof (CapsuleCollider))]
    public class RigidbodyFirstPersonController : NetworkBehaviour
    {
         /*[SyncVar] public float posX;
         [SyncVar] public float posY;
         [SyncVar] public float posZ;

         [SyncVar] public float rotX;
         [SyncVar] public float rotY;
         [SyncVar] public float rotZ;
         [SyncVar] public float rotW;*/


        /*public struct ServerState {
            public float posX;
            public float posY;
            public float posZ;

            public float rotX;
            public float rotY;
            public float rotZ;
            public float rotW;
        }

        [SyncVar] public ServerState state;*/
        /*public float posX;
        public float posY;
        public float posZ;

        public float rotX;
        public float rotY;
        public float rotZ;
        public float rotW;*/

        public void Awake() {
            transform.position = new Vector3(100, 100, 100);
            //GameObject player = NetworkServer.FindLocalObject(GetComponent<NetworkIdentity>().netId);
            InitState();
        }

        public void SyncState() {
            //transform.position = new Vector3(state.posX, state.posY, state.posZ);
            //m_RigidBody.rotation = new Quaternion(state.rotX, state.rotY, state.rotZ, state.rotW);
        }
        
        [Server] public void InitState () {
            /*posX = 100;
            posY = 100;
            posZ = 100;
            rotX = 0;
            rotY = 0;
            rotZ = 0;*/
            //state = new ServerState() { posX = 100, posY = 100, posZ = 100};
        }
        
        [Command] public void CmdMove() {
            /*posX = m_RigidBody.transform.position.x;
            posY = m_RigidBody.transform.position.y;
            posZ = m_RigidBody.transform.position.z;*/
            /*ServerState st = new ServerState();
            st.posX = transform.position.x;
            st.posY = transform.position.y;
            st.posZ = transform.position.z;

            st.rotX = transform.rotation.x;
            st.rotY = transform.rotation.y;
            st.rotZ = transform.rotation.z;
            st.rotW = transform.rotation.w;
            state = st;*/
        }

        [Command] public void CmdAddForce(Vector3 amount, ForceMode type) {
            m_RigidBody.AddForce(amount, type);
            //print("Force added: " + amount + " local=" + isLocalPlayer);
        }

        [Command] public void CmdAddForce2(Vector3 amt, int pl) {
            RigidbodyFirstPersonController play = GameObject.FindGameObjectWithTag("Player" + pl).GetComponent<RigidbodyFirstPersonController>();
            RpcAddServerForce(amt);
            play.m_RigidBody.AddForce(amt);
            

            print(gameObject.tag + " is hitting Player" + pl+" server="+isServer);
        }

        [ClientRpc] public void RpcAddServerForce(Vector3 amt) {
            if (isLocalPlayer) m_RigidBody.AddForce(amt);
            print("RPC: "+gameObject.tag + " is hitting Player_HOST local="+isLocalPlayer);
        }

        public void ApplyForceToPlayer(Vector3 amt, int pl) {                
            CmdAddForce2(amt, pl);
        }


        private void RotateView() {
            //avoids the mouse looking if the game is effectively paused
            if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

            // get the rotation before it's changed
            float oldYRotation = transform.eulerAngles.y;

            mouseLook.LookRotation(transform, cam.transform);

            if (m_IsGrounded || advancedSettings.airControl) {
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
            [HideInInspector] public float CurrentTargetSpeed = 8f;

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
            public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
            public bool airControl; // can the user control the direction that is being moved in the air
            [Tooltip("set it to 0.1 or more if you get stuck in wall")]
            public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
        }


        public Camera cam;
        public MovementSettings movementSettings = new MovementSettings();
        public MouseLook mouseLook = new MouseLook();
        public AdvancedSettings advancedSettings = new AdvancedSettings();

        public Rigidbody m_RigidBody;
        private CapsuleCollider m_Capsule;
        private float m_YRotation;
        private Vector3 m_GroundContactNormal;
        private bool m_PreviouslyGrounded, m_Jumping, m_IsGrounded;
        public bool m_Jump;

        public Vector3 Velocity
        {
            get { return m_RigidBody.velocity; }
        }

        public bool Grounded
        {
            get { return m_IsGrounded; }
        }

        public bool Jumping
        {
            get { return m_Jumping; }
        }

        public bool Running
        {
            get
            {
 #if !MOBILE_INPUT
				return movementSettings.Running;
#else
	            return false;
#endif
            }
        }


        private void Start() {
            m_RigidBody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            if (!isLocalPlayer) {
                this.gameObject.transform.Find("OVRPlayerController").gameObject.SetActive(false);
                m_RigidBody.drag = 5; //DO NOT REMOVE THIS SORCERY
            } else {
                mouseLook.Init(transform, cam.transform);
            }
            SyncState();
        }


        private void Update() {
            if (isLocalPlayer) {
                RotateView();

                if (CrossPlatformInputManager.GetButtonDown("Jump") && !m_Jump) {
                    m_Jump = true;
                }
            //} else {
                
            }
            //SyncState();
        }


        private void FixedUpdate()
        {
            if (isLocalPlayer) {
                GroundCheck();
                Vector2 input = GetInput();

                if ((Mathf.Abs(input.x) > float.Epsilon || Mathf.Abs(input.y) > float.Epsilon) && (advancedSettings.airControl || m_IsGrounded)) {
                    // always move along the camera forward as it is the direction that it being aimed at
                    Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
                    desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;

                    desiredMove.x = desiredMove.x * movementSettings.CurrentTargetSpeed;
                    desiredMove.z = desiredMove.z * movementSettings.CurrentTargetSpeed;
                    desiredMove.y = desiredMove.y * movementSettings.CurrentTargetSpeed;
                    if (m_RigidBody.velocity.sqrMagnitude <
                        (movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed)) {
                        //m_RigidBody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
                        CmdAddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
                    }
                }

                if (m_IsGrounded) {
                    m_RigidBody.drag = 5f;

                    if (m_Jump) {
                        m_RigidBody.drag = 0f;
                        m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
                        //m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                        CmdAddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
                        m_Jumping = true;
                    }

                    if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f) {
                        m_RigidBody.Sleep();
                    }
                } else {
                    m_RigidBody.drag = 0f;
                    if (m_PreviouslyGrounded && !m_Jumping) {
                        StickToGroundHelper();
                    }
                }
                m_Jump = false;
                CmdMove();
            }
            SyncState();
        }


        private float SlopeMultiplier()
        {
            float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
            return movementSettings.SlopeCurveModifier.Evaluate(angle);
        }


        private void StickToGroundHelper()
        {
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) +
                                   advancedSettings.stickToGroundHelperDistance, ~0, QueryTriggerInteraction.Ignore))
            {
                if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
                {
                    m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
                }
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




        /// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
        private void GroundCheck()
        {
            m_PreviouslyGrounded = m_IsGrounded;
            RaycastHit hitInfo;
            if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
                                   ((m_Capsule.height/2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, ~0, QueryTriggerInteraction.Ignore))
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
    }
}
