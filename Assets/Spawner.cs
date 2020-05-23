﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] Tetrominos;


    // Start is called before the first frame update
    void Start()
    {
        addTetromino();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addTetromino()
    {
        Instantiate(Tetrominos[Random.Range(0, Tetrominos.Length)], transform.position, Quaternion.identity);
    }
}
