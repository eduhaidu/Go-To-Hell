using UnityEngine;

public class AddRBForce : MonoBehaviour
{
    public enum ForceType { Fixed, Random }

    [Tooltip("The Rigidbody components of the gore parts.")]
    public Rigidbody[] goreRigidbodies;

    [Tooltip("The type of force to apply: Fixed or Random.")]
    public ForceType forceType = ForceType.Fixed;

    [Tooltip("The fixed force to apply to each gore part.")]
    public Vector3 fixedForce = new Vector3(0, 500, 0);

    [Tooltip("The minimum random force to apply to each gore part.")]
    public Vector3 randomForceMin = new Vector3(0, 250, 0);

    [Tooltip("The maximum random force to apply to each gore part.")]
    public Vector3 randomForceMax = new Vector3(0, 750, 0);

    [Tooltip("The mode in which the force will be applied.")]
    public ForceMode forceMode = ForceMode.Impulse;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Rigidbody goreRigidbody in goreRigidbodies)
        {
            if (goreRigidbody != null)
            {
                Vector3 forceToApply = GetForce();
                goreRigidbody.AddForce(forceToApply, forceMode);
            }
            else
            {
                Debug.LogWarning("A Rigidbody component in the goreRigidbodies array is null.");
            }
        }
    }

    private Vector3 GetForce()
    {
        if (forceType == ForceType.Fixed)
        {
            return fixedForce;
        }
        else // forceType == ForceType.Random
        {
            float x = Random.Range(randomForceMin.x, randomForceMax.x);
            float y = Random.Range(randomForceMin.y, randomForceMax.y);
            float z = Random.Range(randomForceMin.z, randomForceMax.z);
            return new Vector3(x, y, z);
        }
    }
}
