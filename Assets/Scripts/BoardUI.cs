using System;
using UnityEngine;
using UnityEngine.UI;

public class BoardUI : MonoBehaviour
{
    private Image[] _tileImages;

    private void Awake()
    {
        _tileImages = GetComponentsInChildren<Image>();

        Game game = FindObjectOfType<Game>();

        int id = 0;
        foreach(var button in GetComponentsInChildren<Button>())
        {
            int buttonId = id;
            button.onClick.AddListener(() => game.Play(buttonId));
            id++;
        }
    }

    public void UpdateBoard(Node _gameState)
    {
        for(int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                _tileImages[i * 7 + j].color = _gameState.Board[i, j % 7] switch
                {
                    Node.Tile.Empty => Color.white,
                    Node.Tile.AI => Color.red,
                    Node.Tile.Opponent => Color.yellow,
                    Node.Tile.Opponent1 => Color.red,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
        }
    }
}