using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Transform character;
    public Animator characterAnimator;
    public Text viewStatusText;
    private Quaternion characterFrontRotation;
    private Quaternion characterLeftRotation;
    private Quaternion characterRightRotation;
    private Quaternion characterBackRotation;

    private string frontText = "Front View";
    private string backText = "Back View";
    private string rightText = "Right View";
    private string leftText = "Left View";

    public float frontYRotationValue = -18f;
    public float backYRotationValue = 150f;
    public float rightYRotationValue = 10f;
    public float leftYRotationValue = -10f;

    private bool isTurningLeft = false;
    private bool isTurningRight = false;

    [SerializeField] private float rotationSpeed = 20.0f;

    void Start()
    {
        characterFrontRotation = Quaternion.Euler(0, frontYRotationValue, 0);
        characterLeftRotation = Quaternion.Euler(0, leftYRotationValue, 0);
        characterRightRotation = Quaternion.Euler(0, rightYRotationValue, 0);
        characterBackRotation = Quaternion.Euler(0, backYRotationValue, 0);

        character.rotation = characterFrontRotation; // Set initial rotation to front
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
                TriggerRun();
            else
                TriggerWalk();
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            TriggerIdle();
        }
        else if (Input.GetKeyDown(KeyCode.A))
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

        // Rotate the character while A or D key is pressed
        // Rotate the character while A or D key is pressed
        if (isTurningLeft)
        {
            character.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        else if (isTurningRight)
        {
            character.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

    }

    public void TriggerIdle()
    {
        characterAnimator.SetTrigger("idleTrigger");
    }

    public void TriggerWalk()
    {
        characterAnimator.SetTrigger("walkTrigger");
    }

    public void TriggerRun()
    {
        characterAnimator.SetTrigger("runTrigger");
    }

    public void TriggerBow()
    {
        characterAnimator.SetTrigger("bowTrigger");
    }
}
