using UnityEngine;

/**
Handles the player's Mental Health (MH), Financial Stability (FS), and Social Connections (SC) scores.
**/

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int MentalHealth = 80;
    public int FinancialStability = 60;
    public int SocialConnections = 70;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void AdjustScore(string scoreType, int amount)
    {
        //Debug.Log($"Applying {amount} impact to {scoreType}");
        switch (scoreType)
        {
            case "MH":
                MentalHealth += amount;
                //Debug.Log($"After adjustment: {scoreType} score is {MentalHealth}");
                break;
            case "FS":
                FinancialStability += amount;
                //Debug.Log($"After adjustment: {scoreType} score is {FinancialStability}");
                break;
            case "SC":
                //Debug.Log($"After adjustment: {scoreType} score is {SocialConnections}");
                SocialConnections += amount;
                break;
        }
        UIManager.Instance.UpdateUI(MentalHealth, FinancialStability, SocialConnections);
    }
}
