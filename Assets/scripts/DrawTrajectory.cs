using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{

    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    [Range(3, 30)]
    private int _lineSegmentCount = 20;

    private List<Vector3> _linePoints = new List<Vector3>();

    public static DrawTrajectory Instance;

    private void Awake()
    {
        Instance = this;
    }

    

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        //v o aceleracion = (F * t)/m
        Vector3 velocity = (forceVector / rigidbody.mass) * Time.deltaTime;

        //formula de rango de trayectoria, linea total dibujada
        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        //un trozo de linea o segmento
        float stepTime = FlightDuration / _lineSegmentCount;

        _linePoints.Clear();

        for (int i = 0; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i; //change in Time

            // a = v0 * t - 1/2*g*t*t
            Vector3 MovementVector = new Vector3(velocity.x * stepTimePassed, velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed, velocity.z * stepTimePassed);

            //RaycastHit hit;
            //if (Physics.Raycast(startingPoint, -MovementVector, out hit, MovementVector.magnitude))
            //{
            //    break;
            //}
            _linePoints.Add(-MovementVector + startingPoint);
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }
    
    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
