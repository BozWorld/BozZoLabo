using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private AnimationCurve accelerationCurve;
    private float horizontalSpeed;
    [SerializeField] private float maxHorizontalSpeed;
    private float verticalSpeed = 1000f;
    [SerializeField] private float maxVerticalSpeed;
    [SerializeField] private float currentValue;
    [SerializeField] private float addedValue;
    [SerializeField] private PlayerInput input;
    [SerializeField] private Vector2 inputVector;
    private bool isJumping;

    private void Awake()
    {
        input = new PlayerInput();
        input.Player.Enable();
        input.Player.Move.started += ctx => inputVector = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += _ => inputVector = Vector2.zero;
        input.Player.Jump.started += ctx => jump();
        input.Player.Jump.canceled += ctx => isJumping = false;
    }


    private void FixedUpdate()
    {
        if (inputVector != Vector2.zero || isJumping )
        {
            speedGain();
        }
        horizontalSpeed = maxHorizontalSpeed * accelerationCurve.Evaluate(currentValue);
        transform.position = new Vector2(transform.position.x + (inputVector.x* horizontalSpeed * Time.deltaTime*currentValue), transform.position.y);
    }

    private void speedGain() {
        currentValue = Mathf.Clamp01(currentValue + addedValue * Time.deltaTime);
    }

    private void jump()
    {
        isJumping = true;
    }
}
