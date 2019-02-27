using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour
{
  #region Global Vars
  // Range for our charges, can be altered here to allow larger magnitude charges
  [Range(-1, 1)]
  public float charge;
  private Rigidbody rb;
  Color chargeColor;
  Color finalColor;
  // Arbitrary representation of Coulomb's Constant.
  private float electroStaticConstant = 5f;
  #endregion
  void Start()
  {
    // Add this particle to the Particle Trackers list:
    ParticleTracker.instance.AddParticle(gameObject);
  }
  // AddElectroStaticForce method does the calculations for the direction and magnitude of force to apply to another particle, then applies that force.
  // Takes a full particle Game Object as the argument so we can access multiple parts of the particle from this one method:
  void AddElectroStaticForce(GameObject particle)
  {
    if (particle != null)
    {
      // Grab the components we need to access on the particle we are influencing:
      Rigidbody otherRB = particle.GetComponent<Rigidbody>();
      float otherCharge = particle.GetComponent<ParticleBehavior>().charge;
      // Calculate the distance between the particles
      float distance = Vector3.Distance(transform.position, particle.transform.position);
      // Get a vector pointing from this particle to the other particle, divide by distance to normalize the vector:
      Vector3 direction = (particle.transform.position - transform.position) / distance;
      // Use Coulomb's Law to calculate the magnitude of the force placed on the particle, because this is a "scaled up" simlation and
      // we are using -1 to 1 as our range for charge to keep things simple, the electrostatic constant is just a float we can adjust as we like(replaces Coulomb's constant).
      float electroStaticForce = (electroStaticConstant * charge * otherCharge) / (distance * distance);
      // Add the force to the rigidbody of the other particle, we only add it to the other particle because the other particle will add the force back on it's own.
      // We normalize the direction vector because we want to use the force magnitude we calculated here to determine the magnitude of the force vector applied. If both
      // charges are positive or negative, the force will be positive and repulse. If they are opposite then the result will be a negative force and attract:
      otherRB.AddForce(direction * electroStaticForce);
    }
  }
  void LateUpdate()
  {
    // Code to check our charge and assign a color based on it. Negative charge will lerp between white and blue, and positive between white
    // and red. Magnitude of charge will determine how blue or red the particle is.
    float lerpValue = 0f;
    if (charge < 0)
    {
      // since lerping uses value between 0 and 1, if our charge is negative we need to convert it to positive
      chargeColor = Color.blue;
      lerpValue = -charge;
    }
    else if (charge > 0)
    {
      chargeColor = Color.red;
      lerpValue = charge;
    }
    // get our lerped color based on charge:
    finalColor = Color.Lerp(Color.white, chargeColor, lerpValue);
    // get our renderer and set the color to our calculated color
    GetComponent<Renderer>().material.color = finalColor;

    // Set the mass of a particle if it has a positive charge
  }
  void Update()
  {
    // Loop through all other particles in the Particle Tracker, and if they aren't the particle this script is attached to we call our method to calculate and add our force:
    foreach (GameObject particle in ParticleTracker.instance.particles)
    {
      if (!GameObject.ReferenceEquals(particle, gameObject))
      {
        AddElectroStaticForce(particle);
      }
    }
  }


}
