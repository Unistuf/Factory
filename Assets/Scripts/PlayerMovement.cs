using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Camera camera;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnPlayerMove(InputAction.CallbackContext value)
    {
        // Read the input value and convert it to velocity on input pressed
        if (value.performed)
        {
            Vector2 moveVector = value.ReadValue<Vector2>();

            Vector2 movement = new Vector2(-moveVector.x, -moveVector.y) * moveSpeed;
            rb.velocity = movement;
        }
        // And reset velocity to zero when we stop inputting
        if (value.canceled)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void OnPlayerZoom(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Get the scroll input and normalise it to either 1 or -1
            Vector2 inputVector = context.ReadValue<Vector2>();
            float zoomValue = inputVector.y / 120;
            zoomValue = Mathf.Clamp(zoomValue, -1, 1);

            // Then check that the resulting zoom will not leave our zoom bounds
            float resultingZoom = camera.orthographicSize - zoomValue;
            
            // If it won't, change the zoom
            if (resultingZoom >= 1 && resultingZoom <= 100)
            {
                camera.orthographicSize -= zoomValue;
            }
        }
    }
}
