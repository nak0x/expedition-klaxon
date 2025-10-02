using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Difficulty", menuName = "Difficulties/Difficulty", order = 1)]
    public class Difficulty : ScriptableObject
    {
        public string difficultyName;
        public string slug;
        public ParticleSystem.MinMaxCurve bpmToSpeed;
        public float childSpawnFactor;
        public bool isDefault;
    }
}