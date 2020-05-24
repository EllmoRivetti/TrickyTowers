using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PowerUp))]
public class GameLoop : MonoBehaviour
{
    public Spawner spawner;
    private bool isPlaying = false;
    private List<GameObject> blockList;
    private bool currentBlock = false;
    // Start is called before the first frame update
    void Start()
    {
        blockList = new List<GameObject>();
        //if (Input.GetKeyDown(KeyCode.Space) && isPlaying == false) { 
            Spawn();
            isPlaying = true;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        if (!currentBlock)
        {
            GameObject block = spawner.AddTetromino();
            block.GetComponent<TetrisBlock>().TouchGround += OnTouchGround;
            blockList.Add(block);
            currentBlock = true;
        }
    }

    void OnTouchGround(object sender, EventArgs e)
    {
        TetrisBlock block = sender as TetrisBlock;
        block.TouchGround -= OnTouchGround;
        currentBlock = false;
        Spawn();
        
    }


}
