using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 10.0f, leftRightSpeed = 6.0f;
    private float jumpForce = 500.0f;
    static public bool canMove = true;
    private Vector2 move;

    private Rigidbody rb;
    private PlayerAnimation playerAnimation;
    private PlayerInput playerInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<PlayerAnimation>();
    }

    void Update()
    {
        // Desplazamiento automático hacia adelante
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        
        // Movimiento left/right
        move = playerInput.actions["mover"].ReadValue<Vector2>();
        if(canMove)
        {
            if(transform.position.x > LevelBoundary.leftSide && move.x < 0)
            {
                transform.Translate(Vector3.right * move.x * leftRightSpeed * Time.deltaTime, Space.World);
            }
            else if(transform.position.x < LevelBoundary.rightSide && move.x > 0)
            {
                transform.Translate(Vector3.right * move.x * leftRightSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    public bool IsGrounded()
    {
        // Define la longitud del raycast (puede necesitar ajustar esto)
        float distanceToGround = 0.8f;

        // Obtiene la posición de los pies del personaje
        Vector3 startPosition = transform.position - new Vector3(0, transform.localScale.y / 2, 0);

        // Dispara un raycast hacia abajo desde el centro del personaje
        RaycastHit hit;
        if (Physics.Raycast(startPosition, Vector3.down, out hit, distanceToGround))
        {
            // Si el raycast golpea algo, entonces el personaje está en el suelo
            //Debug.Log("En el suelo");
            return true;
        }

        // Si el raycast no golpea nada, entonces el personaje no está en el suelo
        //Debug.Log("En el aire");
        return false;
    }

    public void Jump(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.started)
        {
            if(IsGrounded())
            {
                rb.AddForce(Vector3.up * jumpForce);
                //Debug.Log("Salto en " + callbackContext.phase);
                playerAnimation.Jump();
            }
        }
    }
}
