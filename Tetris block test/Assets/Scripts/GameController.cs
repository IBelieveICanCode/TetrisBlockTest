using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TetrisGameSpace.PlayerSpace;

namespace TetrisGameSpace
{

    public class GameController : MonoBehaviour
    {

        void Awake()
        {
            PlayerCreator playerCreator = new PlayerCreator(8, 5, 2);
            playerCreator.CreatePlayer();


        }

    }
}
