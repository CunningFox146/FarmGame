using Farm.Util;
using System;
using UnityEngine;

namespace Farm.Interactable.GrowSystem
{
    public class Growable : MonoBehaviour
    {
        public event Action<int> StageChanged;

        protected SourceModifierList _growthSpeedMult = new();
        protected int _currentStage = 0;

        [field: SerializeField, Tooltip("First stage is 0")]
        public int StagesCount { get; protected set; }
        [field: SerializeField] public float StageGrowthTime { get; protected set; }
        [field: SerializeField] public bool IsGrowing { get; protected set; }

        public int CurrentStage
        {
            get => _currentStage;
            protected set
            {
                if (_currentStage != value)
                {
                    _currentStage = value;
                    StageChanged?.Invoke(_currentStage);
                }
            }
        }

        public float Progress { get; protected set; }

        public float GrowthSpeedMultiplier => _growthSpeedMult.Modifier;
        public bool IsFull => StagesCount == CurrentStage;

        protected virtual void Update()
        {
            if (IsGrowing && CurrentStage < StagesCount)
            {
                UpdateStage();
            }
        }

        private void UpdateStage()
        {
            Progress += Time.deltaTime * GrowthSpeedMultiplier;
            if (Progress >= StageGrowthTime)
            {
                CurrentStage = Mathf.Clamp(CurrentStage + 1, 0, StagesCount);
                Progress = 0f;
            }
        }
    }
}
