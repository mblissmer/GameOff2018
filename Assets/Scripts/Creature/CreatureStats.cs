using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStats : MonoBehaviour {

    public FloatVariable tiredness;
    public FloatVariable hunger;
    public FloatVariable happiness;

    private void Start() {
        tiredness.Value = 50;
        hunger.Value = 50;
        happiness.Value = 50;
    }

    public void ChangeTiredness(float change) {
        tiredness.Value = Mathf.Clamp(tiredness.Value += change, 0, 100);
    }

    public void ChangeHunger(float change) {
        hunger.Value = Mathf.Clamp(hunger.Value += change, 0, 100);
    }

    public void ChangeHappiness(float change) {
        happiness.Value = Mathf.Clamp(happiness.Value += change, 0, 100);
    }
}
