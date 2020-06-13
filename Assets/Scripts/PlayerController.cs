using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {


    private PlayerControls playerControls;
    private Rigidbody2D rb;
    [SerializeField] private float playerSpeed = 4f;
    private float xLeftInput, yLeftInput;
    private float xRightInput, yRightInput;
    private Vector2 rightInput;

    private void Awake() {
        playerControls = new PlayerControls();
    }

    private void OnEnable() {
        playerControls.Enable();
    }

    private void OnDisable() {
        playerControls.Disable();

    }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        GetPlayerInput();


    }

    //Read the movement value
    private void GetPlayerInput() {
        xLeftInput = playerControls.Movement.Horizontal.ReadValue<float>();
        yLeftInput = playerControls.Movement.Vertical.ReadValue<float>();

        xRightInput = playerControls.Movement.HorizontalRotation.ReadValue<float>();
        yRightInput = playerControls.Movement.VerticalRotation.ReadValue<float>();

        rightInput = new Vector2(xRightInput, yRightInput);

    }

    private void FixedUpdate()
    {

        Vector2 currentMovement = new Vector2(xLeftInput * playerSpeed * Time.deltaTime, yLeftInput * playerSpeed * Time.deltaTime);
        rb.MovePosition(rb.position + currentMovement);

        Vector3 currentRotation = Vector3.left * rightInput.x + Vector3.down * rightInput.y;
        Quaternion playerRotation = Quaternion.LookRotation(currentRotation, Vector3.forward);

        rb.SetRotation(playerRotation);
    }

}
