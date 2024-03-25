using UnityEngine;

public class ChoiceBoxDetector : MonoBehaviour
{
    public bool isLeftChoice; // Set this in the inspector based on which box this script is attached to

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerController")) 
        {
            Debug.Log("Controller collision");
            if (GameManager.Instance == null) Debug.LogError("GameManager.Instance is null.");
            else if (GameManager.Instance.CurrentScenario == null) Debug.LogError("GameManager.Instance.CurrentScenario is null.");
            else {
                // Assuming the first choice is always the left choice and the second choice is the scenarios
                int choiceIndex = isLeftChoice ? 0 : 1;
                if (GameManager.Instance.CurrentScenario.choices.Length > choiceIndex)
                {
                    Choice choiceMade = GameManager.Instance.CurrentScenario.choices[choiceIndex];
                    GameManager.Instance.MakeChoice(choiceMade);
                }
            }
        }
    }
}
