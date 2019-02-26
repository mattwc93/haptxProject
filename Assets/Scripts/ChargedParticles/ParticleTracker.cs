using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTracker : MonoBehaviour
{
  #region Singleton
  // Singleton pattern to keep 1 single list of all particles in the scene, that is also easily accessible by those particles behavior scripts.
  // Saves us time by avoiding having to search for the list of all particles every time by tag or name.
  public static ParticleTracker instance;
  #endregion
  // List of gameobjects to store all the particles in the scene, particles will add themselves to this list in their start method.
  public List<GameObject> particles;
  void Awake()
  {
    // Singleton pattern:
    instance = this;
    // Instantiate our particle list:
    particles = new List<GameObject>();
  }

  // Public method so particles can add themselves to this list when they are created using our singleton class:
  public void AddParticle(GameObject particle)
  {
    particles.Add(particle);
  }
}
