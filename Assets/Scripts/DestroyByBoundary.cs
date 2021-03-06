﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{
    public int scoreValue;
    private GameController gameController;
    void Start() {
    GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null) {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null) {
            Debug.Log("Cannot find 'GameController' script");
        }
    }
    void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
        if (other.gameObject.tag == "Asteroid") {
            gameController.AddScore(scoreValue);
        }
    }
}
