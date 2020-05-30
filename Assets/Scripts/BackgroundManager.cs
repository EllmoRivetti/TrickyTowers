using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    GameLoop gameLoop;

    [SerializeField]
    private GameObject backgroundParent;

    [SerializeField]
    private GameObject highestLayer;

    private int scoreToAchieve;
    private int scoreToAdd = 30;

    private void Start()
    {
        scoreToAchieve = scoreToAdd;
    }


    // Update is called once per frame
    void Update()
    {
        if(gameLoop.GetScore() >= scoreToAchieve)
        {
            Vector3 vec = new Vector3(highestLayer.transform.position.x, highestLayer.transform.position.y + 30, highestLayer.transform.position.z);
            highestLayer = Instantiate(highestLayer, vec, Quaternion.identity, backgroundParent.transform);
            scoreToAchieve += scoreToAdd;
        }
    }
}
