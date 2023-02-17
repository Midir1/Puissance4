using System;
using UnityEngine;
using UnityEngine.UI;

public class BoardUI : MonoBehaviour
{
    private Text[] _tileTexts;

    private void Awake()
    {
        _tileTexts = GetComponentsInChildren<Text>();

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
                        _tileTexts[i * 7 + j].text = "";
                        break;
                    case Node.Tile.AI:
                        _tileTexts[i * 7 + j].text = "O";
                        break;
                    case Node.Tile.Opponent:
                        _tileTexts[i * 7 + j].text = "X";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}