using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [Header("Spawning Configuration")]
    [Tooltip("Drag your Food Prefab here from the Project window")]
    public GameObject foodPrefab;
    
    [Tooltip("Drag the empty SpawnLocation object here from the Hierarchy")]
    public Transform spawnLocation;

    // This public method can be called by UI Buttons, VR Interactors, or triggers
    public void SpawnItem()
    {
        if (foodPrefab != null && spawnLocation != null)
        {
            // Creates a copy of the prefab at the specific position and rotation
            Instantiate(foodPrefab, spawnLocation.position, spawnLocation.rotation);
        }
        else
        {
            Debug.LogWarning("FoodSpawner: Missing the Food Prefab or Spawn Location!");
        }
    }

    // Quick testing method: Works if you click the panel with a standard mouse
    private void OnMouseDown()
    {
        SpawnItem();
    }
}