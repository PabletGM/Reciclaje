using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    //shot trayectory
    [SerializeField]
    private LineRenderer _lineRenderer;

    //number of parts of the line
    [SerializeField]
    private int _lineSegmentCount;
    private List<Vector3> _linePoints = new List<Vector3>();

    //singleton
    public static DrawTrajectory Instance;

    private void Awake()
    {
        Instance = this;
    }

    
    //physic method where the line is painted
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        //v o aceleracion = (F * t)/m
        Vector3 velocity = (forceVector / rigidbody.mass) * Time.deltaTime;

        //formula de rango de trayectoria, linea total dibujada
        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        //un trozo de linea o segmento
        float stepTime = FlightDuration / _lineSegmentCount;



        for (int i = 0; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i; //change in Time

            // a = v0 * t - 1/2*g*t*t
            Vector3 MovementVector = new Vector3(velocity.x * stepTimePassed, velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed, velocity.z * stepTimePassed);

            _linePoints.Add(-MovementVector + startingPoint);
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
        _linePoints.Clear();
    }

    //hide the line when is called
    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
