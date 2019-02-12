using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    [SerializeField] private float scrollSpeed = 3;

    private void Update() {
        this.transform.Translate(-1*scrollSpeed*Time.deltaTime, 0, 0);
        if( this.transform.position.x < -17.7f ) {
            this.transform.Translate(17.7f * 2.0f, 0, 0);
        }
    }
}
