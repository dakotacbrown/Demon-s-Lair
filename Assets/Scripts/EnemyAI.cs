using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{

    public Transform player;
    public Transform enemy;
    public float speed;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .3f);
    }

    void UpdatePath() {
        if(seeker.IsDone() && player != null)
            seeker.StartPath(rb.position, player.position, OnPathComplete);

    }

    // Update is called once per frame
    void Update() {
        
    }

    private void FixedUpdate() {
        if (path == null)
            return;
        if(currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
            currentWaypoint++;

        if (force.x >= 0.01f)
            enemy.localScale = new Vector3(-1f, 1f, 1f);
        else if (force.x <= -0.01f)
            enemy.localScale = new Vector3(1f, 1f, 1f);

    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }
}
