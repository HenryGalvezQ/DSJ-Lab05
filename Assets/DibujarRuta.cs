using UnityEngine;

// Fuerza al script a ejecutarse en el editor
[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class DibujarRuta : MonoBehaviour
{
    // Arrastra aquí los mismos waypoints que usaste para el auto
    public GameObject[] waypoints;

    // --- ESTA ES LA LÍNEA QUE PIDES ---
    // Define la altura a la que quieres que se dibuje la pista
    // La he puesto en 0.2f para que se vea SOBRE el suelo (Y=0)
    // ¡Cámbiala a 0.0f si lo prefieres!
    public float alturaDeLaPista = 0.2f;

    private LineRenderer lineRenderer;

    void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ActualizarPista();
    }

    void OnValidate()
    {
        ActualizarPista();
    }

    void ActualizarPista()
    {
        if (waypoints == null || waypoints.Length == 0 || lineRenderer == null)
        {
            if (lineRenderer != null) lineRenderer.positionCount = 0;
            return;
        }

        lineRenderer.positionCount = waypoints.Length;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] == null)
            {
                lineRenderer.positionCount = 0;
                return;
            }

            // 1. Obtiene la posición (X, Y, Z) del waypoint
            Vector3 pos = waypoints[i].transform.position;

            // 2. ¡IGNORA LA ALTURA!
            // Sobreescribe la 'Y' con tu valor fijo
            pos.y = alturaDeLaPista;

            // 3. Asigna el punto
            lineRenderer.SetPosition(i, pos);
        }

        lineRenderer.loop = true;
    }
}