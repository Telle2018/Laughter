using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;

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
        //_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is moving
        if (((Mathf.Abs(_moveHoriz) > 0.1f || Mathf.Abs(_moveVert) > 0.1f)) && _isMovable)
        {
            // Move player
            Vector3 movement = new Vector3(_moveHoriz, 0.0f, _moveVert);
            transform.position += movement * _speed * Time.deltaTime;
            transform.rotation = Quaternion.identity * Quaternion.AngleAxis(Mathf.Atan2(_moveHoriz, _moveVert) * Mathf.Rad2Deg, Vector3.up);

            _isWalking = true;
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

        _moveHoriz = -1 * movementVector.x;
        _moveVert = -1 * movementVector.y;

    }

}
