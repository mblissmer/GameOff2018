using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {

    private Animator anim;
    public ObjectController objectController;    
    public IntVariable activeGame;
    public Slider tirednessSlider;
    public Slider hungerSlider;
    public Slider happinessSlider;
    public Button[] inGameButtons;
    [Header("Debug Objects")]
    public bool debugging;
    public GameObject debugPanel;
    public FloatVariable tiredness;
    public FloatVariable hunger;
    public FloatVariable happiness;
    public Text stateTimerText;
    public Creature creature;

    private void Awake() {
        activeGame.Value = 0;
        Screen.fullScreen = false;
    }

    void Start () {
        anim = GetComponent<Animator>();
        for (int i = 0; i < inGameButtons.Length; i++) {
            inGameButtons[i].interactable = false;
        }
        debugPanel.SetActive(debugging);
                
    }

    private void Update() {
        if (debugging) {
            if (tiredness.Value != tirednessSlider.value) tirednessSlider.value = tiredness.Value;
            if (hunger.Value != hungerSlider.value) hungerSlider.value = hunger.Value;
            if (happiness.Value != happinessSlider.value) happinessSlider.value = happiness.Value;
            stateTimerText.text = Mathf.FloorToInt(creature.stuckTimer).ToString();
        }
    }

    public void PlayButton() {
        anim.SetFloat("speed", 1);
        anim.Play("MenuToGame");
        activeGame.Value = 1;
        iTween.AudioTo(gameObject, 0, 1, 5);
        SoundManager.sm.ButtonPressSound();
    }

    public void BrushButton() {
        objectController.SetState(objectController.brush);
    }

    public void FoodButton() {
        objectController.SetState(objectController.food);
    }

    public void ToyButton() {
        objectController.SetState(objectController.toy);
    }

    public void EmptyButton() {
        objectController.SetState(objectController.empty);
    }

    public void QuitButton() {
        activeGame.Value = 0;
        anim.SetFloat("speed",-1);
        //anim.playbackTime = 1;
        anim.Play("MenuToGame");
        // save and go back to main menu
    }

    public void HoverOverButton() {
        SoundManager.sm.ButtonHoverSound();
    }

    public void EnableInGameButtons() {
        for (int i = 0; i < inGameButtons.Length; i++) {
            inGameButtons[i].interactable = true;
        }
    }

    public void GoToURL(string url) {
        Application.OpenURL(url);
    }
}
