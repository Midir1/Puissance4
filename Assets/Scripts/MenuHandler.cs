using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [Header("PlayerVsAI")]
    [SerializeField] private TMP_Text complexityText;
    [SerializeField] private Button playButton;
    [SerializeField] private Toggle aiPlayFirstToggle;
    
    private MinMax minMax;

    private void Start()
    {
        minMax = FindObjectOfType<MinMax>();
    }

    public void SetEasyAIComplexity()
    {
        minMax.Depth = 2;

        complexityText.text = "Easy";
        playButton.interactable = true;
    }
    
    public void SetMediumAIComplexity()
    {
        minMax.Depth = 4;
        
        complexityText.text = "Medium";
        playButton.interactable = true;
    }
    
    public void SetHardAIComplexity()
    {
        minMax.Depth = 6;
        
        complexityText.text = "Hard";
        playButton.interactable = true;
    }

    public void PlayGame()
    {
        minMax.StartMinMax(aiPlayFirstToggle.isOn);
    }
}