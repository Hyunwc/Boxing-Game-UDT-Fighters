using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }    //�̱���
    private Rigidbody playerRB;
    private Animator playerAnimator;

    private Vector3 direction = Vector3.zero;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float moveSpeed;


    public Vector3 position { get { return playerRB.position; } } // transform.position���� rigidbody.position�� ����� ����.
    public bool isLive { get { return gameObject.activeSelf; } }
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    { 
    }
    void FixedUpdate()
    {
        playerRB.velocity = velocity;
    }
    public void Initialize()
    {
        direction = Vector3.zero;
        velocity = Vector3.zero;
        gameObject.SetActive(true);
    }
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Move(Action)�� Control Type�� Vector2�̴�.
        Vector2 v = ctx.ReadValue<Vector2>();
        direction = new Vector3(v.x, 0, v.y);

        velocity = direction * moveSpeed; // ����� �ӷ��� �̿��Ͽ� �ӵ�(velocity)�� ���Ѵ�.
    }
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        playerAnimator.SetTrigger("Attack_1");
    }
}
