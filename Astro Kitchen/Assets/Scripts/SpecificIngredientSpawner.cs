using UnityEngine;

public class SpecificIngredientSpawner : MonoBehaviour
{
    [Header("What to Spawn")]
    [Tooltip("The specific ingredient this button will spawn.")]
    public GameObject ingredientPrefab;

    [Header("Where to Spawn")]
    [Tooltip("The shared spawn location above the counter.")]
    public Transform spawnLocation;

    [Header("Touch Settings")]
    [Tooltip("The exact tag applied to your VR hand colliders.")]
    public string handTag = "GameController"; 

    // Prevents rapid-fire spawning when the hand lingers inside the button
    private bool canSpawn = true;
    private float spawnCooldown = 0.5f;

    public void SpawnIngredient()
    {
        if (ingredientPrefab != null && spawnLocation != null)
        {
            // 1. Spawn the ingredient and store a reference to it
            GameObject spawnedItem = Instantiate(ingredientPrefab, spawnLocation.position, spawnLocation.rotation);
            
            // 2. Grab the Rigidbody of the newly spawned item
            Rigidbody rb = spawnedItem.GetComponent<Rigidbody>();

            // 3. Enforce the zero-gravity rules immediately
            if (rb != null)
            {
                rb.useGravity = false;
                
                // Clear any phantom physics forces using Unity 6 syntax
                rb.linearVelocity = Vector3.zero; 
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    // This built-in Unity function fires the moment another collider touches this trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object touching the button has the correct hand tag
        if (other.CompareTag(handTag) && canSpawn)
        {
            SpawnIngredient();
            
            // Lock the spawner and start the cooldown
            canSpawn = false;
            Invoke(nameof(ResetSpawner), spawnCooldown);
        }
    }

    private void ResetSpawner()
    {
        canSpawn = true;
    }
}