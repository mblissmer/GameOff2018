using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    private SpriteRenderer sr;
    public SpriteRenderer srChild;

    private void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }
    
    public void SetSprite(Sprite s) {
        sr.sprite = s;
        srChild.sprite = s;
    }
}
