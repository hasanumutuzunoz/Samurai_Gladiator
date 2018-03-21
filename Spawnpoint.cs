using UnityEngine;

public class Spawnpoint : MonoBehaviour {

    public Unit enemyPrefab;
    private float timeToSpawn = 3; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Spawn enemy every 3 seconds
	void Update () {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0)
        {
            Unit clone = Instantiate(enemyPrefab, transform.position, transform.rotation);
            timeToSpawn = 3;
        }
        
    }
}
