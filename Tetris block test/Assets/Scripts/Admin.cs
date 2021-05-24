using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TetrisRunnerSpace.PlayerSpace;
using TetrisRunnerSpace.RoadSpace;
using TetrisRunnerSpace.GateSpace;

namespace TetrisRunnerSpace
{

    public class Admin : MonoBehaviour
    {
        [SerializeField]
        GameSettings _settings;

        IEnumerator Start()
        {
            PlayerCreator playerCreator = new PlayerCreator(_settings.PlayerSettings);
            PlayerController player = playerCreator.CreatePlayer();

            GateSpawner gateSpawner = new GateSpawner(player.GetComponent<IRotatable>(), _settings.GateSettings, _settings.PlayerSettings.Gap);
            gateSpawner.CreateGatePools();

            RoadSpawner roadSpawner = new RoadSpawner(_settings.RoadSettings);
            roadSpawner.CreateStartRoad(gateSpawner.GatePools);

            yield return new WaitForSeconds(5);
            roadSpawner.StartGame();
            player.gameObject.AddComponent<PlayerSpeedControl>();




        }

    }
}
