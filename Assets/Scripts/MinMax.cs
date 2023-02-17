using System;
using System.Linq;
using UnityEngine;

public class MinMax : MonoBehaviour
{
    [SerializeField] private int CurrentValue;
    [SerializeField] private int Depth;
    
    private Node _currentState;
    private BoardUI _boardUI;

    private void Start()
    {
        _boardUI = FindObjectOfType<BoardUI>();

        _currentState = new Node();
        _boardUI.UpdateBoard(_currentState);

        //PlayAI();
    }
    
    private void BuildTree(Node _node, int _depth)
    {
        if(_depth == 0) return;
        
        if (_node.IsAligned() == Node.Tile.Empty)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (_node.Board[i, j] == Node.Tile.Empty)
                    {
                        if (i == 0 || _node.Board[i - 1, j] != Node.Tile.Empty)
                        {
                            Node newNode = new Node();
                            Array.Copy(_node.Board, newNode.Board, 42);
                            newNode.Board[i, j] = _node.AITurn ? Node.Tile.AI : Node.Tile.Opponent;
                            newNode.AITurn = !_node.AITurn;

                            BuildTree(newNode, _depth - 1);
                            _node.Children.Add(newNode);
                        }
                    }
                }
            }
        }
    }

    private int ComputeMinMax(Node _node, int _depth)
    {
        if(_depth == 0 || _node.Children.Count == 0) return _node.Evaluate();

        if(_node.AITurn) // Max Node
        {
            int max = int.MinValue;
            foreach(Node child in _node.Children)
            {
                int value = ComputeMinMax(child, _depth - 1);
                max = Math.Max(max, value);
            }
            _node.Value = max;
            return max;
        }
        
        // Min Node
        int min = int.MaxValue;
        foreach (Node child in _node.Children)
        {
            int value = ComputeMinMax(child, _depth - 1);
            min = Math.Min(min, value);
        }
        _node.Value = min;
        return min;
    }

    public void Play(int _id)
    {
        if (_currentState.Board[_id / 7, _id % 7] == Node.Tile.Empty)
        {
            _currentState.Board[_id / 7, _id % 7] = Node.Tile.Opponent;
            _boardUI.UpdateBoard(_currentState);

            PlayAI();
        }
    }

    private void PlayAI()
    {
        _currentState.Children.Clear();
        _currentState.AITurn = true;
        BuildTree(_currentState, Depth);
        CurrentValue = ComputeMinMax(_currentState, Depth);
        _currentState = _currentState.Children.First(_n => _n.Value == CurrentValue);
        _boardUI.UpdateBoard(_currentState);
    }
}