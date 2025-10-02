using System.Collections.Generic;
using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "ChunkTypeList", menuName = "Chunks/Chunk Type List", order = 1)]
    public class ChunkTypeList : ScriptableObject
    {
        public List<ChunkType> chunkTypes;
    }
}