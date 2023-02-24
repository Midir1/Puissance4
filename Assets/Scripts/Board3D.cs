using System;
using UnityEngine;

public class Board3D : MonoBehaviour
{
    private Renderer[] _cubeRenderers;
    private Game _game;
    private Camera _camera;

    private void Awake()
    {
        _cubeRenderers = GetComponentsInChildren<Renderer>();
        _game = FindObjectOfType<Game>();
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i) == hit.transform)
                {
                    _game.Play(i);
                    return;
                }
            }
        }
    }

    public void UpdateBoard(Node _gameState)
    {
        for(int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                _cubeRenderers[i * 7 + j].material.color = _gameState.Board[i, j % 7] switch
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