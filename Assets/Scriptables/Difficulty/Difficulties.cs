using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Difficulties", menuName = "Difficulties/Difficulty List", order = 1)]
    public class Difficulties : ScriptableObject
    {
        public List<Difficulty> difficulties;

        public static Difficulty FindDifficultyBySlug(List<Difficulty> difficulties, string slug)
        {
            foreach (var difficulty in difficulties)
            {
                if (difficulty.slug == slug)
                    return difficulty;
            }
            return null;
        }

        public static Difficulty GetDefaultDifficulty(List<Difficulty> difficulties)
        {
            foreach (var difficulty in difficulties)
            {
                if (difficulty.isDefault)
                    return difficulty;
            }
            return null;
        }
    }
}