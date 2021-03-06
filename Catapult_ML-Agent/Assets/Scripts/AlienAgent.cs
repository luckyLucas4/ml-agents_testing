﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MLAgents;

public class AlienAgent : Agent
{
    Rigidbody2D rBody;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }


    public Transform startPosition;
    public Transform coin;
    public CircleCollider2D coinCollider;
    public Text scoreText;

    public override void AgentReset()
    {
        if(this.transform.position.y < -1 || timer > maxTime)
        {
            // If the Agent fell, zero its momentum
            this.rBody.velocity = Vector2.zero;
            this.transform.position = startPosition.position;
        }

        timer = 0;

        // Move the coin to a new spot
        coin.position = new Vector3(Random.value * 14 - 2, -1.4f, 0);
    }

    public override void CollectObservations()
    {
        AddVectorObs(coin.position.x);
        AddVectorObs(this.transform.position.x);
        AddVectorObs(rBody.velocity);
    }

    public float speed = 10;
    public int maxTime = 1000;
    private int timer;
    public override void AgentAction(float[] vectorAction, string textAction)
    {
        // Actions, size = 1
        Vector2 controlSignal = Vector2.zero;
        controlSignal.x = vectorAction[0];
        rBody.AddForce(controlSignal * speed);

        // Rewards
        float distanceToTarget = Vector2.Distance(this.transform.position, coin.position);

        // Fell off platform
        if(this.transform.position.y < - 1)
        {
            Done();
        }

        // Time's up
        if(timer > maxTime)
        {
            Done();
        }
        timer++;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            SetReward(1.0f);
            Done();
            score++;
            scoreText.text = "Score: " + score.ToString();
        }
    }
}
