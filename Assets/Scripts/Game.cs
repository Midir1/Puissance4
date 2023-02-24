using System.Linq;
using TMPro;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Mode CurrentMode = Mode.PlayerVsAI;
    public MinMax MinMax, MinMax1;

    [SerializeField] private GameObject EndPanel;
    [SerializeField] private TMP_Text EndText;

    private Node _currentState;
    private BoardUI _boardUI;
    private Board3D _board3D;
    private bool _gameEnded;
    private bool _player1Turn = true;
    private bool _ai1Turn = true;
    private int _nbMove = 42;

    public enum Mode
    {
        PlayerVsAI,
        PlayerVsPlayer,
        AIVsAI
    }
    
    private void Start()
    {
        _currentState = new Node();
        MinMax = new MinMax();
        MinMax1 = new MinMax();

        _boardUI = FindObjectOfType<BoardUI>();
        _board3D = FindObjectOfType<Board3D>();
    }
    
    public void Play(int _id)
    {
        if (_gameEnded || CurrentMode == Mode.AIVsAI) return;
        _nbMove--;
        if (_currentState.Board[_id / 7, _id % 7] == Node.Tile.Empty)
        {
            if (_id / 7 == 0 || (_id / 7 != 0 && _currentState.Board[(_id / 7) - 1, _id % 7] != Node.Tile.Empty))
            {
                _currentState.Board[_id / 7, _id % 7] = _player1Turn ? Node.Tile.Opponent : Node.Tile.Opponent1;
                _boardUI.UpdateBoard(_currentState);
                _board3D.UpdateBoard(_currentState);

                if (_player1Turn && _currentState.IsAligned() == Node.Tile.Opponent)
                {
                    EndPanel.SetActive(true);
                    EndText.text = "Player1 Wins !";
                    Debug.Log("Player1 Wins !");
                    _gameEnded = true;
                    return;
                }
                
                if (!_player1Turn && _currentState.IsAligned() == Node.Tile.Opponent1)
                {
                    EndPanel.SetActive(true);
                    EndText.text = "Player2 Wins !";
                    Debug.Log("Player2 Wins !");
                    _gameEnded = true;
                    return;
                }

                if (CurrentMode == Mode.PlayerVsPlayer) _player1Turn = !_player1Turn;
                else if(CurrentMode == Mode.PlayerVsAI) PlayAI();
            }
        }
    }
    
    public void PlayAI()
    {
        if (_nbMove == 0)
        {
            EndPanel.SetActive(true);
            EndText.text = "Draw !";
            Debug.Log("Draw !");
            return;
        }

        _nbMove--;
        
        _currentState.Children.Clear();
        _currentState.AITurn = _ai1Turn;

        if (_currentState.Board[0, 3] == Node.Tile.Empty)
        {
            _currentState.Board[0, 3] = _ai1Turn ? Node.Tile.AI : Node.Tile.Opponent;
            _boardUI.UpdateBoard(_currentState);
            _board3D.UpdateBoard(_currentState);
            
            if (CurrentMode == Mode.AIVsAI)
            {
                _ai1Turn = !_ai1Turn;
                Invoke(nameof(PlayAI), 1f);
            }
            
            return;
        }
        
        if (_ai1Turn)
        {
            MinMax.BuildTree(_currentState, MinMax.Depth);
            
            MinMax.CurrentValue = MinMax.CurrentAlgorithm == MinMax.Algorithm.MinMax 
                ? MinMax.ComputeMinMax(_currentState, MinMax.Depth)
                : MinMax.ComputeAlphaBeta(_currentState, MinMax.Depth);
        
            _currentState = _currentState.Children.First(_n => _n.Value == MinMax.CurrentValue);
        }
        else
        {
            MinMax.BuildTree(_currentState, MinMax1.Depth);
            
            MinMax1.CurrentValue = MinMax.CurrentAlgorithm == MinMax.Algorithm.MinMax 
                ? MinMax.ComputeMinMax(_currentState, MinMax1.Depth)
                : MinMax.ComputeAlphaBeta(_currentState, MinMax1.Depth);
        
            _currentState = _currentState.Children.First(_n => _n.Value == MinMax1.CurrentValue);
        }
        
        _boardUI.UpdateBoard(_currentState);
        _board3D.UpdateBoard(_currentState);
        
        if (_ai1Turn && _currentState.IsAligned() == Node.Tile.AI)
        {
            EndPanel.SetActive(true);
            EndText.text = "AI1 Wins !";
            Debug.Log("AI1 Wins !");
            _gameEnded = true;
            return;
        }
        
        if (!_ai1Turn && _currentState.IsAligned() == Node.Tile.Opponent)
        {
            EndPanel.SetActive(true);
            EndText.text = "AI2 Wins !";
            Debug.Log("AI2 Wins !");
            _gameEnded = true;
            return;
        }
        
        if (CurrentMode == Mode.AIVsAI)
        {
            _ai1Turn = !_ai1Turn;
            Invoke(nameof(PlayAI), 1f);
        }
    }
}