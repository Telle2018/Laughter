using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _rotspeed = 90;

    private float _moveHoriz;
    private float _moveVert;
    private Rigidbody _playerRB;
    private bool _isMovable = true;

    private Quaternion _lastRotation = new Quaternion();

    //private Animator _animator;
    public bool _isWalking = false;

    public bool IsMovable
    {
        get { return _isMovable; }
        set { _isMovable = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        _playerRB.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is moving
        if ((Mathf.Abs(_moveVert) > 0.1f) && _isMovable)
        {
            // Move player
            if (_moveVert > 0.0f)
            {
                transform.position += transform.forward * Time.deltaTime * _speed;
            }
            else
            {
                transform.position -= transform.forward * Time.deltaTime * _speed;
            }

            _isWalking = true;
        }
        else if ((Mathf.Abs(_moveHoriz) > 0.1f ) && _isMovable)
        {
            transform.rotation = transform.rotation * Quaternion.AngleAxis(_moveHoriz * _rotspeed * Time.deltaTime, Vector3.up);
            _lastRotation = transform.rotation;
        }
        else
        {
            _isWalking = false;
            transform.rotation = _lastRotation;
        }
        //_animator.SetBool("Walk", _isWalking);
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        _moveHoriz = 1 * movementVector.x;
        _moveVert = 1 * movementVector.y;

    }

}
