using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StackMaker
{
    public class LevelManager : Singleton<LevelManager>
    {
        public List<Level> levels = new List<Level>();
        public Player player;
        Level currentLv;

        int levelId = 1;
        private void Start()
        {

            UIManager.Instance.OpenMenuUI();
            RestartLevel();
        }

        public void RestartLevel()
        {
            LoadLevel(levelId);
            OnInit();
        }

        public void LoadLevel(int id)
        {
            if(currentLv != null)
            {
                Destroy(currentLv.gameObject);
            }
            currentLv = Instantiate(levels[id-1]);
        }    

        public void OnInit()
        {
            player.transform.position = currentLv.TfStartPoint.position;
            player.OnInit();
        }    

        public void OnStart()
        {
            GameManager.Instance.ChangeState(GameState.Play);
        }

        public void OnFinish()
        {
            UIManager.Instance.OpenFinishUI();
            GameManager.Instance.ChangeState(GameState.Finish);
        }    

        public void NextLevel()
        {
            levelId++;
            if(levelId > levels.Count)
            {
                levelId = 1;
            }
            RestartLevel();
        }
    }

}
