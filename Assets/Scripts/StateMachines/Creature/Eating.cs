using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eating : State {

    public Eating(Creature creature) : base(creature) {

    }

    public override void OnStateEnter() {
        Debug.Log("eating start");
        creature.anim.SetBool("eating", true);
        if (creature.cs.hunger.Value > 15) creature.ptp.RunSpeed();
        else creature.ptp.WalkSpeed();
        creature.ptp.MakeNewMove(creature.nearestFood.transform.position);

    }

    public override void OnStateExit() {
        Debug.Log("eating stop");
        creature.anim.SetBool("eating", false);
    }

    public override void Tick() { }

    public override void OnAnimationEnd() {
        SoundManager.sm.EatingSound();
        creature.ps.Play();
        Debug.Log("ate the food");
        creature.nearestFood.SetActive(false);
        creature.anim.SetBool("eating", false);
        creature.stateLock = false;
        creature.SetState(creature.idle);
    }

    public override void OnMovementEnd() {
        creature.cs.ChangeHunger(-10);
    }
}
