using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace TetrisRunnerSpace
{
    namespace UISpace
    {
        public class UIController : MonoBehaviour
        {
            [SerializeField]
            TMP_Text _scores;
            [SerializeField]
            TMP_Text _finalScores;
            [SerializeField]
            TMP_Text _countDownText;
            [SerializeField]
            GameObject _finalPanel;
            public void UpdateUI()
            {
                _scores.text = LevelProgress.GetScoreReadableValue();
                _finalScores.text = _scores.text; //TODO Save final scorees differently
            }
            private void Start()
            {
                StartCountDown();
                _finalPanel.SetActive(false);
            }

            public void StartCountDown()
            {
                StartCoroutine(CountDown());
            }

            public void ShowEndOfLevel()
            {
                //TODO Score count
                _finalPanel.SetActive(true);
            }

            private IEnumerator CountDown()
            {
                float iterationTime = 0.75f;
                string def = "Game start...";
                int seconds = 3;
                for (int i = seconds; i >= 0; i--)
                {
                    if (i == 0)
                        _countDownText.text = "GO!";
                    else
                        _countDownText.text = def + i;
                    yield return new WaitForSeconds(iterationTime);
                }
                LevelProgress.Instance.StartGame();
                _countDownText.transform.parent.gameObject.SetActive(false);

            }
        }
    }
}
