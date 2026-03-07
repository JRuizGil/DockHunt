using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset InpActions;

    private InputAction Move;
    private InputAction Look;
    private InputAction Jump;

    private Vector2 mMove;
    private Vector2 mLook;

    private Rigidbody rb;

    [SerializeField] private float mSpeed;
    [SerializeField] private float rSpeed;
    [SerializeField] private float jSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InpActions.FindActionMap("Player").Enable();
    }

    // Update is called once per frame
    void Update()
    {
        mMove = Move.ReadValue<Vector2>();
        mLook = Look.ReadValue<Vector2>();
        if (Jump.WasPressedThisFrame())
        {
            mJump();
        }
    }
    private void FixedUpdate()
    {
        Walk();
        //Rotate();
    }
    private void Awake()
    {
        Move = InputSystem.actions.FindAction("Move");
        Look = InputSystem.actions.FindAction("Look");
        Jump = InputSystem.actions.FindAction("Jump");

    }
    public void mJump()
    {
        rb.AddForce(Vector3.up * jSpeed, ForceMode.Impulse);
    }
    public void Walk()
    {
        rb.MovePosition(rb.position + transform.forward * mMove.y * mSpeed * Time.deltaTime);
    }
    /*public void Rotate()
    {
        float Rot = mLook.x * rSpeed * Time.deltaTime;
        
    }*/

}
