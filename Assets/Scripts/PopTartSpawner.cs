using System.Collections.Generic;
using UnityEngine;

public class PopTartSpawner : MonoBehaviour
{
    public GameObject badPopTart; // poptart prefab is assigned in Inspector
    public List<GameObject> badPopTarts = new List<GameObject>();

    private float lastSpawn;
    [SerializeField] private float spawnInterval = 1;

    void Start() {
        lastSpawn = Time.time;
    }

    void Update() {
        if( Time.time - spawnInterval > lastSpawn) {
            badPopTarts.Add(spawnBadPopTart());
            lastSpawn = Time.time;
        }
    }

    private GameObject spawnBadPopTart() {
        float yPos = Random.Range(-2.0f, 3.0f);
        // instantiate interface is: prefab, position, and rotation
        GameObject thisPopTart = Instantiate(badPopTart, new Vector3(8.0f, yPos, 0), Quaternion.identity );
        thisPopTart.GetComponent<BadPopTart>().speed = Random.Range(3.0f, 5.0f);
        return thisPopTart;
    }

    public void eat( GameObject eaten) {
        badPopTarts.Remove(eaten);
        Destroy(eaten);
    }
}
