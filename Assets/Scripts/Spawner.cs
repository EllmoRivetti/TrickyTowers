using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] Tetrominos;
    public GameObject container;
    static int iDBlock = 0;

    // Start is called before the first frame update
    void Start()
    {
        //AddTetromino();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject AddTetromino()
    {
        GameObject tetromino = Tetrominos[Random.Range(0, Tetrominos.Length)];
        iDBlock++;
        return Instantiate(tetromino, transform.position, Quaternion.identity, container.transform);
    }

    public int getIdNb()
    {
        return iDBlock;
    }
}
