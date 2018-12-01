using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFood : State {

    private FoodPooler fp;

    public ObjFood(ObjectController oc) : base(oc) {
        fp = oc.gameObject.GetComponent<FoodPooler>();
    }

    public override void OnStateEnter() {
        oc.foodObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        oc.foodObj.SetActive(true);
    }

    public override void OnStateExit() {
        oc.foodObj.SetActive(false);
    }

    public override void Tick() {
        oc.foodObj.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        if (Input.GetMouseButtonDown(0) && 
            oc.foodObj.transform.position.x > oc.LLLimits.Value.x &&
            oc.foodObj.transform.position.x < oc.URLimits.Value.x &&
            oc.foodObj.transform.position.y > oc.LLLimits.Value.y &&
            oc.foodObj.transform.position.y < oc.URLimits.Value.y) {
            GameObject o = fp.GetPooledObject();
            if (o != null) {
                o.transform.position = oc.foodObj.transform.position;
                o.SetActive(true);
                SoundManager.sm.DropSound();
            }
            
        }
    }
}
