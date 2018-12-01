using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskAnim : MonoBehaviour {

    private SpriteMask sm;
    public Sprite[] sprites = new Sprite[4];
    private int index = 0;
    private float timer = 0;
    public float animSpeed;
    private bool forward;
    public bool animate;
    
	void Start () {
        sm = GetComponent<SpriteMask>();
	}


    public void Hide() {

    }

    private void Update() {
        if (animate) {
            if (!sm.enabled) sm.enabled = true;
            timer += Time.deltaTime;
            if (timer > animSpeed) {
                index++;
                if (index >= sprites.Length) {
                    index = 0;
                    animate = false;
                    sm.enabled = false;
                }
                else {
                    timer = 0;
                    sm.sprite = sprites[index];
                }
            }
        }
    }

    public void Reveal() {
        animate = true;
        SoundManager.sm.DrawingSound();
    }
}
