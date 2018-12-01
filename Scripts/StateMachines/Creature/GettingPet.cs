using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingPet : State {

    public float timer;
    private readonly float minLockTime = 1;
    private float soundTimer;
    private float soundTimeLimit;

    public GettingPet(Creature creature) : base(creature) {

    }

    public override void OnStateEnter() {
        Debug.Log("petting start");
        soundTimer = 0;
        soundTimeLimit = Random.Range(2, 3);
        timer = 0;
        creature.anim.SetBool("gettingPet", true);
        creature.anim.Play("GettingPet");
        SoundManager.sm.BrushingStart();
    }

    public override void OnStateExit() {
        Debug.Log("petting stop");
        SoundManager.sm.BrushingStop();
        creature.anim.SetBool("gettingPet", false);
        
    }

    public override void Tick() {
        soundTimer += Time.deltaTime;
        if (soundTimer > soundTimeLimit) {
            SoundManager.sm.PurrSound();
            soundTimer = 0;
            soundTimeLimit = Random.Range(2, 3);
        }
        creature.cs.ChangeHappiness(Time.deltaTime * 4);
        if (creature.mouseMoving &&
            Input.GetMouseButton(0) &&
            creature.pettingCollision &&
            creature.cs.happiness.Value < 90) {
            timer = 0;
        }
        else {
            timer += Time.deltaTime;
            if (timer > minLockTime) {
                creature.SetState(creature.idle);
            }
        }
    }
}
