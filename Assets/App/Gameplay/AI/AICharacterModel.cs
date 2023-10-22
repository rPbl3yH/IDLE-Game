using App.Gameplay.AI.Model;
using UnityEngine;

namespace App.Gameplay.AI
{
    public class AICharacterModel : MonoBehaviour
    {
        public MoveToPositionData MoveToPositionData;

        [SerializeField] private CharacterModel _characterModel;

        private MoveToPositionMechanics _moveToPositionMechanics;

        private void Awake()
        {
            _moveToPositionMechanics = new MoveToPositionMechanics(MoveToPositionData, _characterModel.MoveDirection, _characterModel.Root);
        }

        private void Update()
        {
            _moveToPositionMechanics.Update();
        }
    }
}