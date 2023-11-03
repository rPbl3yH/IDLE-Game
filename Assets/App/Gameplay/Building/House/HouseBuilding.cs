using System;
using App.GameEngine.AI.Spawner;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Building.House
{
    public class HouseBuilding : LevelStorage.Building
    {
        [SerializeField] private Transform _spawnPoint;
        
        [Inject]
        public void Construct(AICharacterSpawner aiCharacterSpawner)
        {
            aiCharacterSpawner.Spawn(_spawnPoint);
        }
    }
}