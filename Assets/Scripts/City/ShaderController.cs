using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace City
{
    public class GroundShaderController : MonoBehaviour
    {
        public Renderer targetRenderer; 
        private Material mat;

        void Start()
        {
            mat = targetRenderer.material;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                float speed = mat.GetFloat("_SpeedAnimation");
                mat.SetFloat("_SpeedAnimation", speed + 0.1f);
            }
        }
    }
}
