using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [Header("PlayerVsAI")]
    [SerializeField] private TMP_Text ComplexityText;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Toggle AIPlayFirstToggle;
    
    private MinMax _minMax;
    private Game _game;

    private void Start()
    {
        _minMax = FindObjectOfType<MinMax>();
        _game = FindObjectOfType<Game>();
    }

    public void SetEasyAIComplexity()
    {
        _minMax.Depth = 2;

        ComplexityText.text = "Easy";
        PlayButton.interactable = true;
    }
    
    public void SetMediumAIComplexity()
    {
        _minMax.Depth = 4;
        
        ComplexityText.text = "Medium";
        PlayButton.interactable = true;
    }
    
    public void SetHardAIComplexity()
    {
        _minMax.Depth = 6;
        
        ComplexityText.text = "Hard";
        PlayButton.interactable = true;
    }

    public void LaunchPlayerVsAI()
    {
        if(AIPlayFirstToggle.isOn) _game.PlayAI();
    }
    
    public void LaunchPlayerVsPlayer()
    {
        _game.CurrentMode = Game.Mode.PlayerVsPlayer;
    }

    public void Quit() => Application.Quit();
}