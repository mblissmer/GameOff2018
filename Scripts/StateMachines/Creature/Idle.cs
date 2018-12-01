using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{

    private float timer;
    private float timeLimit;
    private int boredAction;
    private int nextAction;



    public Idle(Creature creature) : base(creature) { }

    public override void OnStateEnter() {
        SetupWaiting();
    }

    public override void OnStateExit() {
        Debug.Log("idle stop");
        creature.anim.SetBool("sleeping", false);
    }

    public override void Tick() {
        switch (boredAction) {
            case -1:
                WaitingForMoveToStart();
                break;
            case 0:
                Waiting();
                break;
            case 1:
                Running();
                break;
            case 2:
                Walking();
                break;
            case 3:
                Napping();
                break;
            default:
                Debug.Log("wait what");
                break;
        }
    }

    #region Actions
    void WaitingForMoveToStart() {
        if (creature.ptp.moving) boredAction = nextAction;
    }

    void Waiting() {
        creature.cs.ChangeHunger(Time.deltaTime);
        creature.cs.ChangeHappiness(-Time.deltaTime);
        timer += Time.deltaTime;
        if (creature.stateLock) creature.stateLock = false;
        if (timer >= timeLimit) {
            int[] chances = new int[100];
            for (int i = 0; i < chances.Length; i++) {
                if (i <= creature.cs.tiredness.Value) chances[i] = chances[i] = 2;
                else chances[i] = Random.Range(0, 2);
            }
            int choice = chances[Random.Range(0, chances.Length)];
            switch (choice) {
                case 0:
                    SetupRunning();
                    break;
                case 1:
                    SetupWalking();
                    break;
                case 2:
                    SetupNapping();
                    break;
                default:
                    break;
            }
        }
    }

    void Running() {
        creature.cs.ChangeTiredness(Time.deltaTime * 2);
        creature.cs.ChangeHunger(Time.deltaTime * 2);
        creature.cs.ChangeHappiness(-Time.deltaTime);
        if (!creature.ptp.moving) {
            Debug.Log("idle running stop");
            SetupWaiting();
        }
    }

    void Walking() {
        creature.cs.ChangeTiredness(Time.deltaTime);
        creature.cs.ChangeHunger(Time.deltaTime);
        creature.cs.ChangeHappiness(-Time.deltaTime);
        if (!creature.ptp.moving) {
            Debug.Log("idle walking stop");
            SetupWaiting();
        }
    }

    void Napping() {
        creature.cs.ChangeHappiness(-Time.deltaTime);
        creature.cs.ChangeTiredness(-Time.deltaTime * 4);
        timer += Time.deltaTime;
        if (timer >= timeLimit) {
            creature.anim.SetBool("sleeping", false);
            SoundManager.sm.SnoringStop();
            SetupWaiting();
        }
    }
    #endregion


    #region Setup Tasks
    void SetupWaiting() {
        Debug.Log("idle start");
        timeLimit = Random.Range(5, 10);
        timer = 0;
        boredAction = 0;
    }

    void SetupRunning() {
        creature.stateLock = true;
        Debug.Log("idle running start");
        creature.ptp.RunSpeed();
        creature.ptp.MakeNewMove();
        PrepMoveAction(1);
    }

    void SetupWalking() {
        creature.stateLock = true;
        Debug.Log("idle walking start");
        creature.ptp.WalkSpeed();
        creature.ptp.MakeNewMove();
        PrepMoveAction(2);
    }

    void SetupNapping() {
        creature.stateLock = true;
        Debug.Log("sleeping start");
        creature.anim.SetBool("sleeping", true);
        timeLimit = Random.Range(5, 10);
        SoundManager.sm.SnoringStart();
        timer = 0;
        boredAction = 3;
    }
    #endregion

    void PrepMoveAction(int action) {
        boredAction = -1;
        nextAction = action;
    }
}