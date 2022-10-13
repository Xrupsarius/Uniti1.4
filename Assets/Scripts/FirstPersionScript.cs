using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class FirstPersionScript : MonoBehaviour
{
    [FormerlySerializedAs("sphere")] public Transform squareSphere;
    public Transform triangleSphere;
    private float SPEED = 5f;

    private void Start()
    {
        squareSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        squareSphere.position = _squarePositions[0];

        triangleSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
        triangleSphere.position = _trianglePositions[0];
    }

    void Update()
    {
        var cor = StartCoroutine(DrawSquareAndTriangle());
    }

    private IEnumerator DrawSquareAndTriangle()
    {
        // DrawSquare();
        
        yield return null;

        DrawTriangle();

    }

    private void DrawTriangle()
    {

        if (triangleSphere.position.x >= 0)
        {
            Debug.Log("Position 1");
            triangleSphere.position = Vector3.MoveTowards(_trianglePositions[1], _trianglePositions[2], SPEED) * SPEED * Time.deltaTime;
        }
        
    }
    
    private void DrawSquare()
    {
        if (squareSphere.position.x == _squarePositions[0].x)
        {
            Debug.Log("Position 1");
            squareSphere.position += Vector3.back * SPEED * Time.deltaTime;
        }

        if (squareSphere.position.z < _squarePositions[1].z)
        {
            Debug.Log("Position 2");
            squareSphere.position += Vector3.left * SPEED * Time.deltaTime;
        }

        if (squareSphere.position.x < _squarePositions[3].x && squareSphere.position.z < _squarePositions[3].z)
        {
            Debug.Log("Position 3");
            squareSphere.position += Vector3.forward * SPEED * Time.deltaTime;
        }

        if (squareSphere.position.z > _squarePositions[3].z)
        {
            Debug.Log("Position 4");
            squareSphere.position += Vector3.right * SPEED * Time.deltaTime;
        }

        if (squareSphere.position.x > _squarePositions[0].x && squareSphere.position.z > _squarePositions[0].z)
        {
            Destroy(squareSphere.gameObject);
        }
    }

    private readonly Vector3[] _squarePositions =
    {
        new Vector3(10, 0, 10),
        new Vector3(10, 0, -10),
        new Vector3(-10, 0, -10),
        new Vector3(-10, 0, 10)
        
    };

    private readonly Vector3[] _trianglePositions =
    {
        new Vector3(10, 0, 10),
        new Vector3(0, 0, -10),
        new Vector3(-10, 0, 10)
    };

}