using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTimer : MonoBehaviour
{
  // Timer script to check and print out time from firing to impact:
  private float timer;
  private bool timerActive = true;    // flag to stop timer once impact happens
  void Start()
  {
    timer = 0f;               // Initialize timer.
    Debug.Log("Timer Start!");
  }
  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.name == "Projectile")            // Make sure the object we collided with was a projectile
    {
      Debug.Log("Time to impact: " + timer);                  // Print out timer and set timer flag to false so it stops counting.
      timerActive = false;
    }
  }

  void Update()
  {
    if (timerActive)
      timer += Time.deltaTime;      // incremement timer every frame with time since last frame
  }
}
