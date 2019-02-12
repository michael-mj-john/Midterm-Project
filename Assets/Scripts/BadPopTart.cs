using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is low difficulty. You should be able to 
 * describe everything that occurs in this class
 */

public class BadPopTart : MonoBehaviour {
    [HideInInspector] public float speed = 4;
    private Vector3 movementVector;
    private PopTartSpawner spawner;

    private void Start( ) {
        movementVector = new Vector3(Random.Range(0, -1), Random.Range(-1, 1), 0);
        movementVector = new Vector3(-10, Random.Range(-5, 5), 0);
        movementVector.Normalize();
        // reference spawner script
        spawner = GameObject.Find("Spawner").GetComponent<PopTartSpawner>(); 
    }

    private void Update() {
        this.transform.Translate(movementVector * speed * Time.deltaTime);
        //note: Mathf.Abs returns absolute value
        if ( this.transform.position.x < -10 || Mathf.Abs(this.transform.position.y) > 8 ) {
            spawner.eat(this.gameObject);
        }
    }

    public Vector2 getPosition() {
        return new Vector2(this.transform.position.x, this.transform.position.y);
    }

}
