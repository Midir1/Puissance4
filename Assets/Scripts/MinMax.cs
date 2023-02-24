using System;
using JetBrains.Annotations;
using UnityEngine;

public class MinMax : MonoBehaviour
{
    public Algorithm CurrentAlgorithm;
    public int CurrentValue;
    public int Depth;
    public Node CurrentState;
    
    public enum Algorithm
    {
        MinMax,
        [UsedImplicitly] AlphaBeta
    }
    
    private void Start() => CurrentState = new Node();

    public static void BuildTree(Node _node, int _depth)
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

    public static int ComputeMinMax(Node _node, int _depth)
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

    public static int ComputeAlphaBeta(Node _node, int _depth, int _alpha = int.MinValue, int _beta = int.MaxValue)
    {
        if (_depth == 0 || _node.Children.Count == 0) return _node.Evaluate();
    
        int value;
        
        if (!_node.AITurn)
        {
            value = int.MaxValue;
            foreach (Node child in _node.Children)
            {
                value = Math.Min(value, ComputeAlphaBeta(child,_depth - 1, _alpha, _beta));
                _beta = Math.Min(_beta, value);
    
                if (_alpha > value) break;
            }
        }
        else
        {
            value = int.MinValue;
            foreach (Node child in _node.Children)
            {
                value = Math.Max(value, ComputeAlphaBeta(child, _depth - 1, _alpha, _beta));
                _alpha = Math.Max(_alpha, value);
    
                if (value > _beta) break;
            }
        }

        _node.Value = value;
        return value;
    }
}