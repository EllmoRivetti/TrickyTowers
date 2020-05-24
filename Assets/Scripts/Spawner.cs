using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] Tetrominos;
    public GameObject container;

    // Start is called before the first frame update
    void Start()
    {
        AddTetromino();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTetromino()
    {
        Instantiate(Tetrominos[Random.Range(0, Tetrominos.Length)], transform.position, Quaternion.identity, container.transform);
    }
}
