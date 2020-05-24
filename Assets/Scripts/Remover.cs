﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remover : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<TetrisBlock>().m_hasToCollide)
            //collision.gameObject.GetComponent<TetrisBlock>().m_hasToCollide = false;
            collision.gameObject.GetComponent<TetrisBlock>().ActionOnCollide(null);
        Destroy(collision.gameObject);
    }
}