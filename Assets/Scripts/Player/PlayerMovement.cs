using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionAsset InpActions;

    private InputAction Move;
    private InputAction Look;
    private InputAction Jump;

    public Vector2 mMove;
    private Vector2 mLook;

    private Rigidbody rb;
    private bool isController = false;
    private float lookInfluence = 0.35f;

    [SerializeField] private float mSpeed;
    [SerializeField] private float rSpeed;
    [SerializeField] private float jSpeed;
    [SerializeField] private float sensitivity = 0.1f;
    [SerializeField] private Camera camera;

    public Animator animator;
    public float currentSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        InpActions.FindActionMap("Player").Enable();
        InputSystem.onActionChange += OnActionChange;
    }
    private void OnDestroy()
    {
        InputSystem.onActionChange -= OnActionChange;
    }
    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            InputAction action = obj as InputAction;
            if (action == null) return;

            InputDevice device = action.activeControl?.device;
            if (device == null) return;

            isController = !(device is Mouse || device is Keyboard);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mMove = Move.ReadValue<Vector2>();
        mLook = Look.ReadValue<Vector2>();        
        animator.SetFloat("currentSpeed", currentSpeed);
        if (Jump.WasPressedThisFrame())
        {
            mJump();
        }
    }
    private void FixedUpdate()
    {  
        currentSpeed = mMove.magnitude * mSpeed;      
        
        Walk();
        Rotate();
    }
    private void Awake()
    {
        Move = InputSystem.actions.FindAction("Move");
        Look = InputSystem.actions.FindAction("Look");
        Jump = InputSystem.actions.FindAction("Jump");
        animator = GetComponentInChildren<Animator>();
        

    }
    public void mJump()
    {
        rb.AddForce(Vector3.up * jSpeed, ForceMode.Impulse);
    }
    public void Walk()
    {
        Vector3 move = new Vector3(mMove.x, 0f, mMove.y) * mSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }
    public void Rotate()
    {
        if (isController)
        {
            
            if (mLook.sqrMagnitude > 0.01f)
            {
                Vector3 direct = new Vector3(mLook.x, 0f, mLook.y);
                Quaternion targetRotation = Quaternion.LookRotation(direct);
                rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rSpeed * Time.fixedDeltaTime));
            }
        }
        else
        {
            Plane ground = new Plane(Vector3.up, transform.position);
            Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (ground.Raycast(ray, out float dist))
            {
                Vector3 Point = ray.GetPoint(dist);
                Vector3 direct = Point - transform.position;
                direct.y = 0f;
                if (direct.sqrMagnitude > 0.0001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direct);
                    rb.MoveRotation(targetRotation);
                }
            }
        }

    }

}
