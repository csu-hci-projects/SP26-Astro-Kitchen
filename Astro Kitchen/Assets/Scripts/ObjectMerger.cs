using UnityEngine;

public class ObjectMerger : MonoBehaviour
{
    public string targetTag;      // The tag of the OTHER object (e.g., "Ingredient")
    public GameObject resultPrefab; // The new object to create

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Only the object with the higher instance ID handles the merger
            // This prevents double-spawning the result
            if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
            {
                Vector3 spawnPosition = (transform.position + collision.transform.position) / 2;
                Instantiate(resultPrefab, spawnPosition, Quaternion.identity);

                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}