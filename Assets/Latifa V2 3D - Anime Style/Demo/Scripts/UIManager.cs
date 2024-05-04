using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Transform character;
    public Animator characterAnimator;

    [SerializeField] private float rotationSpeed = 20.0f;

    private bool isWalking = false;
    private bool isRunning = false;
    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    private void Start()
    {
        characterAnimator.SetTrigger("idleTrigger"); // Set initial animation state to idle
    }

    private void Update()
    {
        HandleMovementInput();
        HandleRotationInput();
    }

    private void HandleMovementInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                TriggerRun();
            else
                TriggerWalk();
        }

        if (Input.GetKeyUp(KeyCode.W))
            TriggerIdle();
    }

    private void HandleRotationInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isTurningLeft = true;
            isTurningRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            isTurningLeft = false;
            isTurningRight = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            isTurningLeft = false;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isTurningRight = false;
        }

        RotateCharacter();
    }

    private void RotateCharacter()
    {
        if (isTurningLeft)
        {
            character.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (isTurningRight)
        {
            character.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    private void TriggerIdle()
    {
        characterAnimator.SetTrigger("idleTrigger");
        isWalking = false;
        isRunning = false;
    }

    private void TriggerWalk()
    {
        characterAnimator.SetTrigger("walkTrigger");
        isWalking = true;
        isRunning = false;
    }

    private void TriggerRun()
    {
        characterAnimator.SetTrigger("runTrigger");
        isWalking = false;
        isRunning = true;
    }

    private void TriggerBow()
    {
        characterAnimator.SetTrigger("bowTrigger");
    }
}
