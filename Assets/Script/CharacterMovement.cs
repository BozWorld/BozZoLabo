using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {

    public LayerMask groundLayer;
    private Collision coll;
    public Vector3 Velocity { get; private set; }
    public bool JumpingThisFrame { get; private set; }
    public bool LandingThisFrame { get; private set; }
    public Vector3 RawMovement { get; private set; }

    private Vector3 _lastPosition;
    private float _currentHorizontalSpeed, _currentVerticalSpeed;
    [SerializeField] private PlayerInput _input;
    [SerializeField] private Vector2 _inputVector;
    private bool _active;
    [SerializeField] private GameObject _ground;
    public Vector2 bottomOffset;

    #region Event Func

    void Awake () {
        _input = new PlayerInput ();
        _input.Player.Enable ();
        _input.Player.Move.started += ctx => _inputVector = ctx.ReadValue<Vector2> ();
        _input.Player.Move.canceled += _ => _inputVector = Vector2.zero;
        _input.Player.Jump.started += ctx => Jump();
        _input.Player.Jump.canceled += ctx => isJumping = false;
        coll = GetComponent<Collision>();
    }
    
    private void FixedUpdate () {
        Walk();
        Move();
        Islanded();
    }

    #endregion

    #region Walk
    private float _moveClamp = 13;
    [SerializeField] private float _acceleration = 90;

    private void Walk () {
        if ( _inputVector.x != 0 ){
            _currentHorizontalSpeed += _inputVector.x * _acceleration * Time.deltaTime;
            _currentHorizontalSpeed = Mathf.Clamp(_currentHorizontalSpeed, -_moveClamp, _moveClamp);
        }

        else{
            _currentHorizontalSpeed = 0;
        }
       //RawMovement = new Vector2 (_currentHorizontalSpeed, _currentVerticalSpeed);
       //transform.position += RawMovement * Time.deltaTime;
       //transform.position = new Vector2 (transform.position.x + (_inputVector.x * _currentHorizontalSpeed * Time.deltaTime), transform.position.y);
    }

    #region Jump
    [SerializeField] private float _jumpHeight = 30;
    [SerializeField] private float _jumpBuffer = 0.1f;
    [SerializeField] private float _jumpEndEarlyGravityModifier = 3;
    private bool _endedJumpEarly = true;
    private bool isJumping = false;

    private void Jump() {
        if(coll.onGround){
            isJumping = true;
            _currentVerticalSpeed = _jumpHeight;
        }
    }
    private void Islanded (){
        if(coll.onGround && !isJumping ){
            _currentVerticalSpeed = 0;
        }
    }
    #endregion
    #region Move
    private void Move(){
        RawMovement = new Vector2 (_currentHorizontalSpeed, _currentVerticalSpeed);
        transform.position += RawMovement * Time.deltaTime;
    }
    #endregion
    #endregion
}