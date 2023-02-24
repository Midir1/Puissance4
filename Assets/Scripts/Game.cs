using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Mode CurrentMode = Mode.PlayerVsAI;
    
    private MinMax _minMax;
    private BoardUI _boardUI;
    private bool _gameEnded;
    private bool _player1Turn = true;

    public enum Mode
    {
        PlayerVsAI,
        PlayerVsPlayer,
        AIVsAI
    }
    
    private void Start()
    {
        _minMax = FindObjectOfType<MinMax>();
        _boardUI = FindObjectOfType<BoardUI>();
    }
    
    public void Play(int _id)
    {
        if (_gameEnded) return;
        if (_minMax.CurrentState.Board[_id / 7, _id % 7] == Node.Tile.Empty)
        {
            if (_id / 7 == 0 || (_id / 7 != 0 && _minMax.CurrentState.Board[(_id / 7) - 1, _id % 7] != Node.Tile.Empty))
            {
                _minMax.CurrentState.Board[_id / 7, _id % 7] = _player1Turn ? Node.Tile.Opponent : Node.Tile.Opponent1;
                _boardUI.UpdateBoard(_minMax.CurrentState);

                if (_player1Turn && _minMax.CurrentState.IsAligned() == Node.Tile.Opponent)
                {
                    Debug.Log("Player1 Wins !");
                    _gameEnded = true;
                    return;
                }
                
                if (!_player1Turn && _minMax.CurrentState.IsAligned() == Node.Tile.Opponent1)
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
        _minMax.CurrentState.Children.Clear();
        _minMax.CurrentState.AITurn = true;

        if (_minMax.CurrentState.Board[0, 3] == Node.Tile.Empty)
        {
            _minMax.CurrentState.Board[0, 3] = Node.Tile.AI;
            _boardUI.UpdateBoard(_minMax.CurrentState);
            return;
        }
        
        MinMax.BuildTree(_minMax.CurrentState, _minMax.Depth);
        
        _minMax.CurrentValue = _minMax.CurrentAlgorithm == MinMax.Algorithm.MinMax 
            ? MinMax.ComputeMinMax(_minMax.CurrentState, _minMax.Depth)
            : MinMax.ComputeAlphaBeta(_minMax.CurrentState, _minMax.Depth);
        
        _minMax.CurrentState = _minMax.CurrentState.Children.First(_n => _n.Value == _minMax.CurrentValue);
        _boardUI.UpdateBoard(_minMax.CurrentState);
        
        if (_minMax.CurrentState.IsAligned() == Node.Tile.AI)
        {
            Debug.Log("AI Wins !");
            _gameEnded = true;
        }
    }
}