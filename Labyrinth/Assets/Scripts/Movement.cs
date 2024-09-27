using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody Rig;
    [SerializeField] private GameObject RunObject;
    [Header("Movement Speed Parameters")]
    [SerializeField] private float WalkingSpeed = 4;
    [SerializeField] private float RunningSpeed = 8;
    [SerializeField] private float WalkToRunChangeSpeed = 0.5f;
    [SerializeField] private float MovementSpeed = 1;
    [Header("Camera Parameters")]
    [SerializeField] private float Speed = 1;
    [SerializeField] private Transform CameraTransfore;
    [SerializeField] private float mouseSensitivity = 2;
    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance = 1f;
    private float walkRunLerp;

    private void OnDisable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        RunObject.SetActive(false);
    }

    private void OnEnable()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        TryJump();
        TryRun();
        Debug.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance, Color.green);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 cameraForwardDir = CameraTransfore.forward;
        cameraForwardDir.y = 0;
        Vector3 cameraRightDir = CameraTransfore.right;
        cameraRightDir.y = 0;

        Vector3 movementDir = cameraForwardDir.normalized * vertical + cameraRightDir.normalized * horizontal;
        movementDir = Vector3.ClampMagnitude(movementDir, 1) * Speed;
        movementDir.y = Rig.velocity.y;
        Rig.velocity = movementDir;

        Rig.angularVelocity = Vector3.zero;

        float newAngelX = CameraTransfore.rotation.eulerAngles.x - mouseY * mouseSensitivity;
        if (newAngelX > 180)
        {
            newAngelX = newAngelX - 360;
        }
        newAngelX = Mathf.Clamp(newAngelX, -80, 80);
        CameraTransfore.rotation = Quaternion.Euler(newAngelX, CameraTransfore.rotation.eulerAngles.y, CameraTransfore.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseX * mouseSensitivity, transform.rotation.eulerAngles.z);
        RunObject.SetActive(vertical > 0);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, LayerMask.GetMask("Default")))
            {
                Rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }
        }
    }

    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            if (walkRunLerp < 1)
            {
                walkRunLerp += Time.deltaTime / WalkToRunChangeSpeed;
            }
        }
        else
        {
            if (walkRunLerp > 0)
            {
                walkRunLerp -= Time.deltaTime / WalkToRunChangeSpeed;
            }
        }

        Speed = Mathf.Lerp(WalkingSpeed, RunningSpeed, walkRunLerp);
    }
}