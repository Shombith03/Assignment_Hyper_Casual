using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _jumpForce;
    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private float _groundOffset;
    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private int _maxJumps = 2;

    private int _jumpsLeft;

    private Rigidbody _rigidbody;
    private bool isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _jumpsLeft = _maxJumps;
    }

    private void Update()
    {

        isGrounded = Physics.Raycast(_groundCheck.position, Vector3.down, _groundOffset, _layerMask);

        if (isGrounded && _rigidbody.velocity.y <= 0)
        {
            _jumpsLeft = _maxJumps;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _jumpsLeft > 0)
        {
            Jump();
            _jumpsLeft -= 1;
        }

        if(transform.position.y > 10 || transform.position.y < -10)
        {
            //GameManager.Instance.GameOverMenu();
            GameManager.Instance.isGameOver = true;
        }
    }

    private void FixedUpdate()
    {
        float movement = Input.GetAxisRaw("Vertical");
        _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, movement * _moveSpeed);

    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector3(0f, _jumpForce, _rigidbody.velocity.z);
    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_groundCheck.position, _groundCheck.position + Vector3.down * _groundOffset);
    }

}
