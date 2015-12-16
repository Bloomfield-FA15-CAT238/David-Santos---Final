using UnityEngine;
using System.Collections;

public class Winning : MonoBehaviour
{
    public GameController2 GameController2;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        GameController2.Win();
    }
}
