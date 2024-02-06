using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] int sens = 10;

    private int maxAngleX = 45;
    private int minAngleX = -45;

    float xRotation = 0f;

    private PlayerController playerController;


    private void Awake()
    {
        playerController = new PlayerController();
    }

    private void OnEnable()
    {
        playerController.Enable();
    }

    private void OnDisable()
    {
        playerController.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Rotation();
    }

    private void Rotation()
    {
        var direction = playerController.Player.Rotation.ReadValue<Vector2>();

        Debug.Log (direction);

        var mousePosX = direction.y * sens * Time.deltaTime;
        var mousePosY = direction.x * sens * Time.deltaTime;

        xRotation -= mousePosX;
        xRotation = Mathf.Clamp(xRotation, minAngleX, maxAngleX);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        player.Rotate(Vector3.up, mousePosY);      
    }
}
