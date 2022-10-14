using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PersonScript : MonoBehaviour
{
    [SerializeField, Range(0, 10)] private float _step = 5;
    [SerializeField] private MotionFigure _figure;
    [SerializeField, Range(0, 10)] private float _speed = 5;

    private Coroutine _motionRoutine;
    private Vector3 _startPosition = Vector3.zero;
    private List<GameObject> _pointShapes = new List<GameObject>();

    private void Start() 
    {
        StartMotion(_figure);
    }

    private IEnumerator ShapeMotion(float rotationAngle)
    {
        do
        {
            yield return MotionForward();
            AddPointShape();
            yield return Rotation(rotationAngle);
        } while (transform.position != _startPosition);
    }

    private IEnumerator StartMotion()
    {
        do
        {
            yield return MotionForward();
            AddPointShape();
            yield return Rotation(-72);
            yield return MotionForward();
            AddPointShape();
            yield return Rotation(144);
        } while (transform.position != _startPosition);
    }

    private void StopMotion()
    {
        StopCoroutine(_motionRoutine);
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        RemoveAllPointShapes();
    }

    private void AddPointShape()
    {
        var shapeSize = 0.3f;
        var createdShape = GameObject.CreatePrimitive(PrimitiveType.Cube);
        createdShape.transform.localScale = Vector3.one * shapeSize;
        createdShape.transform.position = transform.position;
        _pointShapes.Add(createdShape);

    }

    private void RemoveAllPointShapes()
    {
        _pointShapes.ForEach(shape => Destroy(shape));
        _pointShapes.Clear();
    }
    
    private IEnumerator MotionForward()
    {
        var position = transform.position + transform.forward * _step;

        while (transform.position != position)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, _speed * Time.deltaTime);
            yield return null;
        }
        
        yield break;
    }

    private IEnumerator Rotation(float angle)
    {
        transform.Rotate(Vector3.up, angle);
        yield break;
    }
    
    private void StartMotion(MotionFigure figure)
    {
        switch (figure)
        {
            case MotionFigure.Square:
                _motionRoutine = StartCoroutine(ShapeMotion(90));
                break;
            
            case MotionFigure.Triangle:
                _motionRoutine = StartCoroutine(ShapeMotion(120));
                break;
            
            case MotionFigure.Star:
                _motionRoutine = StartCoroutine(StartMotion());
                break;
            
            case MotionFigure.Hexagon:
                _motionRoutine = StartCoroutine(ShapeMotion(60));
                break;
        }
    }
}