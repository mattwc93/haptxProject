using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
  #region Global Variables
  // Reference for the target, drag and drop the target onto this field on the projectile script component:
  public Transform target;
  private Rigidbody rb;
  private float range;
  private float firingAngle;
  private float initialVelocity;
  private float zVelocity;
  private float yVelocity;
  // Set our gravitional constant to an easier to use/read variable:
  private float g = -Physics.gravity.y;
  private float timeToImpact = 2f;
  #endregion
  #region Physics equation reference:
  // t = 2 * v * sin(θ) / g
  // R = v^2 * sin(2θ) / g
  // v0 = sqrt(Rg/sin(2θ))
  // vz = v0 * cos(θ)
  // vy = v0 * sin(θ)
  // θ = arcsin(t^2 * g^2 / 2Rg)
  #endregion
  void Start()
  {
    rb = GetComponent<Rigidbody>();                                           // Grab our rigidboy component so we can set its velocity later.
    Aim();
    Fire();
  }
  private void Aim()
  {
    if (target == null)                                                                            // Make sure a target exists, otherwise return.
      return;
    transform.LookAt(target.position);                                                             // Face our target so we can just do 2d physics calculations.
    range = Vector3.Distance(new Vector3(target.position.x, 0, target.position.z), new Vector3(transform.position.x, 0, transform.position.z));    // Get the distance to our target.
    firingAngle = Mathf.Asin((Mathf.Pow(timeToImpact, 2) * Mathf.Pow(g, 2)) / (2 * range * g));     // Get firing angle to impact at set time(in our case 2 seconds)(in radians).
    // initialVelocity = Mathf.Sqrt((range * g) / Mathf.Sin(2 * firingAngle));                         // Calculate our initial velocity based on the starting angle we set.
    initialVelocity = range / (timeToImpact * Mathf.Cos(firingAngle));                         // Calculate our initial velocity based on the starting angle we set.
    zVelocity = range / timeToImpact;                                           // Calculate the z and y components of our velocity vector.
    yVelocity = initialVelocity * Mathf.Sin(firingAngle);
    Debug.Log("Target Position: " + target.position + ".\nProjectile Position: " + transform.position + ".");
    Debug.Log("Range to target = " + range + " meters.");
    Debug.Log("Firing Angle = " + firingAngle + " radians.");
    Debug.Log("Initial velocity = " + initialVelocity + " m/s.");
    Debug.Log("Initial horizontal velocity = " + zVelocity + " m/s.");
    Debug.Log("Initial vertical velocity = " + yVelocity + " m/s.");

  }
  private void Fire()
  {
    Vector3 velocityVector = new Vector3(0f, yVelocity, zVelocity);             // Create our velocity vector based on initial z and y components of initial velocity.
    velocityVector = transform.TransformDirection(velocityVector);              // Translate vector to world space, needed to compensate for our inital rotation to face target.
    rb.velocity = velocityVector;                                               // Apply our initial velocity!
  }

}
