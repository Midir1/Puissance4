using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Mode CurrentMode = Mode.PlayerVsAI;
    public MinMax MinMax, MinMax1;

    private Node _currentState;
    private BoardUI _boardUI;
    private bool _gameEnded;
    private bool _player1Turn = true;
    private bool _ai1Turn = true;

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
    }
    
    public void Play(int _id)
    {
        if (_gameEnded) return;
        if (_currentState.Board[_id / 7, _id % 7] == Node.Tile.Empty)
        {
            if (_id / 7 == 0 || (_id / 7 != 0 && _currentState.Board[(_id / 7) - 1, _id % 7] != Node.Tile.Empty))
            {
                _currentState.Board[_id / 7, _id % 7] = _player1Turn ? Node.Tile.Opponent : Node.Tile.Opponent1;
                _boardUI.UpdateBoard(_currentState);

                if (_player1Turn && _currentState.IsAligned() == Node.Tile.Opponent)
                {
                    Debug.Log("Player1 Wins !");
                    _gameEnded = true;
                    return;
                }
                
                if (!_player1Turn && _currentState.IsAligned() == Node.Tile.Opponent1)
                {
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
        _currentState.Children.Clear();
        
        if(_ai1Turn) _currentState.AITurn = true;
        else _currentState.AITurn = false;

        if (_currentState.Board[0, 3] == Node.Tile.Empty)
        {
            _currentState.Board[0, 3] = _ai1Turn ? Node.Tile.AI : Node.Tile.Opponent;
            _boardUI.UpdateBoard(_currentState);
            
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
        
        if (_ai1Turn && _currentState.IsAligned() == Node.Tile.AI)
        {
            Debug.Log("AI1 Wins !");
            _gameEnded = true;
            return;
        }
        
        if (!_ai1Turn && _currentState.IsAligned() == Node.Tile.Opponent)
        {
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