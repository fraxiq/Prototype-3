using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameOver = false;
    private bool dobuleJumpUsed = false;
    private float dobuleJumpForce = 5f;
    public bool isDashing;
    


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isDashing = true;
            playerAnim.SetFloat("Speed_Multiplier", 2.0f);
        }
        else if (isDashing)
        {
            isDashing = false;
            playerAnim.SetFloat("Speed_Multiplier", 1.0f);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            dobuleJumpUsed = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !dobuleJumpUsed)
        {
            dobuleJumpUsed = true;
            playerRb.AddForce(Vector3.up * dobuleJumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 1.0f);
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
    }
}
