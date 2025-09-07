using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Sprite Settings")]
    public SpriteRenderer spriteRenderer;
    public Sprite spriteFront;
    public Sprite spriteBack;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    public Transform cameraTransform;

    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("keybinds")]
    public KeyCode jumpkey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        UpdateSpriteDirection();

        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpkey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }


    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }    
    }    

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }


    private void UpdateSpriteDirection()
    {
        // ī�޶� ���� forward/right ����
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0;
        camRight.y = 0;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camForward * verticalInput + camRight * horizontalInput;

        // �̵� �Է� ������ �� ī�޶� ��ġ ����
        if (moveDir == Vector3.zero)
        {
            Vector3 dirToCam = (cameraTransform.position - transform.position);
            dirToCam.y = 0;

            float camAngle = Vector3.SignedAngle(Vector3.forward, dirToCam.normalized, Vector3.up);

            if (camAngle >= -45 && camAngle < 45)
                spriteRenderer.sprite = spriteFront;   // ī�޶� �տ� �����ϱ� ���� ������
            else if (camAngle >= 45 && camAngle < 135)
                spriteRenderer.sprite = spriteLeft;
            else if (camAngle >= -135 && camAngle < -45)
                spriteRenderer.sprite = spriteRight;
            else
                spriteRenderer.sprite = spriteBack;
        }
        else // �̵� ���� �� �� �̵� ���� ����
        {
            float moveAngle = Vector3.SignedAngle(Vector3.forward, moveDir.normalized, Vector3.up);

            if (moveAngle >= -45 && moveAngle < 45)
                spriteRenderer.sprite = spriteBack;   // ������ �̵� (�÷��̾� �޸��)
            else if (moveAngle >= 45 && moveAngle < 135)
                spriteRenderer.sprite = spriteLeft;
            else if (moveAngle >= -135 && moveAngle < -45)
                spriteRenderer.sprite = spriteRight;
            else
                spriteRenderer.sprite = spriteFront;  // �ڷ� �̵� (�÷��̾� �� ����)
        }
    }
}
