using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NyanCat : MonoBehaviour {

    [SerializeField] private float speed = 5;
    private PopTartSpawner targetSpawner;
    private Vector3 returnPoint;
    [SerializeField] private float maxTrackingDistance = 5;
    private Toaster toaster;
    // hard difficulty fields
    private enum quality { good, bad }
    private Stack<GameObject> rainbows;
    public GameObject rainbow;
    
    //these three collapsed methods are viewable on the prior page
    void Start() {
        targetSpawner = GameObject.Find("Spawner").GetComponent<PopTartSpawner>();
        returnPoint = this.transform.position; // assume this is the center of the screen
        toaster = GameObject.Find("toaster").GetComponent<Toaster>();
        rainbows = new Stack<GameObject>();  // hard difficulty
    }

    void Update() {
        Vector3 moveTarget = findTarget();
        this.transform.position = 
            Vector3.MoveTowards(this.transform.position, moveTarget, Time.deltaTime * speed);
        if( rainbows.Count > 0 ) {   // high difficulty.
            updateRainbows();
        }
    }

    private Vector3 findTarget() {
        Vector3 location = returnPoint;

        if (toaster.goodPopTart != null) {
            location = toaster.goodPopTart.transform.position;
            if (getDistance(toaster.goodPopTart) < 1.0 ) {
                eatPopTart(quality.good); // this line is part of the hard difficulty portion
                toaster.removePopTart();
            }
        }

        float minDist = Mathf.Infinity; // built-in method for "extremely large number"
        foreach (GameObject target in targetSpawner.badPopTarts) {
            float distance = getDistance(target);
            if ( distance < 1.0 ) {
                targetSpawner.eat(target);
                eatPopTart(quality.bad); // this line is high difficulty
                break;
            }
            else if (distance < minDist && distance < maxTrackingDistance ) {
                location = target.transform.position;
                minDist = distance;
            }
        }
        return location;
    }

    private float getDistance( GameObject gameObject ) {
        return Vector2.Distance(this.transform.position, gameObject.transform.position);
    }

    // the following method is high difficulty (we have not gone over stacks in class)
    private void eatPopTart( quality thisQuality ) {
        if( thisQuality == quality.good ) {
            GameObject thisRainbow = Instantiate(rainbow, this.transform);
            rainbows.Push(thisRainbow);
        }
        if( thisQuality == quality.bad && rainbows.Count > 0 ) {
            GameObject temp = rainbows.Pop();
            Destroy(temp);
        }
    }

    private void updateRainbows() {
        // offset and initialOffset are hard-coded based on the sprite size (bad engineering practice)
        float offset = 1.0f;
        float initialOffset = 0.4f;
        int counter = 1;
        foreach (GameObject thisRainbow in rainbows) {
            thisRainbow.transform.position = 
                new Vector2(transform.position.x - initialOffset - (offset * counter), transform.position.y);
            counter++;
        }
    }
}
