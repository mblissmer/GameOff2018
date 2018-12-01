using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {

    private State currentState;
    public State atMenu;
    public State idle;
    public State eating;
    public State fleeing;
    public State gettingPet;
    public State playingWithToy;
    public IntVariable activeGame;
    private Vector3 mouseLocation;
    public bool mouseMoving;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public PointToPoint ptp;
    [HideInInspector]
    public MaskAnim ma;
    [HideInInspector]
    public CreatureStats cs;
    public FoodPooler fp;
    public Toy toy;
    public GameObject brush;
    [HideInInspector]
    public GameObject nearestFood;
    public Transform mouth;
    public bool stateLock;
    public bool pettingCollision;
    public float stuckTimer = 0;
    private float stuckTimeLimit = 10;
    private State prevState;
    private Vector3 creatureLocation;
    public ParticleSystem ps;

    private void Awake() {
        atMenu = new AtMenu(this);
        idle = new Idle(this);
        gettingPet = new GettingPet(this);
        eating = new Eating(this);
        playingWithToy = new PlayingWithToy(this);
    }
    private void Start() {
        cs = GetComponent<CreatureStats>();
        anim = GetComponent<Animator>();
        ptp = GetComponent<PointToPoint>();
        ma = GetComponentInChildren<MaskAnim>();
        SetState(atMenu);

    }

    private void Update() {
        GetMouseInfo();
        if (!stateLock) {
            DecideOnState();
        }
        currentState.Tick();
        StuckCheck();
    }

    void GetMouseInfo() {
        if (mouseLocation != Input.mousePosition) {
            mouseMoving = true;
            mouseLocation = Input.mousePosition;
        }
        else {
            mouseMoving = false;
        }
    }

    void DecideOnState() {
        if (cs.hunger.Value > 10) {
            if (LookForFood()) return;
        }
        if (cs.happiness.Value < 80) {
            if (CheckForPets()) return;
        }
        if (cs.tiredness.Value < 75) {
            if (LookForToy()) return;
        }
        SetState(idle);
    }

    bool LookForFood() {
        bool found = false;
        GameObject f = fp.FindNearest(transform.position);
        if (f != null) {
            nearestFood = f;
            SetState(eating);
            found = true;
        }
        return found;
    }

    bool CheckForPets() {
        bool demPets = false;
        if (mouseMoving && Input.GetMouseButton(0) && pettingCollision) {
            SetState(gettingPet);
            demPets = true;
        }
        return demPets;
    }

    bool LookForToy() {
        bool found = false;
        Vector2 pos = toy.GetPosition();
        if (pos.x + pos.y < 100000) {
            SetState(playingWithToy);
            found = true;
        }
        return found;
    }

    public void SetState(State state) {
        if (state == idle && currentState == idle) return;
        if (state == playingWithToy && currentState == playingWithToy) return;
        stateLock = true;
        if (currentState != null) currentState.OnStateExit();
        currentState = state;
        gameObject.name = "Creature - " + state.GetType().Name;
        if (currentState != null) currentState.OnStateEnter();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.name == brush.name) {
            pettingCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == brush.name) {
            pettingCollision = false;
        }
    }

    public void AnimationEnd() {
        currentState.OnAnimationEnd();
    }

    public void MovementEnd() {
        currentState.OnMovementEnd();
    }

    void StuckCheck() {
        if (currentState != idle && currentState != atMenu) {
            if (currentState == prevState && creatureLocation == transform.position) {
                stuckTimer += Time.deltaTime;
                if (stuckTimer > stuckTimeLimit) {
                    Debug.LogWarning("Stuck Override Hit, setting status to idle");
                    //SetState(idle);
                    //anim.SetBool("running", false);
                    //anim.SetBool("walking", false);
                    //anim.SetBool("sleeping", false);
                    //anim.SetBool("eating", false);
                    //anim.SetBool("gettingPet", false);
                    //anim.SetBool("playing", false);
                    stuckTimer = 0;
                }
            }
            else {
                prevState = currentState;
                stuckTimer = 0;
            }
        }
        else if (stuckTimer != 0) {
            stuckTimer = 0;
        }

        creatureLocation = transform.position;
    }
}
