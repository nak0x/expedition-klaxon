using UnityEngine;

namespace Scriptables
{
    public enum ChunkContentType
    {
        Building,
        Shop,
        Park,
        Debug
    }
    [CreateAssetMenu(fileName = "ChunkType", menuName = "Chunks/Chunk Type", order = 0)]
    public class ChunkType : ScriptableObject
    {
        public GameObject Prefab;
        public ChunkContentType type;
    }
}