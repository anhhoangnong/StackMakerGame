using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace StackMaker
{
    public enum GameState
    {
        Menu,
        Play,
        Finish
    }
    public class GameManager : Singleton<GameManager>
    {
        private GameState state;
        public static GameManager instance;
        void Awake()
        {
            ChangeState(GameState.Menu);

        }

        public void ChangeState(GameState gameState)
        {
            state = gameState;
        }    

        public bool isState (GameState gameState)
        {
            return state == gameState;
        }
    }
}
