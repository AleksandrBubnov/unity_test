using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] float _speed = 4f;
    [SerializeField] float _gravity = -9.81f;
    [SerializeField] float _jumpHeight = 2f;

    private Vector3 _velosity;
    private bool _isGrounded;
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _velosity.y < 0) _velosity.y = -2f;

        if (Input.GetButtonDown("Jump") && _isGrounded)
            _velosity.y = Mathf.Sqrt(_jumpHeight * -_gravity);
    }
    private void FixedUpdate()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        _velosity.y += _gravity * Time.deltaTime;

        Vector3 moveDirection = transform.forward * moveZ +
            transform.right * moveX + _velosity;

        _characterController.Move(moveDirection * _speed * Time.deltaTime);
    }
}