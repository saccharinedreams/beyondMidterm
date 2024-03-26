using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scenario
{
    public string description;
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public string description;
    public int fsImpact; // Financial Stability impact
    public int mhImpact; // Mental Health impact
    public int scImpact; // Social Connections impact
    public string result;
}

/**
Manages game states, including displaying choices and applying the results of those choices.
**/

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Material[] skyboxMaterials; // add in editor
    public Scenario[] scenarios; // add in editor
    private int currentScenarioIndex = 0;
    private int[] skyboxChangePoints = {4, 7, 9}; // After 4, 3 more, and 2 more scenarios
    private int nextSkyboxChangeIndex = 0;
    private int currentSkyboxMaterialIndex = 0; // Tracks which skybox material to switch to next; same as game stage
    private bool canMakeChoice = true;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (scenarios.Length > 0)
        {
            CurrentScenario = scenarios[0];
            PresentNextScenario(); 
        }
    }

    public void PresentNextScenario()
    {
        if (currentScenarioIndex < scenarios.Length)
        {
            UIManager.Instance.PresentScenario(scenarios[currentScenarioIndex]);
            currentScenarioIndex++;
            CurrentScenario = scenarios[currentScenarioIndex];
            CheckForStateTransition();
        }
        else
        {
            Debug.Log("No more scenarios.");
        }
    }

    private void CheckForStateTransition()
    {
        if (nextSkyboxChangeIndex < skyboxChangePoints.Length && currentScenarioIndex == skyboxChangePoints[nextSkyboxChangeIndex])
        {
            TransitionToState((nextSkyboxChangeIndex++).ToString());
        }
    }

    public Scenario CurrentScenario { get; private set; } 

    public void MakeChoice(Choice choice)
    {
        if (!canMakeChoice) return; 
        canMakeChoice = false;
        Debug.Log("fs impact: " + choice.fsImpact.ToString());
        Debug.Log("mh impact: " + choice.mhImpact.ToString());
        Debug.Log("sc impact: " + choice.scImpact.ToString());
        ScoreManager.Instance.AdjustScore("FS", choice.fsImpact);
        ScoreManager.Instance.AdjustScore("MH", choice.mhImpact);
        ScoreManager.Instance.AdjustScore("SC", choice.scImpact);
        UIManager.Instance.PresentResult(choice);
        StartCoroutine(ChoiceCooldownAndNextScenario());
    }

    private IEnumerator ChoiceCooldownAndNextScenario()
    {
        yield return new WaitForSeconds(10f); 
        canMakeChoice = true;
        PresentNextScenario();
    }

    public void ChangeSkybox(int materialIndex)
    {
        if (materialIndex >= 0 && materialIndex < skyboxMaterials.Length)
        {
            RenderSettings.skybox = skyboxMaterials[materialIndex];
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogError("ChangeSkybox: Material index out of range.");
        }
    }
    
    public void TransitionToState(string newState)
    {
        Debug.Log("Transitioning to state: " + newState);
        if (int.TryParse(newState, out int newIndex) && newIndex >= 0 && newIndex < skyboxMaterials.Length)
        {
            RenderSettings.skybox = skyboxMaterials[newIndex];
            DynamicGI.UpdateEnvironment();
        }
        else
        {
            Debug.LogError($"Invalid state or skybox index: {newState}");
        }
    }
}
