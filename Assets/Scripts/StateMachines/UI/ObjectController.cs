using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

    public static ObjectController oc;
    public Vec2Variable LLLimits;
    public Vec2Variable URLimits;
    private State currentState;
    public State empty;
    public State brush;
    public State food;
    public State toy;
    public GameObject brushObj;
    public GameObject foodObj;
    public GameObject toyObj;
    public bool toyDropped;

    private void Awake() {
        oc = this;
        empty = new ObjEmpty(this);
        brush = new ObjBrush(this);
        food = new ObjFood(this);
        toy = new ObjToy(this);
        brushObj.SetActive(false);
        foodObj.SetActive(false);
        toyObj.SetActive(false);
    }

    void Start () {
        SetState(empty);
    }

	void Update () {
        currentState.Tick();
    }

    public void SetState(State state) {
        if (currentState != null) currentState.OnStateExit();
        currentState = state;
        gameObject.name = "Creature - " + state.GetType().Name;
        if (currentState != null) currentState.OnStateEnter();
    }
}
