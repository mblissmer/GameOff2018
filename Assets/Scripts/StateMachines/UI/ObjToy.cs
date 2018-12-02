using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjToy : State
{

    private bool dropped;
    private Toy toy;

    public ObjToy(ObjectController oc) : base(oc) {
        toy = oc.toyObj.GetComponent<Toy>();
    }

    public override void OnStateEnter() {
        if (!dropped) {
            oc.toyObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            oc.toyObj.SetActive(true);
        }
    }

    public override void OnStateExit() {
        if (!dropped) {
            oc.toyObj.SetActive(false);
        }
    }

    public override void Tick() {
        if (!dropped) {
            oc.toyObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            if (Input.GetMouseButtonDown(0) &&
                oc.toyObj.transform.position.x > oc.LLLimits.Value.x &&
                oc.toyObj.transform.position.x < oc.URLimits.Value.x &&
                oc.toyObj.transform.position.y > oc.LLLimits.Value.y &&
                oc.toyObj.transform.position.y < oc.URLimits.Value.y) {
                dropped = true;
                oc.toyDropped = true;
                toy.Drop();

            }
        }
        else {
            if (!oc.toyDropped) dropped = false;
        }
    }
}
