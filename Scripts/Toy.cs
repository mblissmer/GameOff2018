using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toy : MonoBehaviour {

    //public Vec2Variable LLLimits;
    //public Vec2Variable URLimits;
    private ObjectController oc;
    public bool dropped = false;
    private Collider2D col;
    private Vector3 lastpos;
    //private Vector3 velocity;
    private Rigidbody2D rb;
    public bool heldByCreature;
    //public Transform creatureMouth;
	

	void Start () {
        oc = GetComponentInParent<ObjectController>();
        col = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        col.enabled = false;
        lastpos = Input.mousePosition;
	}

    public void Drop() {
        oc.toyDropped = true;
        dropped = true;
        col.enabled = true;
        SoundManager.sm.DropSound();
    }

    private void OnMouseDown() {
        if (dropped) {
            dropped = false;
            oc.toyDropped = false;
            col.enabled = false;
            heldByCreature = false;
            oc.SetState(oc.toy);
            SoundManager.sm.PickUp();
        }
    }

    private void Update() {
        if (!heldByCreature) {
            if (!dropped) {
                rb.velocity = Input.mousePosition - lastpos;
                lastpos = Input.mousePosition;
            }

        }
    }

    private void FixedUpdate() {
        if (!heldByCreature && dropped && 
            (transform.position.x < oc.LLLimits.Value.x ||
            transform.position.x > oc.URLimits.Value.y ||
            transform.position.y < oc.LLLimits.Value.y ||
            transform.position.y > oc.URLimits.Value.y)) {
            float xpos = Mathf.Clamp(transform.position.x, oc.LLLimits.Value.x, oc.URLimits.Value.x);
            float ypos = Mathf.Clamp(transform.position.y, oc.LLLimits.Value.y, oc.URLimits.Value.y);
            rb.velocity = Vector2.zero;
            rb.MovePosition(new Vector2(xpos, ypos));
        }
    }

    public Vector2 GetPosition() {
        Vector2 result = new Vector2(Mathf.Infinity, Mathf.Infinity);
        if (dropped) result = transform.position;
        return result;
    }
}
