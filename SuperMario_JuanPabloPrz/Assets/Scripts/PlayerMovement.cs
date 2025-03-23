using System;
using UnityEditor.Callbacks;
using UnityEngine;

// Clase que gestiona el movimiento del jugador
public class PlayerMovement : MonoBehaviour
{
    // Variables para almacenar la entrada horizontal, la velocidad de movimiento,
    // el estado de orientación, la potencia de salto y el estado de si está en el suelo
    float horizontalInput;
    float moveSpeed = 5f;
    bool isFacingRight = false;
    float jumpPower = 8f;
    bool isGrounded = false;

    // Referencias al componente Rigidbody2D y al componente Animator
    Rigidbody2D rb;
    Animator animator;

    // Método Start se ejecuta una vez al comienzo de la ejecución del script
    void Start()
    {
        // Obtener los componentes Rigidbody2D y Animator del objeto al que está asociado el script
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Método Update se ejecuta una vez por cada frame
    void Update()
    {
        // Captura la entrada horizontal (teclas A/D o flechas izquierda/derecha)
        horizontalInput = Input.GetAxis("Horizontal");

        // Verificar y actualizar la orientación del sprite
        FlipSprite();

        // Si el jugador presiona el botón de salto ("Jump") y está en el suelo
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            // Aplicar velocidad en el eje Y para realizar el salto
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);

            // Indicar que ya no está en el suelo después de saltar
            isGrounded = false;

            // Actualizar la animación para reflejar el estado de salto
            animator.SetBool("isJumping", !isGrounded);
        }
    }

    // Método FixedUpdate se ejecuta en intervalos fijos de tiempo, ideal para manipulación de física
    private void FixedUpdate()
    {
        // Actualizar la velocidad horizontal del objeto en función de la entrada del jugador
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocityY);

        // Actualizar los parámetros de velocidad en el Animator para controlar las animaciones
        animator.SetFloat("xVelocity", Math.Abs(rb.linearVelocityX));
        animator.SetFloat("yVelocity", rb.linearVelocityY);
    }

    // Método para voltear el sprite del personaje en función de la dirección del movimiento
    void FlipSprite()
    {
        // Verificar si el personaje está mirando en la dirección opuesta al movimiento
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            // Invertir el valor de la orientación
            isFacingRight = !isFacingRight;

            // Obtener el tamaño local del objeto (escala)
            Vector3 ls = transform.localScale;

            // Invertir la escala en el eje X para voltear el sprite
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    // Método que se activa cuando el objeto entra en colisión con otro objeto 2D con trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Establecer el estado como en el suelo cuando colisiona
        isGrounded = true;

        // Actualizar la animación para reflejar que ya no está saltando
        animator.SetBool("isJumping", !isGrounded); 
    }
}

