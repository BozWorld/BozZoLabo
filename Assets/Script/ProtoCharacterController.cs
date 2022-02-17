using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoCharacterController : MonoBehaviour{
    // [SerializeField] private AnimationCurve accelerationCurve;
    // private float horizontalSpeed;
    [SerializeField] private float maxHorizontalSpeed;
    private float verticalSpeed = 1000f;
    // [SerializeField] private float currentValue;
    // [SerializeField] private float addedValue;
    [SerializeField] private PlayerInput input;
    [SerializeField] private Vector2 inputVector;
    private bool isJumping;
    private CharacterController myCharacter;
    private Rigidbody2D _r;

    private void Awake () {
        input = new PlayerInput ();
        input.Player.Enable ();
        input.Player.Move.started += ctx => inputVector = ctx.ReadValue<Vector2> ();
        input.Player.Move.canceled += _ => inputVector = Vector2.zero;
        input.Player.Jump.started += ctx => jump ();
        input.Player.Jump.canceled += ctx => isJumping = false;
        _r = transform.gameObject.AddComponent<Rigidbody2D> ();
    }

    private void FixedUpdate () {
        // if (inputVector != Vector2.zero || isJumping )
        // {
        //     speedGain();
        // }
        //horizontalSpeed = maxHorizontalSpeed * accelerationCurve.Evaluate(currentValue);
        transform.position = new Vector2 (transform.position.x + (inputVector.x * maxHorizontalSpeed * Time.deltaTime), transform.position.y);
    }

    // private void speedGain() {
    //     currentValue = Mathf.Clamp01(currentValue + addedValue * Time.deltaTime);
    // }

    private void jump () 
    {
        isJumping = true;
    }
}
