using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class CharacterMechanics : MonoBehaviour
    {
        [SerializeField] private MobileController _joystick;

        //Main settings
        public float speedMovie; //character speed
        public float jumpPower; //jump power

        //Gameplay Options
        private float gravityForce;//character gravity
        private Vector3 moveVector;//direction of travel

        //Component Links
        private CharacterController ch_controller;
        private Animator ch_animator;

        private bool move = false;

        // Use this for initialization
        void Start()
        {
            ch_controller = GetComponent<CharacterController>();
            ch_animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            CharacterMove();
            GamingGravity();
            GamingUse();

        }

        //character movement method
        private void CharacterMove()
        {
            // surface movement
            if (ch_controller.isGrounded)
            {
                //ch_animator.ResetTrigger("Jump");
                ch_animator.SetBool("Jump", false);

                moveVector = Vector3.zero;
                if (_joystick != null)
                {
                    moveVector.x = _joystick.Horizontal() * speedMovie;
                    moveVector.z = _joystick.Vertical() * speedMovie;
                }
                else
                {
                    moveVector.x = Input.GetAxis("Horizontal") * speedMovie;
                    moveVector.z = Input.GetAxis("Vertical") * speedMovie;
                }

                //character movement animation
                if (moveVector.x != 0 || moveVector.z != 0)
                {
                    var vc_magnitude = new Vector2(_joystick.Horizontal(), _joystick.Vertical()).magnitude;
                    ch_animator.speed = vc_magnitude;
                    ch_animator.SetBool("Move", true);
                }
                else
                {
                    ch_animator.SetBool("Move", false);
                }

                //heading in the direction of travel
                if (Vector3.Angle(Vector3.forward, moveVector) > 1.0f || Vector3.Angle(Vector3.forward, moveVector) == 0.0f)
                {
                    Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMovie, 0.0f);
                    transform.rotation = Quaternion.LookRotation(direct); // make a turn
                }
            }

            moveVector.y = gravityForce;
            ch_controller.Move(moveVector * Time.deltaTime); // the method is moved in the direction
        }

        //gravity method
        private void GamingGravity()
        {
            if (!ch_controller.isGrounded)
            {
                gravityForce -= 20.0f * Time.deltaTime;
            }
            else
            {
                gravityForce = -1.0f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && ch_controller.isGrounded)
            {
                gravityForce = jumpPower;
                ch_animator.SetBool("Jump", true);
            }
        }

        private void GamingUse()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ch_animator.SetBool("Use", true);
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                ch_animator.SetBool("Use", false);
            }
        }
    }
}
