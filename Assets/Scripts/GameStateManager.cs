using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState { Paused, Death, Playing };

    public GameState currentGameState;

    private void Start()
    {
        ChangeGameState(GameState.Playing);
    }

    public void ChangeGameState(GameState _state)
    {
        currentGameState = _state;
    }
}
