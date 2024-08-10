using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject flashlight;
    [SerializeField] private Animator animator;
    [SerializeField] private string characterName;

    public static Action OnBlackOut;
    public static Action OnEndBlackOut;

    private Rigidbody2D rb;
    private Vector2 movement;

    private enum State { Idle, LeftWalk, RightWalk }
    private State currentState = State.Idle;

    private void OnEnable()
    {
        OnBlackOut += ActivateFlashlight;
        OnEndBlackOut += DeactivateFlashlight;
    }

    private void OnDisable()
    {
        OnBlackOut -= ActivateFlashlight;
        OnEndBlackOut -= DeactivateFlashlight;
    }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        flashlight.SetActive(false);
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        movement = new Vector2(moveX, 0);

        UpdateState();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void UpdateState()
    {
        if (movement.x > 0)
        {
            ChangeState(State.RightWalk);
        }
        else if (movement.x < 0)
        {
            ChangeState(State.LeftWalk);
        }
        else
        {
            ChangeState(State.Idle);
        }
    }

    private void ChangeState(State newState)
    {
        if (currentState == newState) return;

        currentState = newState;

        switch (currentState)
        {
            case State.Idle:
                animator.Play(characterName + "_Idle");
                break;
            case State.LeftWalk:
                animator.Play(characterName + "_Walk_L");
                break;
            case State.RightWalk:
                animator.Play(characterName + "_Walk_R");
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("RepairZone"))
        {
            AudioManager.Instance.PlaySFXClip("CableGameOpening");
            FindObjectOfType<OfficeCableGameController>().StartRepairGame();
        }
    }

    public void ActivateFlashlight()
    {
        flashlight.SetActive(true);
    }

    public void DeactivateFlashlight()
    {
        flashlight.SetActive(false);
    }
}
