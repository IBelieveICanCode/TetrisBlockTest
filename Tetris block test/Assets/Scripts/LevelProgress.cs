using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TetrisRunnerSpace.UISpace;

namespace TetrisRunnerSpace
{
    public class LevelProgress : Singleton<LevelProgress>
    {
        private int score = 0;

        [SerializeField]
        UIController _hud;

        [SerializeField]
        UnityEvent _startGame;
        //TODO add SaveController
        public int Score
        {
            get => score;
            set
            {
                score = value;
                _hud.UpdateUI();
            }
        }

        public void StartGame()
        {
            _startGame.Invoke();
        }

        public void FinishGame()
        {
            _hud.ShowEndOfLevel();
        }

        public static string GetScoreReadableValue()
        {
            return GetReadableValue(Instance.Score);
        }

        public static string GetReadableValue(int value)
        {
            List<int> digits = new List<int>();

            while (value > 0)
            {
                digits.Add(value % 10);
                value /= 10;
            }

            string str = "";
            for (int i = 0; i < digits.Count; i++)
            {
                str = digits[i].ToString() + str;
                if ((i + 1) % 3 == 0 && i != digits.Count - 1)
                    str = "," + str; // Insert comma after every 3 digits
            }
            return str;
        }
    }
}

