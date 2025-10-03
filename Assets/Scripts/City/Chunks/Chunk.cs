using UnityEngine;

namespace City.Chunks
{
    public class Chunk : MonoBehaviour
    {
        [SerializeField] private Vector3 movementDirection = new Vector3(0, 0, 1);
        [SerializeField] private float speed = 1;
        [SerializeField] private float speedFactor = 1;

        private bool _hasSpawnedAChunk = false;

        void Start()
        {
            this.speed = CityController.Instance.CurrentSpeed();
            Debug.Log("Chunk getting speed from city controller : " + this.speed);
            CityController.Instance.OnSpeedChange += UpdateSpeedEvent;
            this.name = "Chunk" + BuildingsController.Instance.chunkCount;
            BuildingsController.Instance.chunkCount += 1;
        }

        void UpdateSpeedEvent(float newSpeed)
        {
            this.speed = newSpeed;
        }
        
        void FixedUpdate()
        {
            this.transform.position = this.transform.position + movementDirection * (this.speed * this.speedFactor * Time.deltaTime);
        }
        void OnTriggerEnter(Collider other)
        {
            Debug.Log("OnTriggerEnter chunk : " + other.gameObject.name + " " + this.name);
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