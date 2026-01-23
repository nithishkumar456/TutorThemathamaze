using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{

    #region  variables for puzzle 
    [SerializeField] private Slider cicadaSlider;
    [SerializeField] private TextMeshProUGUI cicadaText;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private List<Button> predatorButtons;
    [SerializeField] private Button calculateButton;
    #endregion

   #region Solved UI Panels 
   [SerializeField] private GameObject puzzleSolvedPanel;  
   [SerializeField] private TextMeshProUGUI keysText;
   #endregion

   #region animation 
   [SerializeField] private Animator gateAnimator;
   #endregion 

   private int keyCollected = 0; 

    private int currentCicadaValue = 0;
    private int currentPredatorCycle = 0;
    private int totalYears = 100;

    void Start()
    {
        cicadaSlider.minValue = 1;
        cicadaSlider.maxValue = 21;
        cicadaSlider.onValueChanged.AddListener(UpdateCicadaText);
        currentCicadaValue = (int)cicadaSlider.value;

        AssignPredatorValues();
        calculateButton.onClick.AddListener(CalculateMeetings);
        puzzleSolvedPanel.SetActive(false);
        keysText.text = "Keys: 0/3";
    }

    private void UpdateCicadaText(float value)
    {
        currentCicadaValue = (int)value;
        cicadaText.text = "Cicada Life Cycle: " + currentCicadaValue + " years";
    }

    private void AssignPredatorValues()
    {
        for (int i = 0; i < predatorButtons.Count; i++)
        {
            int randomValue = UnityEngine.Random.Range(1, 16); // predator cycles 1–15
            TextMeshProUGUI buttonText = predatorButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = randomValue.ToString();
            }

            int capturedValue = randomValue;
            predatorButtons[i].onClick.AddListener(() =>
            {
                currentPredatorCycle = capturedValue;
                resultText.text = "Selected predator cycle: " + capturedValue + " years";
            });
        }
    }

    public void CalculateMeetings()
    {
        currentCicadaValue = (int)cicadaSlider.value;

        if (currentPredatorCycle == 0)
        {
            resultText.text = "Please select a predator life cycle.";
            return;
        }

        int lcmValue = LCM(currentCicadaValue, currentPredatorCycle);
        int meetings = totalYears / lcmValue; // integer division floors automatically

        resultText.text = "Over " + totalYears + " years, cicadas and predators meet " + meetings + " times.";

        if(meetings == 0)
        {
            PuzzleSolved();
        }
    }

    public void PuzzleSolved()
    {
        puzzleSolvedPanel.SetActive(true);

        StartCoroutine(PuzzleSolvedSequence());     
    }

    private IEnumerator PuzzleSolvedSequence()
    {
        yield return new WaitForSeconds(2.5f);
        puzzleSolvedPanel.SetActive(false);
        gateAnimator.SetBool("GateOpen", true);
    }

    public void CollectKey()
    {
        keyCollected++; 
        keysText.text = "Keys: " + keyCollected + " / 3";
    }

    private int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    private int LCM(int a, int b)
    {
        return (a / GCD(a, b)) * b;
    }
}