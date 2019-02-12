using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanCat_Easy : MonoBehaviour {

    [SerializeField] private float speed = 5;
    private PopTartSpawner targetSpawner;
    private Vector3 returnPoint;
    [SerializeField] private float maxTrackingDistance = 5;

    void Start() {
        targetSpawner = GameObject.Find("Spawner").GetComponent<PopTartSpawner>();
        returnPoint = this.transform.position; // assume this is the center of the screen
    }

    void Update() {
        Vector3 moveTarget = findTarget();
        this.transform.position = 
            Vector3.MoveTowards(this.transform.position, moveTarget, Time.deltaTime * speed);
    }

    private Vector3 findTarget() {

        Vector3 location = returnPoint;

        float minDist = Mathf.Infinity; // built-in method for "extremely large number"
        foreach (GameObject target in targetSpawner.badPopTarts) {
            float distance = getDistance(target);
            if (distance < 1.0) {
                targetSpawner.eat(target);
                break;
            }
            else if (distance < minDist && distance < maxTrackingDistance) {
                location = target.transform.position;
                minDist = distance;
            }
        }

        return location;

    }

    private float getDistance(GameObject gameObject) {
        return Vector2.Distance(this.transform.position, gameObject.transform.position);
    }

}
