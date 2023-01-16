using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Public_Variables

    public float mouseSensitivity;
    public float bottomAngle; //límite de giro de la cámara en el eje x
    public float topAngle;
    public float yRotationSpeed;
    public float xCameraSpeed;
    #endregion

    #region Private_Variables
    float desiredYRotation; //rotación deseada para la capsula en el eje Y
    float desiredCameraXRotation;
    float currentYRotation;
    float currentCameraXRotation;
    float rotationYVelocity;
    float cameraXVelocity;

    Camera myCamera;
    float mouseX;
    float mouseY;
    #endregion

    private void Awake()
    {
        myCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Start()
    {
        
    }

    void Update()
    {
        MouseInputMovement();
        
    }

    private void FixedUpdate()
    {
        ApplyRotation();
    }

    void MouseInputMovement()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        //rotación de la capsula/player en eje Y
        desiredYRotation = desiredYRotation + (mouseX * mouseSensitivity);
        //rotación de la cámara en eje X
        desiredCameraXRotation = desiredCameraXRotation - (mouseY * mouseSensitivity);
        //bloqueamos el giro de la cámara (giro permitido, tope abajo y tope arriba)
        desiredCameraXRotation = Mathf.Clamp(desiredCameraXRotation, bottomAngle, topAngle);
    }

    void ApplyRotation()
    {
        //misma manera que en el script playermovement pero aplicado a un valor númerico float
        currentYRotation = Mathf.SmoothDamp(currentYRotation, desiredYRotation, ref rotationYVelocity, yRotationSpeed);
        currentCameraXRotation = Mathf.SmoothDamp(currentCameraXRotation, desiredCameraXRotation,
            ref cameraXVelocity, xCameraSpeed);

        //giro la capsula
        //currentYRotation es el ángulo en grados que queremos plicarle a la capsula en su eje Y
        //transform.rotation es un quaternion
        // con Quaternion.Euler se puede girar el objeto X grados a lo largo del eje que queramos (o los 3)
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);

        //giro la cámara usando su rotación local para que no gire con la rotación de la cápsula
        myCamera.transform.localRotation = Quaternion.Euler(currentCameraXRotation, 0, 0);
    }
}
