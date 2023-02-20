using System;
using UnityEngine;
using UnityEngine.UI;

public class BoardUI : MonoBehaviour
{
    private Image[] _tileImages;

    private void Awake()
    {
        _tileImages = GetComponentsInChildren<Image>();

        MinMax minMax = FindObjectOfType<MinMax>();

        int id = 0;
        foreach(var button in GetComponentsInChildren<Button>())
        {
            int buttonId = id;
            button.onClick.AddListener(() => minMax.Play(buttonId));
            id++;
        }
    }

    public void UpdateBoard(Node _gameState)
    {
        for(int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                switch (_gameState.Board[i, j % 7])
                {
                    case Node.Tile.Empty:
                        _tileImages[i * 7 + j].color = Color.white;
                        break;
                    case Node.Tile.AI:
                        _tileImages[i * 7 + j].color = Color.red;
                        break;
                    case Node.Tile.Opponent:
                        _tileImages[i * 7 + j].color = Color.yellow;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}