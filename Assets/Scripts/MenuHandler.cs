using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    [Header("PlayerVsAI")]
    [SerializeField] private TMP_Text ComplexityText;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Toggle AIPlayFirstToggle;
    
    [Header("AIVsAI")]
    [SerializeField] private TMP_Text ComplexityText1;
    [SerializeField] private TMP_Text ComplexityText2;
    [SerializeField] private Button PlayButton1;
    
    [Header("Views")]
    [SerializeField] private GameObject Background2D;
    [SerializeField] private GameObject Board2D;

    private Game _game;
    private MinMax _minMax, _minMax1;
    private bool _hasSetAI1Complexity, _hasSetAI2Complexity;

    private void Start()
    {
        _game = FindObjectOfType<Game>();
        _minMax = _game.MinMax;
        _minMax1 = _game.MinMax1;
    }

    public void SetEasyAIComplexity()
    {
        _minMax.Depth = 2;
        
        ComplexityText1.text = ComplexityText.text = "Easy";
        PlayButton.interactable = true;

        _hasSetAI1Complexity = true;
        if (_hasSetAI1Complexity && _hasSetAI2Complexity) PlayButton1.interactable = true;
    }
    
    public void SetMediumAIComplexity()
    {
        _minMax.Depth = 4;
        
        ComplexityText1.text = ComplexityText.text = "Medium";
        PlayButton.interactable = true;
        
        _hasSetAI1Complexity = true;
        if (_hasSetAI1Complexity && _hasSetAI2Complexity) PlayButton1.interactable = true;
    }
    
    public void SetHardAIComplexity()
    {
        _minMax.Depth = 6;
        
        ComplexityText1.text = ComplexityText.text = "Hard";
        PlayButton.interactable = true;
        
        _hasSetAI1Complexity = true;
        if (_hasSetAI1Complexity && _hasSetAI2Complexity) PlayButton1.interactable = true;
    }
    
    public void SetEasyAI2Complexity()
    {
        _minMax1.Depth = 2;

        ComplexityText2.text = "Easy";
        
        _hasSetAI2Complexity = true;
        if (_hasSetAI1Complexity && _hasSetAI2Complexity) PlayButton1.interactable = true;
    }
    
    public void SetMediumAI2Complexity()
    {
        _minMax1.Depth = 4;
        
        ComplexityText2.text = "Medium";
        
        _hasSetAI2Complexity = true;
        if (_hasSetAI1Complexity && _hasSetAI2Complexity) PlayButton1.interactable = true;
    }
    
    public void SetHardAI2Complexity()
    {
        _minMax1.Depth = 6;
        
        ComplexityText2.text = "Hard";
        
        _hasSetAI2Complexity = true;
        if (_hasSetAI1Complexity && _hasSetAI2Complexity) PlayButton1.interactable = true;
    }

    public void LaunchPlayerVsAI()
    {
        if(AIPlayFirstToggle.isOn) _game.PlayAI();
    }
    
    public void LaunchPlayerVsPlayer()
    {
        _game.CurrentMode = Game.Mode.PlayerVsPlayer;
    }

    public void LaunchAIVsAI()
    {
        _game.CurrentMode = Game.Mode.AIVsAI;
        _game.PlayAI();
    }

    public void SwitchView()
    {
        Background2D.SetActive(!Background2D.activeSelf);
        Board2D.SetActive(!Board2D.activeSelf);
    }
    
    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit() => Application.Quit();
}