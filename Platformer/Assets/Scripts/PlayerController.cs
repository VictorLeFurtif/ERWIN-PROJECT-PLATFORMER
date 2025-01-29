using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float x;

    [SerializeField]
    private Rigidbody2D rigidbody2dPlayer;

    private LayerMask groundLayer = 3;
    [SerializeField] private float moveSpeed;

    [SerializeField] private SpriteRenderer spriteRendererPlayer;
    [SerializeField] private CapsuleCollider2D capsuleCollider2D;
    [SerializeField] private float valueToAjustJump;
    [Header("JUMP")]
    [SerializeField]
    private  float jumpStrenght;
    private bool canJump = false;
    [SerializeField] private float timeToChangeGravity;
    [SerializeField] private GameObject prefabs;
    [SerializeField] private float elapsedTime;
    [SerializeField] private float speedTimeScale;
    [SerializeField] private float dashStrenght;
    [SerializeField] private Animator animatorPlayer;
    
    
    
    private void Update()
    {
        PlayerMoove();
        InverseGravity();
        RaycastToFlags();
        Dash();
    }

    private void FixedUpdate()
    {
        animatorPlayer.SetFloat("Yvelocity",rigidbody2dPlayer.velocity.y);
        animatorPlayer.SetBool("IsMooving", MathF.Abs(rigidbody2dPlayer.velocity.x) > 0.1);
    }

    private void PlayerMoove()
    {
        x = Input.GetAxisRaw("Horizontal");
    
        rigidbody2dPlayer.velocity = new Vector2(x * moveSpeed, rigidbody2dPlayer.velocity.y);
        switch (rigidbody2dPlayer.velocity.x)
        {
            case > 0:
                spriteRendererPlayer.flipX = false;
                break;
            case < 0:
                spriteRendererPlayer.flipX = true;
                break;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rigidbody2dPlayer.AddForce(new Vector2(0,jumpStrenght), ForceMode2D.Impulse);
            canJump = false;
        }

        
    }

    private void Dash()
    {
        if (!Input.GetKeyDown(KeyCode.LeftShift)) return;
        switch (rigidbody2dPlayer.velocity.x)
        {
            case > 0:
                DashHorizontal(Vector2.right);
                break;
            case < 0:
                DashHorizontal(Vector2.left);
                break;
        }
    }
    private void InverseGravity()
    {
        if (!Input.GetKeyDown(KeyCode.R) || !canJump) return;
        Physics2D.gravity = -Physics2D.gravity;
        rigidbody2dPlayer.velocity = new Vector2(rigidbody2dPlayer.velocity.x, -rigidbody2dPlayer.velocity.y);
        spriteRendererPlayer.flipY = spriteRendererPlayer.flipY switch
        {
            true => false,
            false => true
        };
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        foreach (var contact in other.contacts)
        {
            canJump = contact.normal.y > 0.5f;
        }
    }

    private void DashHorizontal(Vector2 direction)
    {
            rigidbody2dPlayer.AddForce(new Vector2(dashStrenght* direction.x,0) ,ForceMode2D.Impulse);
            Debug.Log("Dash");
        
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        canJump = false;
    }

    private void TimeS()
    {
        elapsedTime += Time.deltaTime;
    }
    
    private void RaycastToFlags()
    {
        if (Input.GetMouseButtonDown(0))
        {
            elapsedTime = 0;
        }
        TimeS();
        if (Input.GetMouseButtonUp(0))
        {
            if (Camera.main == null) return;
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Clic position : " + cursorPos);
            GameObject newObj = Instantiate(prefabs,cursorPos,Quaternion.identity);
            newObj.transform.localScale = new Vector2(elapsedTime * speedTimeScale, elapsedTime*speedTimeScale);
        }
        

        
    }

}
