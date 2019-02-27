using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Editor extension script to add our prefab particles to the right click menu in the heirarchy window:
public class CreateMenu : MonoBehaviour
{
  private static System.Random random = new System.Random();
  private static int randomRange = 5;
  // Adds drop down option to heirarchy menu for the particle
  [MenuItem("GameObject/Particles/Proton", false, 10)]
  static void CreateProton()
  {
    // Load our prefab from our resources folder
    Object proton = Resources.Load("Proton");
    // Create a random position vector
    Vector3 position = new Vector3(random.Next(-randomRange, randomRange + 1), random.Next(-randomRange, randomRange + 1), random.Next(-randomRange, randomRange + 1));
    // Instantiate the prefab at the set position
    Instantiate(proton, position, Quaternion.identity);
  }

  [MenuItem("GameObject/Particles/Electron", false, 10)]
  static void CreateElectron()
  {
    Object electron = Resources.Load("Electron");
    Vector3 position = new Vector3(random.Next(-randomRange, randomRange + 1), random.Next(-randomRange, randomRange + 1), random.Next(-randomRange, randomRange + 1));
    Instantiate(electron, position, Quaternion.identity);
  }

  [MenuItem("GameObject/Particles/Neutron", false, 10)]
  static void CreateNeutron()
  {
    Object neutron = Resources.Load("Neutron");
    Vector3 position = new Vector3(random.Next(-randomRange, randomRange + 1), random.Next(-randomRange, randomRange + 1), random.Next(-randomRange, randomRange + 1));
    Instantiate(neutron, position, Quaternion.identity);
  }

  // Menu item to generate a bunch of protons and electrons. Set the total number of particles you want with the numParticles variable.
  [MenuItem("GameObject/Particles/Random Particle System", false, 10)]
  static void CreateRandomParticleSystem()
  {
    int numParticles = 50;

    for (int i = 0; i < numParticles; i++)
    {
      float particleToCreate = random.Next(0, 2);
      switch (particleToCreate)
      {
        case 0f:
          CreateProton();
          break;
        case 1f:
          CreateElectron();
          break;
        default:
          break;
      }

    }
  }
}

