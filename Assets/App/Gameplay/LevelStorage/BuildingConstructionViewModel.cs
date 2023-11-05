﻿using App.Gameplay.Resource.View;
using Modules.Atomic.Values;
using UnityEngine;

namespace App.Gameplay.LevelStorage
{
    public class BuildingConstructionViewModel : MonoBehaviour
    {
        [SerializeField] 
        private BuildingConstructionModel _model;

        [SerializeField] 
        private GameObject _view;

        public AtomicVariable<float> ActivateShowTime = new(0.5f);

        private ActivateModelViewMechanics _activateModelViewMechanics;

        private void Awake()
        {
            _activateModelViewMechanics = new ActivateModelViewMechanics(_view, _model.IsEnable, ActivateShowTime);
        }

        private void OnEnable()
        {
            _activateModelViewMechanics.OnEnable();
        }

        private void OnDisable()
        {
            _activateModelViewMechanics.OnDisable();
        }
    }
}