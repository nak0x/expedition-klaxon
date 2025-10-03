using UnityEngine;

namespace City.Chunks
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private Vector3 movementDirection = new Vector3(0, 0, 1);
        [SerializeField] private float baseSpeed = 5.341177f;
        [SerializeField] private float speedFactor = 0.7f;
        
        [SerializeField] private float speed = 1f;

        private bool _hasSpawnedAChunk = false;

        void Start()
        {
            this.UpdateSpeedEvent(CityController.Instance.CurrentSpeed());
            CityController.Instance.OnSpeedChange += UpdateSpeedEvent;
            this.name = "Chunk" + BuildingsController.Instance.chunkCount;
            BuildingsController.Instance.chunkCount += 1;
        }

        void UpdateSpeedEvent(float newSpeed)
        {
            this.speed = newSpeed * baseSpeed;
        }
        
        void FixedUpdate()
        {
            this.transform.position = this.transform.position + movementDirection * (this.speed * this.speedFactor * Time.deltaTime);
        }
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("ChunkDestroyer"))
            {
                Destroy(this.gameObject);
            }
            
            if (other.CompareTag("ChunkSpawnAnchor"))
            {
                if (!_hasSpawnedAChunk)
                {
                    BuildingsController.Instance.SpawnChunk();
                    _hasSpawnedAChunk = true;
                }
            }
        }
    }
}