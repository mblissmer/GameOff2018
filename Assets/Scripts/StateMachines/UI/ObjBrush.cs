using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjBrush : State {

    public ObjBrush(ObjectController oc) : base(oc) { }

    public override void OnStateEnter() {
        oc.brushObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10));
        oc.brushObj.SetActive(true);
    }

    public override void OnStateExit() {
        oc.brushObj.SetActive(false);
    }

    public override void Tick() {
        oc.brushObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }
}
