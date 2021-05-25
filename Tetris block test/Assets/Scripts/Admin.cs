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
        [SerializeField]
        CameraController _cameraSettings; //ToDo separate settings for camera
        PlayerController _player;

        RoadSpawner _roadSpawner;
        void Start()
        {

            PlayerCreator playerCreator = new PlayerCreator(_settings.PlayerConstructSettings);
            _player = playerCreator.CreatePlayer();

            GateSpawner gateSpawner = new GateSpawner(_player.GetComponent<IRotatable>(), _settings.GateSettings);
            gateSpawner.CreateGatePools();

            _roadSpawner = new RoadSpawner(_settings.RoadSettings);
            _roadSpawner.Init(gateSpawner.GatePools);

            _cameraSettings.Init(_player.GetComponent<ICameraControllable>());
        }

        public void StartGame()
        {
            _roadSpawner.StartSpawnMainRoad();
            _player.gameObject.AddComponent<PlayerSpeedControl>().Init(_settings.PlayerSpeedSettings);
        }

    }
}
