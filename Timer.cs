using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class Timer : MonoBehaviour {

    

    public Text counterText;
    public bool finished = false;
    public float startTime;

	// Use this for initialization
	void Start () {
        startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float t = Time.time - startTime;
        string minutes = ((int) t / 60).ToString("f1");
        string seconds = (t % 60).ToString("f1");

        counterText.text = minutes + ":" + seconds;
	}

    public void Finished()
    {
        finished = true;
        counterText.color = Color.yellow;
    }
}
