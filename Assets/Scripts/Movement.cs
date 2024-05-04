using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 6f;  // Walking speed in units per second
    public float runSpeed = 9f;   // Running speed in units per second

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isWalking = animator.GetCurrentAnimatorStateInfo(0).IsName("Walk");
        bool isRunning = animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
        bool isIdle = animator.GetCurrentAnimatorStateInfo(0).IsName("Idle");

        float speed = 0f;

        if (isRunning)
        {
            speed = runSpeed;
        }
        else if (isWalking)
        {
            speed = walkSpeed;
        }

        if (!isIdle)
        {
            float moveDistance = speed * Time.deltaTime;
            transform.Translate(Vector3.forward * moveDistance);
        }
    }
}
