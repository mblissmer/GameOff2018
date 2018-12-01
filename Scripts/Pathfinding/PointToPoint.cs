using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Seeker))]
public class PointToPoint : MonoBehaviour {

    public Vec2Variable LLLimits;
    public Vec2Variable URLimits;
    private Seeker seeker;
    public float fullSpeed = 10;
    public float altSpeed = 5;
    public float speed;
    private Path path;
    private int currentWaypoint = 0;
    public bool moving;
    public bool moveStarted;
    private Creature creature;
    private bool stepSound;
    private float stepTimer;
    private float stepTimeLimit;


    void Start() {
        seeker = GetComponent<Seeker>();
        speed = fullSpeed;
        creature = GetComponent<Creature>();
    }

    private void Update() {
        if (moving) { 
            if (stepTimer == 0) SoundManager.sm.FootstepsSound();
            stepTimer += Time.deltaTime;
            if (stepTimer > stepTimeLimit) {
                stepTimer = 0;
            }
        }
    }

    public void MakeNewMove() {
        moveStarted = true;
        Vector3 dest = new Vector3(
            Random.Range(LLLimits.Value.x, URLimits.Value.x),
            Random.Range(LLLimits.Value.y, URLimits.Value.y),
            0);
        StartCoroutine(StartPath(dest));
    }

    public void MakeNewMove(Vector3 dest) {
        moveStarted = true;
        StartCoroutine(StartPath(dest));
    }

    IEnumerator StartPath(Vector3 dest) {
        GraphNode node = AstarPath.active.GetNearest(dest, NNConstraint.Default).node;
        seeker.StartPath(transform.position, (Vector3)node.position, OnPathGenerated);
        yield return null;
    }

    void OnPathGenerated(Path p) {
        if (p.error) {
            Debug.Log("Pathing error: " + p.error);
            moveStarted = false;
        }
        else {
            HaveNewPath(p);
        }
    }


    void HaveNewPath(Path p) {
        moving = true;
        stepTimer = 0;
        path = p;
        currentWaypoint = 0;
        if (speed == altSpeed) creature.anim.SetBool("walking", true);
        else creature.anim.SetBool("running", true);
        Step();
    }


    void Step() {
        if (currentWaypoint <= path.vectorPath.Count - 1) {
            iTween.MoveTo(gameObject, iTween.Hash(
                "position", path.vectorPath[currentWaypoint],
                "speed", speed,
                "name", "move",
                "easetype", iTween.EaseType.linear,
                "oncomplete", "Step"));
            currentWaypoint++;
        }
        else {
            moving = false;
            moveStarted = false;
            if (speed == altSpeed) creature.anim.SetBool("walking", false);
            else creature.anim.SetBool("running", false);
            creature.MovementEnd();
        }
    }

    public void Stop() {
        iTween.StopByName(gameObject, "move");
        moving = false;
        moveStarted = false;
    }

    public void RunSpeed() {
        speed = fullSpeed;
        stepTimeLimit = 0.2f;
        stepTimer = 0;
    }

    public void WalkSpeed() {
        speed = altSpeed;
        stepTimeLimit = 0.5f;
        stepTimer = 0;
    }
}
