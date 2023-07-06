using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldOfVeiw : MonoBehaviour
{
    [SerializeField] public LayerMask layerMask;
    //public static fieldOfVeiw instance;
    private Mesh mesh;
    private float fov;
    private Vector3 origin;
    private float startingAngle;
    private Vector3 StartAngleVector; 
    private const int rayCount = 50;
    Vector3[] vertices = new Vector3[rayCount + 1 + 1];
    // Start is called before the first frame update
    private void Start()
    {
         mesh = new Mesh();
        mesh.MarkDynamic();
        int[] triangles = new int[rayCount * 3];

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            if (i > 0)
            {
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 10000f);
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void LateUpdate()
    {
        //transform.position = new Vector2(0, 0);
        //transform.rotation = Quaternion.Euler(0, 30, 0);
        fov = 90f;
        int rayCount = 50;
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;
        float viewDistance = 50f;

        vertices[0] = origin;
        int vertexIndex = 1;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, GetVectorFromAngle(angle), viewDistance, layerMask);
            if (raycastHit2D.collider == null)
            {
                vertex = origin + GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = raycastHit2D.point;
            }
            vertices[vertexIndex] = vertex;

            vertexIndex++;

            angle -= angleIncrease;
        }
        //vertices[1] = new Vector3(50, 0);
        //vertices[2] = new Vector3(0, -50);

        //triangles[0] = 0;
        //triangles[1] = 1;
        //triangles[2] = 2;
        mesh.vertices = vertices;

    }
    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
     }
    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }
    public void SetAimDirection(Quaternion aimDirection)
    {
        //startingAngle = GetAngleFromVectorFloat(aimDirection.eulerAngles) - fov / 2f;
        startingAngle = aimDirection.eulerAngles.z - fov * 2.5f;
        StartAngleVector = aimDirection.eulerAngles;
        //startingAngle = aimDirection;
    }

}