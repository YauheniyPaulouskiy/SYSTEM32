using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    [SerializeField] private int speed = 1;

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

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var scaledMoveSpeed = speed * Time.deltaTime;

        var direction = playerController.Player.Move.ReadValue<Vector2>();

        var move = new Vector3(direction.x, 0, direction.y);
        var playerMove = transform.right * move.x + transform.forward * move.z;
        controller.Move(playerMove * scaledMoveSpeed); 
    }
}
