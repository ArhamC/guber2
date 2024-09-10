using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float speedIncrease = 10f;  // Amount to increase max speed

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            idrive car = collision.GetComponent<idrive>();
            if (car != null)
            {
                car.BoostSpeed(speedIncrease);
                Destroy(gameObject); // Optionally destroy the boost item after it's picked up
            }
        }
    }
}
