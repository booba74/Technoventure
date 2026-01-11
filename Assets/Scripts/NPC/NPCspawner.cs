using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;

public class NPCspawner : MonoBehaviour
{
    public bool RenderingGizmoz;
    public Vector3 bottomLeftCorner; // Левый нижний угол
    public Vector3 topRightCorner;   // Правый верхний угол
    public float minDistance = 1f;   // Минимальное расстояние между точками
    public int maxAttempts = 30;     // Макс. попыток генерации точки
    public int waypointsCount = 10;  // Количество точек
    public List<GameObject> prefubsVariantNPC = new List<GameObject>();
    public int countNPC;
    public List<GameObject> generingNPC = new List<GameObject>();
    public List<Vector3> _generatedPoints = new List<Vector3>();
    public List<Transform> transformPoints = new List<Transform>();

    private void Start()
    {
        GenerateWaypoints();
        for (int i = 0; i<countNPC; i++)
        {
            GameObject newNPC = Instantiate(prefubsVariantNPC[Random.Range(0, prefubsVariantNPC.Count)]);
            newNPC.GetComponent<NPCController>().enabled = false;
            newNPC.GetComponent<NavMeshAgent>().enabled = false;
            newNPC.transform.position = transformPoints[Random.Range(0, transformPoints.Count)].position;
            newNPC.GetComponent<NPCController>().points = transformPoints;
            newNPC.GetComponent<NPCController>().enabled = true;
            newNPC.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
    private void OnDrawGizmos()
    {
        // Устанавливаем цвет (например, зелёный с прозрачностью)
        Gizmos.color = new Color(1f, 0, 0, 0.2f);

        // Вычисляем центр прямоугольника
        Vector3 center = (bottomLeftCorner + topRightCorner) / 2f;

        // Вычисляем размеры прямоугольника
        Vector3 size = new Vector3(
            Mathf.Abs(topRightCorner.x - bottomLeftCorner.x),
            Mathf.Abs(topRightCorner.y - bottomLeftCorner.y),
            Mathf.Abs(topRightCorner.z - bottomLeftCorner.z)
        );

        // Рисуем прямоугольник (в 2D или 3D)
        if(RenderingGizmoz== true)
        {

       
        Gizmos.DrawCube(center, size);

        // Дополнительно: рисуем контур
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(center, size);
        }
    }
    [ContextMenu("Generate Waypoints")]
    public void GenerateWaypoints()
    {
        ClearExistingWaypoints();
        _generatedPoints.Clear();

        for (int i = 0; i < waypointsCount; i++)
        {
            Vector3 newPoint = GenerateValidPoint();

            if (newPoint != Vector3.negativeInfinity)
            {
                CreateWaypointObject(newPoint, i);
                _generatedPoints.Add(newPoint);
            }
            else
            {
                Debug.LogWarning($"Не удалось создать точку {i} - недостаточно места");
            }
        }
    }

    private Vector3 GenerateValidPoint()
    {
        for (int attempt = 0; attempt < maxAttempts; attempt++)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(bottomLeftCorner.x, topRightCorner.x),
                 Random.Range(bottomLeftCorner.y, topRightCorner.y),
                Random.Range(bottomLeftCorner.z, topRightCorner.z)
            );

            if (IsPointValid(randomPoint))
            {
                return randomPoint;
            }
        }

        return Vector3.negativeInfinity;
    }

    private bool IsPointValid(Vector3 point)
    {
        foreach (Vector3 existingPoint in _generatedPoints)
        {
            if (Vector3.Distance(point, existingPoint) < minDistance)
            {
                return false;
            }
        }
        return true;
    }

    private void CreateWaypointObject(Vector3 position, int index)
    {
        GameObject waypoint = new GameObject($"WayPoint_{index}");
        waypoint.transform.position = position;
        waypoint.transform.parent = this.transform;

        transformPoints.Add(waypoint.transform);
        //var gizmo = waypoint.AddComponent<WaypointGizmo>();
        //gizmo.radius = minDistance / 2f;
    }

    [ContextMenu("Clear Waypoints")]
    private void ClearExistingWaypoints()
    {
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}

//Вспомогательный класс для визуализации точек в редакторе
public class WaypointGizmo : MonoBehaviour
{
    public float radius = 0.5f;
    public Color color = Color.cyan;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
        Gizmos.DrawWireSphere(transform.position, radius * 1.1f);
    }
}
