using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {


    [SerializeField] public float playerSpeed;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firePoint;
    [SerializeField] public float timeBetweenShots, timeBetweenHits;
    public int maxHealth = 3;
    public int currentHealth;
    public HealthBarController healthBar;

    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private float xLeftInput, yLeftInput;
    private float xRightInput, yRightInput;
    private Vector2 rightInput;
    private bool canShoot, canBeHit, previouslyHit;

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
        canShoot = true;
        canBeHit = true;
        previouslyHit = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update() {
        if(GameController.instance.gamePlaying)
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

    private void Walk() {
        Vector2 currentMovement = new Vector2(xLeftInput * playerSpeed * Time.deltaTime, yLeftInput * playerSpeed * Time.deltaTime);
        rb.MovePosition(rb.position + currentMovement);
    }

    private void Shoot() {
        canShoot = false;
        Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        StartCoroutine(ShotCooldown());
    }

    private void Turn() {
        Vector3 currentRotation = Vector3.left * rightInput.x + Vector3.down * rightInput.y;
        Quaternion playerRotation = Quaternion.LookRotation(currentRotation, Vector3.forward);

        rb.SetRotation(playerRotation);

    }

    IEnumerator TurnCooldown() {
        Turn();
        yield return new WaitForEndOfFrame();
        if(canShoot)
            Shoot();
    }

    IEnumerator ShotCooldown() {
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    IEnumerator HitCooldown(){
        yield return new WaitForSeconds(timeBetweenHits);
        canBeHit = true;
        previouslyHit = false;
    }

    private void FixedUpdate() {
        Walk();

        if ((xRightInput != 0 || yRightInput != 0)) {
            StartCoroutine(TurnCooldown());
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" && canBeHit && !previouslyHit) {
            canBeHit = false;
            previouslyHit = true;
            currentHealth--;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(HitCooldown());
            if (currentHealth <= 0)  {
                Destroy(gameObject);
                GameController.instance.EndGame();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy" && canBeHit && !previouslyHit) {
            canBeHit = false;
            previouslyHit = true;
            currentHealth--;
            healthBar.SetHealth(currentHealth);
            StartCoroutine(HitCooldown());
            if (currentHealth <= 0) { 
                Destroy(gameObject);
                GameController.instance.EndGame();
            }
        }
    }
}
