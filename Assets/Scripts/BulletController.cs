using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] public float speed;
    [SerializeField] public float timeToLive;
    Vector3 moveVector;

    // Start is called before the first frame update
    void Start() {
        moveVector = Vector3.up * speed * Time.fixedDeltaTime;
        StartCoroutine(DestroyBullet());
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FixedUpdate() {
        transform.Translate(moveVector);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(timeToLive);
        Destroy(gameObject);
    }
}
