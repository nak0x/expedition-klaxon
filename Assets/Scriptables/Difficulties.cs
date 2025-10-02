using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "Difficulties", menuName = "Difficulties/Difficulty List", order = 1)]
    public class Difficulties : ScriptableObject
    {
        public List<Difficulty> difficulties;
    }
}