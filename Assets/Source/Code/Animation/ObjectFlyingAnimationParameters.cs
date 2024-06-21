using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Code.Animation
{
    [Serializable]
    public struct ObjectFlyingAnimationParameters
    {
        public const float MinRotation = 0f;
        public const float MaxRotation = 360f;

        private const string Jumping = nameof(Jumping);
        private const string Rotating = nameof(Rotating);

        [field: SerializeField, FoldoutGroup(Jumping)]
        public Ease JumpEase { get; private set; }

        [field: SerializeField, FoldoutGroup(Jumping), MinValue(0)]
        public float JumpDuration { get; private set; }

        [field: SerializeField, FoldoutGroup(Jumping), HideIf(nameof(IsNeedRandomizeJumpPower))]
        public float JumpPower { get; private set; }

        [field: SerializeField, FoldoutGroup(Jumping)]
        public bool IsNeedRandomizeJumpPower { get; private set; }

        [field: SerializeField, FoldoutGroup(Jumping), MinMaxSlider(0, 10, true),
                ShowIf(nameof(IsNeedRandomizeJumpPower))]
        public Vector2 JumpPowerRange { get; private set; }

        [field: SerializeField, FoldoutGroup(Jumping), HideIf(nameof(IsNeedRandomizeJumpCount))]
        public int JumpCount { get; private set; }

        [field: SerializeField, FoldoutGroup(Jumping)]
        public bool IsNeedRandomizeJumpCount { get; private set; }


        [field: SerializeField, FoldoutGroup(Jumping), MinMaxSlider(0, 10, true),
                ShowIf(nameof(IsNeedRandomizeJumpCount))]
        public Vector2 JumpCountRange { get; private set; }


        [field: SerializeField, FoldoutGroup(Jumping)]
        public bool IsNeedSetRandomRotationBySpawn { get; private set; }

        [field: SerializeField, FoldoutGroup(Rotating), ShowIf(nameof(IsNeedSetRandomRotationBySpawn))]
        public Vector3AsBool IsNeedRandomizeRotationOnAxis { get; private set; }


        [field: SerializeField, FoldoutGroup(Rotating)]
        public bool IsNeedRotateInFlight { get; private set; }

        [field: SerializeField, FoldoutGroup(Rotating), MinValue(0), ShowIf(nameof(IsNeedRotateInFlight))]
        public float RotateDuration { get; private set; }

        [field: SerializeField, FoldoutGroup(Rotating), ShowIf(nameof(IsNeedRotateInFlight))]
        public Vector3AsBool IsNeedRotateOnAxis { get; private set; }


        [Serializable]
        public struct Vector3AsBool
        {
            public bool X;
            public bool Y;
            public bool Z;
        }
    }
}