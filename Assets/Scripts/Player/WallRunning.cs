using UnityEngine;

public class WallRunning : MonoBehaviour
{
    //[Header("Wallrunning")]
    //public LayerMask whatIsWall;
    //public LayerMask whatIsGround;
    //public float wallRunForce;
    //public float maxWallRunTime;
    //private float wallRunTimer;

    //[Header("Input")]
    //private float horizontalInput;
    //private float verticalInput;

    //[Header("Detection")]
    //public float wallCheckDistance;
    //public float minJumpHeight;
    //private RaycastHit leftWallhit;
    //private RaycastHit rightWallhit;
    //private bool wallLeft;
    //private bool wallRight;

    //[Header("Reference")]
    //public Transform orientation;
    //private Rigidbody rb;
    //private newPlayerMovement pm;

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    pm = GetComponent<newPlayerMovement>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    CheckForWall();
    //    StateMachine();
    //}

    //private void FixedUpdate()
    //{
    //    if (pm.wallrunning)
    //        WallRunningMovement();
    //}

    //private void CheckForWall()
    //{
    //    wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
    //    wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    //}

    //private bool AboveGround()
    //{
    //    return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    //}

    //private void StateMachine()
    //{
    //    // Getting Inputs
    //    horizontalInput = Input.GetAxisRaw("Horizontal");
    //    verticalInput = Input.GetAxisRaw("Vertical");

    //    // State 1 - Wallrunning
    //    if((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
    //    {
    //        if (!pm.wallrunning)
    //        {
    //            StartWallRun();
    //        }
    //    }

    //    else
    //    {
    //        if (pm.wallrunning)
    //        {
    //            StopWallRun(); 
    //        }
    //    }
    //}

    //private void StartWallRun()
    //{
    //    pm.wallrunning = true;
    //}

    //private void WallRunningMovement()
    //{
    //    rb.useGravity = false;
    //    rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

    //    Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

    //    Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

    //    if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
    //        wallForward = -wallForward;

    //    //forward force
    //    rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

    //    // push to wall force
    //    if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
    //        rb.AddForce(-wallNormal * 100, ForceMode.Force);
    //}

    //private void StopWallRun()
    //{
    //    pm.wallrunning = false;
    //}









    [Header("Wallrunning")]
    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float wallRunForce;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;

    [Header("References")]
    public Transform orientation;
    private newPlayerMovement pm;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<newPlayerMovement>();
    }

    private void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate()
    {
        if (pm.wallrunning)
            WallRunningMovement();
    }

    private void CheckForWall()
    {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, whatIsWall);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, whatIsWall);
    }

    private bool AboveGround()
    {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, whatIsGround);
    }

    private void StateMachine()
    {
        // Getting Inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // State 1 - Wallrunning
        if ((wallLeft || wallRight) && verticalInput > 0 && AboveGround())
        {
            if (!pm.wallrunning)
                StartWallRun();
        }

        // State 3 - None
        else
        {
            if (pm.wallrunning)
                StopWallRun();
        }
    }

    private void StartWallRun()
    {
        pm.wallrunning = true;
    }

    private void WallRunningMovement()
    {
        rb.useGravity = false;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        // forward force
        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // push to wall force
        if (!(wallLeft && horizontalInput > 0) && !(wallRight && horizontalInput < 0))
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
    }

    private void StopWallRun()
    {
        rb.useGravity = true;
        pm.wallrunning = false;
    }
}
