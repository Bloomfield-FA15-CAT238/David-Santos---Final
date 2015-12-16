using UnityEngine;
using System.Collections;

public class Death : MonoBehaviour {
    public GameObject RespawnPoint;
    public GameObject player;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other){

        player.gameObject.transform.position = RespawnPoint.gameObject.transform.position;
    }
}
