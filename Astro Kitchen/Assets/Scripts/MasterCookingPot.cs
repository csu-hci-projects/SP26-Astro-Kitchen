using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MasterCookingPot : MonoBehaviour
{
    [Header("Models")]
    public GameObject defaultPotModel;
    public GameObject cookingPotModel;

    [Header("Recipe Database")]
    public List<Recipe> allRecipes;
    public GameObject trashPrefab; 
    
    [Header("Accepted Ingredients")]
    [Tooltip("Add tags like Water, Tomato, Chicken here so the pot swallows them")]
    public List<string> validIngredientTags; 

    [Header("UI Elements")]
    public TextMeshProUGUI contentsText;
    public GameObject progressBarCanvas;
    public Image progressBarFill;

    [Header("Cooking Settings")]
    public float timeToCook = 10f;
    private float currentCookTime = 0f;
    
    private List<string> currentIngredients = new List<string>();
    private StoveBurner currentBurner = null;
    private bool isCooked = false;

    void Start()
    {
        ResetPot();
    }

    void OnTriggerEnter(Collider other)
    {
        // 1. THIS PRINTS A MESSAGE TO THE CONSOLE THE EXACT MILLISECOND ANYTHING TOUCHES THE POT
        Debug.Log("THE POT WAS JUST TOUCHED BY: " + other.name + " | ITS TAG IS: " + other.tag);

        if (other.TryGetComponent(out StoveBurner burner))
        {
            currentBurner = burner;
            return;
        }

        // 2. CHECK THE INGREDIENT
        if (!isCooked && validIngredientTags.Contains(other.tag))
        {
            Debug.Log("THE POT ACCEPTED THE INGREDIENT!");
            currentIngredients.Add(other.tag);
            Destroy(other.gameObject);
            UpdateUI();
        }
        else 
        {
            // 3. IF IT REJECTS IT, TELL US EXACTLY WHY
            Debug.Log("THE POT REJECTED IT! Is it already cooked? " + isCooked + " | Is it on the approved list? " + validIngredientTags.Contains(other.tag));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out StoveBurner burner) && currentBurner == burner)
        {
            currentBurner = null;
            SetModel(defaultPotModel); 
        }
    }

    void Update()
    {
        if (currentIngredients.Count > 0 && !isCooked && currentBurner != null && currentBurner.isHot)
    {
        SetModel(cookingPotModel);
        progressBarCanvas.SetActive(true);
        
        // THIS IS THE LINE THAT HIDES THE TEXT:
        contentsText.gameObject.SetActive(false); 

        currentCookTime += Time.deltaTime;
        // ... (rest of the code)
            progressBarFill.fillAmount = currentCookTime / timeToCook;

            if (currentCookTime >= timeToCook)
            {
                isCooked = true;

                // THIS IS THE LINE THAT BRINGS IT BACK:
                contentsText.gameObject.SetActive(true); 
                
                contentsText.text = "DONE! Press B Button to Extract";
                progressBarFill.color = Color.green;
            }
        }
    }

    public void ExtractFood()
    {
        if (!isCooked) return; 

        GameObject resultToSpawn = trashPrefab;

        foreach (Recipe recipe in allRecipes)
        {
            if (AreIngredientsMatch(recipe.requiredTags, currentIngredients))
            {
                resultToSpawn = recipe.cookedPrefab;
                break;
            }
        }

        Instantiate(resultToSpawn, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        ResetPot();
    }

    void UpdateUI()
    {
        if (currentIngredients.Count == 0)
        {
            contentsText.text = "Empty";
        }
        else
        {
            contentsText.text = "Contains:\n- " + string.Join("\n- ", currentIngredients);
        }
    }

    void ResetPot()
    {
        currentIngredients.Clear();
        currentCookTime = 0f;
        isCooked = false;
        
        if(progressBarCanvas != null) progressBarCanvas.SetActive(false);
        if(progressBarFill != null) 
        {
            progressBarFill.fillAmount = 0f;
            progressBarFill.color = Color.white; 
        }
        
        SetModel(defaultPotModel);
        UpdateUI();
    }

    void SetModel(GameObject activeModel)
    {
        if(defaultPotModel != null) defaultPotModel.SetActive(false);
        if(cookingPotModel != null) cookingPotModel.SetActive(false);
        if(activeModel != null) activeModel.SetActive(true);
    }

    private bool AreIngredientsMatch(List<string> required, List<string> inside)
    {
        if (required.Count != inside.Count) return false;
        List<string> reqCopy = new List<string>(required);
        List<string> inCopy = new List<string>(inside);
        reqCopy.Sort();
        inCopy.Sort();
        for (int i = 0; i < reqCopy.Count; i++)
        {
            if (reqCopy[i] != inCopy[i]) return false;
        }
        return true;
    }
}