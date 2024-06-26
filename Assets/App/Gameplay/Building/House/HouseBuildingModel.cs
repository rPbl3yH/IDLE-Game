﻿using App.GameEngine.AI.Spawner;
using UnityEngine;
using VContainer;

namespace App.Gameplay.Building.House
{
    public class HouseBuildingModel : BuildingModel
    {
        [SerializeField] private Transform _spawnPoint;
        
        [Inject]
        public void Construct(AICharacterSpawner aiCharacterSpawner)
        {
            aiCharacterSpawner.Spawn(_spawnPoint);
        }
    }
}