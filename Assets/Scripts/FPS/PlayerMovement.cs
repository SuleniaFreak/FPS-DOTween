using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public_Variables
    [Header("Movement")]
    public float accelerationSpeed;
    public float deaccelerationSpeed;
    public int maxSpeed;

    [Header("Jump")]
    public float jumpForce;

    [Header("Raycast - Ground")]
    public LayerMask groundMask;
    public float rayLength;
    #endregion

    #region Private_Variables
    Vector2 horizontalMovement;
    Vector3 slowdown;

    bool isGrounded;
    bool jumpPressed;
    Ray ray;
    RaycastHit hit;
    Rigidbody rb;
    float h;
    float v;
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        InputPlayer();
        JumpPressed();
    }
    private void FixedUpdate()
    {
        IsGrounded();
        Movement();
        Jump();
    }

    #region Movement_Methods
    void InputPlayer()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void Movement()
    {
        //1º paso: limitamos la velocidad máxima del rb
        horizontalMovement = new Vector2(rb.velocity.x, rb.velocity.z);
        //magnitude es el módulo del vector que representa la velocidad que puede alcanzar el vector
        if(horizontalMovement.magnitude > maxSpeed)
        {
            //normalizamos el vector para que mantenga la dirección a la que va
            horizontalMovement = horizontalMovement.normalized;
            //y se multiplica por la velocidad máxima que queremos que tenga
            horizontalMovement = horizontalMovement * maxSpeed;
        }

        //asignamos velocidad al rb
        //(horizontalMovement.y representa el movimiento en el eje z, el velocity será controlado por el salto)
        rb.velocity = new Vector3(horizontalMovement.x, rb.velocity.y, horizontalMovement.y);

        //aquí aplicamos el movimiento al player por rb si está tocando el suelo
        if (isGrounded)
        {
            rb.AddRelativeForce(h * accelerationSpeed * Time.deltaTime, 0, v * accelerationSpeed * Time.deltaTime);
        }
        else
        { //si no está tocando suelo, se aplica la mitad de la fuerza
            rb.AddRelativeForce(h * accelerationSpeed / 2 * Time.deltaTime, 0, v * accelerationSpeed / 2 * Time.deltaTime);
        }

        //desaceleramos el rb
        if (isGrounded)
        { //smoothDamp cambia gradualmente la velocidad actual de un vector hacia la velocidad deseada
            //la variable deaccelerationSpeed medirá el TIEMPO que tardará en hacerlo, el rb.velocity
            //tiene la velocidad actual y el new vector a donde queremos ir
            //(ref slowdown representa la velocidad calculada a la que irá para llegar de un vector a otro
            //en el tiempo marcado por deaccelerationSpeed)
            rb.velocity = Vector3.SmoothDamp(rb.velocity, new Vector3(0, rb.velocity.y, 0), ref slowdown, deaccelerationSpeed);
        }
    }

    void IsGrounded()
    {
        ray.origin = transform.position;
        ray.direction = -transform.up;

        if(Physics.Raycast(ray, out hit, rayLength, groundMask))
        {
            Debug.Log("Estoy tocando suelo");
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.blue);
    }
    #endregion


    #region Jump_Methods

    void JumpPressed()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpPressed = true;
        }
    }

    void Jump()
    {
        if (jumpPressed)
        {
            jumpPressed = false;
            rb.AddRelativeForce(Vector3.up * jumpForce);
        }
    }
    #endregion
}
