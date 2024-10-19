using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float movementSpeed = 5f;
    public float jumpForce = 7f;
    private float movement;
    private bool isGrounded = true;
    private bool facingRight = true;

    public Text appleText;
    private int appleCount = 0;
    public GameObject panel;

    private int jumpCount = 0; // เก็บจำนวนครั้งที่กระโดด
    private int maxJumps = 2;  // จำนวนครั้งสูงสุดที่สามารถกระโดดได้

    void Start()
    {
        // ซ่อน panel ไว้ก่อนเมื่อเริ่มเกม
        panel.SetActive(false);
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);

        // กระโดดถ้ากด Space และยังมีกระโดดเหลือ
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumps)
        {
            Jump();
            animator.SetBool("Jump", true);
        }

        if (Mathf.Abs(movement) > 0.1f) 
        {
            animator.SetFloat("Run", 1f);
        } 
        else 
        {
            animator.SetFloat("Run", 0f);
        }

        Flip();
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        jumpCount++;  // เพิ่มจำนวนครั้งที่กระโดด
        isGrounded = false;
    }

    void Flip()
    {
        if (movement > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (movement < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    void FlipCharacter()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpCount = 0;  // รีเซ็ตจำนวนครั้งที่กระโดดเมื่อแตะพื้น
            animator.SetBool("Jump", false);
        }

        if (collision.gameObject.CompareTag("Saw") || collision.gameObject.CompareTag("Spike"))
        {
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Apple")
        {
            Destroy(other.gameObject);
            UpdateAppleCount();
        }

        if (other.tag == "Cup")
        {
            SceneManager.LoadScene(0);
        }
    }
    
    void UpdateAppleCount()
    {
        appleCount++;
        appleText.text = appleCount.ToString(); // แสดงเฉพาะตัวเลข
    }

    void Die()
    {
        Debug.Log("ตัวละครตายแล้ว!");
        
        // แสดง panel เมื่อผู้เล่นตาย
        panel.SetActive(true);

        // ลบตัวละครทันที
        Destroy(this.gameObject);
    }
}
