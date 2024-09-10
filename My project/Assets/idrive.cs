using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idrive : MonoBehaviour
{
    public float acceleration = 5f;
    public float maxSpeed = 20f;
    public float maxSpeedIncrease = 10f;
    public float maxTurnSpeedIncrease = 100f;
    public float turnSpeed = 200f;
    public float drag = 3f;
    public float bdrag = 3f;
    public float driftFactor = 0.95f;
    public float driftRecoverySpeed = 2f;

    private float currentSpeed = 0f;
    private float rotation = 0f;
    private Rigidbody2D rb;

    private float originalMaxSpeed;
    private float originalTurnSpeed;
    private float boostMeter = 0f; // Represents the fill level of the boost meter
    public float boostDecayRate = 0.5f;
    public float maxBoost = 100f;
    public float speedup = 1f;

    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalMaxSpeed = maxSpeed;
        originalTurnSpeed = turnSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float inputVertical = Input.GetAxis("Vertical"); // Forward and backward (W/S or Up/Down keys)
        float inputHorizontal = Input.GetAxis("Horizontal"); // Left and right (A/D or Left/Right keys)
        bool brake = Input.GetKey(KeyCode.Space);

        // Calculate acceleration and rotation based on input
        if (inputVertical != 0)
        {
            currentSpeed += inputVertical * acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed, maxSpeed); // Limit speed
        }
        else
        {
           if(Mathf.Abs(currentSpeed) > 0.01f){
                // Apply drag when not accelerating
                currentSpeed = Mathf.Lerp(currentSpeed, 0, drag * Time.deltaTime);
           }
           else{
                currentSpeed = 0f;
           }
        }

        if(currentSpeed > 1.0f || currentSpeed < -1.0f){
            rotation -= inputHorizontal * turnSpeed * Time.deltaTime;
        }

        if(brake){
            currentSpeed = Mathf.Lerp(currentSpeed, 0, bdrag * Time.deltaTime);
        }

        // Calculate drift effect
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 sidewaysVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        // If the player is turning, apply drifting
        if (Mathf.Abs(inputHorizontal) > 0.1f)
        {
            // Reduce sideways velocity for drifting
            rb.velocity = forwardVelocity + sidewaysVelocity * driftFactor;
        }
        else
        {
            // Gradually recover drift when not turning
            rb.velocity = forwardVelocity + sidewaysVelocity * Mathf.Lerp(1, driftFactor, Time.deltaTime * driftRecoverySpeed);
        }
        // Apply movement and rotation to Rigidbody2D
        rb.velocity = transform.up * currentSpeed; // Move car forward
        rb.rotation = rotation;

        HandleBoostAndInstability();

        if(currentSpeed > maxSpeed){
            currentSpeed = maxSpeed;
        }
    }

    public void BoostSpeed(float speedIncrease)
    {
        boostMeter = Mathf.Clamp(boostMeter + speedIncrease, 0f, maxBoost); // Increase the boost meter, clamp to max

        float boostFraction = boostMeter / maxBoost;
        maxSpeed = maxSpeed + speedup;
        turnSpeed = originalTurnSpeed + (boostFraction * maxTurnSpeedIncrease);
    }

    void HandleBoostAndInstability()
    {
        if (boostMeter > 0)
        {

            // Gradually decay the boost meter
            boostMeter -= boostDecayRate * Time.deltaTime;

            // Reduce instability over time as the boost meter depletes
            if (boostMeter <= 0)
            {
                maxSpeed = originalMaxSpeed;
                turnSpeed = originalTurnSpeed;
            }
        }
    }
}