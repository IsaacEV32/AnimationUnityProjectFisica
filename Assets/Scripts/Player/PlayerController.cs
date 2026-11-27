using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float maxSpeedWalking = 4;
    [SerializeField] float maxSpeedRunning = 8;
    float maxSpeed;
    Vector3 moveDirection;
    float currentSpeed = 0;
    bool isMoving = false;
    CharacterController characterController;
    Animator playerAnimator;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();
    }
    public void OnMove(InputAction.CallbackContext onMove)
    {
        if (onMove.performed)
        {
            isMoving = true;
            moveDirection = onMove.ReadValue<Vector2>();
            moveDirection = new Vector3(moveDirection.x, 0, moveDirection.y).normalized;
            maxSpeed = maxSpeedWalking;
        }
        else
        {
            isMoving = false;
        }

    }
    public void OnRunning(InputAction.CallbackContext onRunning)
    {
        if (onRunning.performed)
        {
            maxSpeed = maxSpeedRunning;
        }
        else
        {
            maxSpeed = maxSpeedWalking;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (currentSpeed <= maxSpeed)
            {
                currentSpeed += 2 * Time.deltaTime;
            }
            characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
            playerAnimator.SetFloat("Speed", currentSpeed);
            Debug.Log(currentSpeed);
        }
        else 
        {
            if (currentSpeed >= 0)
            {
                currentSpeed -= 4 * Time.deltaTime;
            }
            characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
            playerAnimator.SetFloat("Speed", currentSpeed);
            Debug.Log(currentSpeed);
        }
    }
}
