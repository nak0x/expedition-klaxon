using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundShaderController : MonoBehaviour
{
    public Renderer targetRenderer; // Associe ton mesh (ex: le sol) dans l’inspecteur
    private Material mat;

    void Start()
    {
        // Récupère une instance unique du matériau (sinon ça change tous les objets avec ce mat)
        mat = targetRenderer.material;
    }

    void Update()
    {
        // Exemple : changer la vitesse de l’animation
        if (Input.GetKey(KeyCode.UpArrow))
        {
            float speed = mat.GetFloat("_SpeedAnimation");
            mat.SetFloat("_SpeedAnimation", speed + 0.1f);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            float speed = mat.GetFloat("_SpeedAnimation");
            mat.SetFloat("_SpeedAnimation", speed - 0.1f);
        }

        // Exemple : changer la couleur A avec la touche C
        if (Input.GetKeyDown(KeyCode.C))
        {
            mat.SetColor("_ColorA", Random.ColorHSV());
        }

        // Exemple : changer la fréquence X,Y
        if (Input.GetKeyDown(KeyCode.F))
        {
            mat.SetVector("_Frequency", new Vector2(Random.Range(1, 10), Random.Range(10, 50)));
        }
    }
}

