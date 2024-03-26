using UnityEngine;
using UnityEngine.UI;
using TMPro;
/**
Handles all UI-related activities, such as showing choices to the player and updating score displays.
**/
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public TextMeshProUGUI mentalHealthText;
    public TextMeshProUGUI financialStabilityText;
    public TextMeshProUGUI socialConnectionsText;
    public TextMeshProUGUI scenarioDescriptionText;
    public TextMeshProUGUI choiceAText;
    public TextMeshProUGUI choiceBText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    public void UpdateUI(int mh, int fs, int sc)
    {
        mentalHealthText.text = "MH: " + mh;
        financialStabilityText.text = "FS: " + fs;
        socialConnectionsText.text = "SC: " + sc;
    }

    public void PresentScenario(Scenario scenario)
    {
        if (scenarioDescriptionText != null && scenario != null)
        {
            scenarioDescriptionText.text = scenario.description;
            choiceAText.text = scenario.choices[0].description;
            choiceBText.text = scenario.choices[1].description;
        }
        else
        {
            Debug.LogError("Scenario description TextMeshProUGUI component or scenario is null.");
        }
    }

    public void PresentResult(Choice choice)
    {
        scenarioDescriptionText.text = choice.result;
        choiceAText.text = "Keep going.";
        choiceBText.text = "Keep going.";
    }
}
