using UnityEngine;
using UnityEngine.UI;

/**
Handles all UI-related activities, such as showing choices to the player and updating score displays.
**/
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Text mentalHealthText;
    public Text financialStabilityText;
    public Text socialConnectionsText;

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
        // Update the UI to show the scenario.description
        // For each choice in scenario.choices, display buttons dynamically
        // Each button should have an onClick listener that calls GameManager.Instance.MakeChoice with the respective Choice object
    }

}
