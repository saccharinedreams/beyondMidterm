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

    // Call this to present the next scenario to the player
    public void PresentNextScenario()
    {
        if (currentScenarioIndex < scenarios.Length)
        {
            Scenario currentScenario = scenarios[currentScenarioIndex];
            UIManager.Instance.PresentScenario(currentScenario);
            currentScenarioIndex++;
        }
        else
        {
            Debug.Log("No more scenarios.");
            // Handle the end of the scenarios/game here
        }
    }

    public Scenario CurrentScenario { get; private set; } 

    // Called when a choice is made within a scenario
    public void MakeChoice(Choice choice)
    {
        ScoreManager.Instance.AdjustScore("FS", choice.fsImpact);
        ScoreManager.Instance.AdjustScore("MH", choice.mhImpact);
        ScoreManager.Instance.AdjustScore("SC", choice.scImpact);
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
        int newIndex;
        if(int.TryParse(newState, out newIndex))
        {
            ChangeSkybox(newIndex);
        }
    }
}
