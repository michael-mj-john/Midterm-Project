using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * this class is medium difficulty. This is the interactive portion
 * of the game. 
 */

public class Toaster : MonoBehaviour
{
    public GameObject projectile;
    public GameObject launchPoint;
    public AudioClip popup;
    private AudioSource source;
    [HideInInspector] public GameObject goodPopTart;
    [SerializeField] private float popForce = 500;
    
    void Start() {
        goodPopTart = null;
        source = GetComponent<AudioSource>();
    }

    void Update() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 positionTarget = 
            new Vector3(mousePos.x, this.transform.position.y, this.transform.position.z);
        float smoothingSpeed = 10;
        this.transform.position = 
            Vector3.Lerp(this.transform.position, positionTarget, smoothingSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0)) {
            fireGoodPoptart();
        }

        if (goodPopTart != null) {
            if (Vector2.Distance(this.transform.position, goodPopTart.transform.position) < 1.0 ||
                goodPopTart.transform.position.y < -8) {
                removePopTart();
            }
        }
     }

    void fireGoodPoptart() {
        if( goodPopTart == null ) {
            goodPopTart = Instantiate(projectile, launchPoint.transform.position, Quaternion.identity);
            // the Good pop tart has a rigidbody2D component
            goodPopTart.GetComponent<Rigidbody2D>().AddForce(transform.up * popForce); 
            source.PlayOneShot(popup, 1.0f);
        }
    }

    public void removePopTart() {
        Destroy(goodPopTart.gameObject);
    }
}
