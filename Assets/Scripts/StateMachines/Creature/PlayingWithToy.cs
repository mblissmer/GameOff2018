using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingWithToy : State {

    private float timer;
    private float timeLimit;
    private float soundTimer;
    private float soundTimeLimit;
    private bool hasToy;
    private int playAction;
    private int nextAction;

    public PlayingWithToy(Creature creature) : base(creature) {

    }

    public override void OnStateEnter() {
        Debug.Log("toy start");
        creature.anim.SetBool("playing", true);
        playAction = -2;
        creature.ptp.RunSpeed();
        creature.ptp.MakeNewMove(creature.toy.transform.position);

    }

    public override void OnStateExit() {
        Debug.Log("toy stop");
        creature.anim.SetBool("playing", false);
        hasToy = false;
        creature.toy.heldByCreature = false;
    }

    public override void Tick() {
        if (creature.toy.heldByCreature) {
            creature.toy.transform.position = creature.mouth.position;
        }
        if (hasToy) {
            switch (playAction) {
                case -1:
                    WaitingForMoveToStart();
                    break;
                case 0:
                    Chewing();
                    break;
                case 1:
                    Running();
                    break;
                case 2:
                    Napping();
                    break;
                default:
                    Debug.Log(playAction);
                    break;
            }
        }
    }

    #region Actions
    void WaitingForMoveToStart() {
        if (creature.ptp.moving) playAction = nextAction;
    }

    void Chewing() {
        creature.cs.ChangeHunger(Time.deltaTime);
        creature.cs.ChangeHappiness(Time.deltaTime);
        if (soundTimer == 0) SoundManager.sm.GrowlSound();
        soundTimer += Time.deltaTime;
        if (soundTimer > soundTimeLimit) {
            soundTimer = 0;
            soundTimeLimit = Random.Range(0.5f, 3);
        }
        timer += Time.deltaTime;
        if (creature.stateLock) creature.stateLock = false;
        if (timer >= timeLimit) {
            int[] chances = new int[100];
            for (int i = 0; i < chances.Length; i++) {
                if (i <= creature.cs.tiredness.Value) chances[i] = chances[i] = 1;
                else chances[i] = 0;
            }
            int choice = chances[Random.Range(0, chances.Length)];
            switch (choice) {
                case 0:
                    SetupRunning();
                    break;
                case 1:
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
            Debug.Log("toy running stop");
            SetupChewing();
        }
    }

    void Walking() {
        creature.cs.ChangeTiredness(Time.deltaTime);
        creature.cs.ChangeHunger(Time.deltaTime);
        creature.cs.ChangeHappiness(-Time.deltaTime);
        if (!creature.ptp.moving) {
            Debug.Log("toy walking stop");
            SetupChewing();
        }
    }

    void Napping() {
        creature.cs.ChangeHappiness(-Time.deltaTime);
        creature.cs.ChangeTiredness(-Time.deltaTime * 4);
        timer += Time.deltaTime;
        if (timer >= timeLimit) {
            creature.anim.SetBool("sleeping", false);
            SoundManager.sm.SnoringStop();
            SetupChewing();
        }
    }
    #endregion


    #region Setup Tasks
    void SetupChewing() {
        Debug.Log("toy chewing start");
        timeLimit = Random.Range(5, 8);
        timer = 0;
        soundTimer = 0;
        soundTimeLimit = Random.Range(1, 4);
        playAction = 0;
    }

    void SetupRunning() {
        creature.stateLock = true;
        Debug.Log("toy running start");
        creature.ptp.RunSpeed();
        creature.ptp.MakeNewMove();
        PrepMoveAction(1);
    }

    void SetupWalking() {
        creature.stateLock = true;
        Debug.Log("toy walking start");
        creature.ptp.WalkSpeed();
        creature.ptp.MakeNewMove();
        PrepMoveAction(2);
    }

    void SetupNapping() {
        creature.stateLock = true;
        Debug.Log("sleeping start");
        creature.anim.SetBool("sleeping", true);
        SoundManager.sm.SnoringStart();
        timeLimit = Random.Range(5, 10);
        timer = 0;
        playAction = 2;
    }
    #endregion

    void PrepMoveAction(int action) {
        playAction = -1;
        nextAction = action;
    }

    public override void OnMovementEnd() {
        if (!hasToy) {
            Debug.Log("pick up toy");
            if (creature.toy.dropped && Vector2.Distance(creature.toy.transform.position, creature.mouth.position) < 1) {
                hasToy = true;
                creature.toy.heldByCreature = true; //set toy to follow creature
                SetupRunning();
            }
            else creature.SetState(creature.idle);
        }
    }
}