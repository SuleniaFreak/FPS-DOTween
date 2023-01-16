using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    #region Public_Variables

    public float mouseSensitivity;
    public float bottomAngle; //l�mite de giro de la c�mara en el eje x
    public float topAngle;
    public float yRotationSpeed;
    public float xCameraSpeed;
    #endregion

    #region Private_Variables
    float desiredYRotation; //rotaci�n deseada para la capsula en el eje Y
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

        //rotaci�n de la capsula/player en eje Y
        desiredYRotation = desiredYRotation + (mouseX * mouseSensitivity);
        //rotaci�n de la c�mara en eje X
        desiredCameraXRotation = desiredCameraXRotation - (mouseY * mouseSensitivity);
        //bloqueamos el giro de la c�mara (giro permitido, tope abajo y tope arriba)
        desiredCameraXRotation = Mathf.Clamp(desiredCameraXRotation, bottomAngle, topAngle);
    }

    void ApplyRotation()
    {
        //misma manera que en el script playermovement pero aplicado a un valor n�merico float
        currentYRotation = Mathf.SmoothDamp(currentYRotation, desiredYRotation, ref rotationYVelocity, yRotationSpeed);
        currentCameraXRotation = Mathf.SmoothDamp(currentCameraXRotation, desiredCameraXRotation,
            ref cameraXVelocity, xCameraSpeed);

        //giro la capsula
        //currentYRotation es el �ngulo en grados que queremos plicarle a la capsula en su eje Y
        //transform.rotation es un quaternion
        // con Quaternion.Euler se puede girar el objeto X grados a lo largo del eje que queramos (o los 3)
        transform.rotation = Quaternion.Euler(0, currentYRotation, 0);

        //giro la c�mara usando su rotaci�n local para que no gire con la rotaci�n de la c�psula
        myCamera.transform.localRotation = Quaternion.Euler(currentCameraXRotation, 0, 0);
    }
}
