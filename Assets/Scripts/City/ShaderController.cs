using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace City
{
    public class GroundShaderController : MonoBehaviour
    {
        public Renderer targetRenderer;
        [SerializeField] private float baseSpeed = -0.131f;
        
        private Material mat;

        void Start()
        {
            CityController.Instance.OnSpeedChange += UpdateSpeed;
            this.mat = targetRenderer.material;
            
            float speed = CityController.Instance.CurrentSpeed();
            this.UpdateSpeed(speed);
        }

        private void UpdateSpeed(float speed)
        {
            Debug.Log("Getted speed : " + speed);
            Debug.Log("Setting shader speed to : " + speed * baseSpeed);
            this.mat.SetFloat("_SpeedAnimation", speed * baseSpeed);
        }

    }
}
