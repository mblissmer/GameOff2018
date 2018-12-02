using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {
    
    private SpriteRenderer sr;


    void Awake () {
        sr = GetComponent<SpriteRenderer>();
	}

    private void OnEnable() {
        SetShadow();
    }

    void SetShadow() {
        switch (sr.sortingLayerName) {
            case "Above":
                sr.color = new Color(0, 0, 0, 0.2f);
                transform.localPosition = new Vector3(-0.2f, -0.2f, 0);
                break;
            case "Touching":
                sr.color = new Color(0, 0, 0, 0.3f);
                transform.localPosition = new Vector3(-0.1f, -0.1f, 0);
                break;
            default:
                break;
        }
    }
}
