using UnityEngine;
using Navigation2D.Geometry; 

public class MovableAgent : MonoBehaviour
{
    #region Fields and Properties
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float steerForce = 1.0f;
    [SerializeField] private float agentRadius = 1.0f;

    private Vector2 velocity = Vector2.zero; 
    private Cell[] path = new Cell[0];
    private int pathIndex = 1; 
    private bool isMoving = false;

    public bool IsMoving => isMoving; 

    public Vector2 TargetPosition
    {
        get
        {
            if (path.Length == 0) return transform.position;
            return (Vector2)(path[path.Length - 1].Position + GridData.Offset); 
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// This method will apply a steering to the agent velocity in order to aim toward a velocity that goes toward a target position.
    /// </summary>
    /// <param name="_target">Target position</param>
    private void Seek(Vector2 _target)
    {
        Vector2 _targetVelocity = _target - (Vector2)transform.position;
        Vector2 _steer = (_targetVelocity - velocity) * steerForce;
        _steer *= Time.deltaTime;
        velocity += _steer; 
    }


    /// <summary>
    /// While the agent is moving.
    /// Check if the agent is close enough to its destination, if so, stop the agent
    /// else if the agfent is close enough, proceed to the next position.
    /// Then get the normal point of the current velocity on the path and if the predicted position is to far away from this normal point
    /// Apply the seeking behaviour.
    /// </summary>
    private void MoveAgent()
    {
        if (isMoving)
        {

            if (Vector2.Distance(transform.position, TargetPosition) <= agentRadius)
            {
                StopAgent();
            }
            else
            {
                Vector3 _previousPosition = path[pathIndex - 1].Position + GridData.Offset;
                Vector3 _nextPosition = path[pathIndex].Position + GridData.Offset;
                if (Vector2.Distance(transform.position, _nextPosition) <= agentRadius)
                {
                    //Increasing path index
                    pathIndex++;
                    _previousPosition = path[pathIndex - 1].Position + GridData.Offset;
                    _nextPosition = path[pathIndex].Position + GridData.Offset;
                }   
                /* Get the predicted Velocity and the Predicted position*/
                Vector3 _predictedPosition = (Vector2)transform.position + velocity;
            
                /*Get the transposed Position of the predicted position on the segment between the previous and the next point
                * The agent has to get closer while it's to far away from the path */
                Vector3 _normalPoint = GeometryHelper2D.GetNormalPoint(_predictedPosition, _previousPosition, _nextPosition);

                /* Direction of the segment between the previous and the next position normalized in order to go further on the path
                * Targeted position is the normal point + an offset defined by the direction of the segment to go a little further on the path
                * If the target is out of the segment between the previous and the next position, the target position is the next position */
                Vector3 _dir = (_nextPosition - _previousPosition).normalized;
                Vector3 _targetPosition = _normalPoint;
                if (GeometryHelper2D.PointContainedInSegment(_previousPosition, _nextPosition, _targetPosition))
                    _targetPosition = _nextPosition;

                /* Distance between the predicted position and the normal point on the segment 
                * If the distance is greater than the radius, it has to steer to get closer */
                float _distance = Vector2.Distance(_predictedPosition, _normalPoint);
                float _scalarProduct = Vector3.Dot(velocity, _dir);
                if (_distance > agentRadius || _scalarProduct == -1 || velocity == Vector2.zero)
                {
                    Seek(_targetPosition);
                }
                velocity = velocity.normalized * speed;
                transform.position += (Vector3)velocity * Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(transform.forward, velocity.normalized); 
            }
        }
    }

    /// <summary>
    /// Set the destination and move the agent.
    /// </summary>
    /// <param name="_destination"></param>
    public void SetDestination(Vector2 _destination)
    {
        path = GridData.Instance.GetPath(transform.position, _destination);
        if(path.Length > 1)
        {
            pathIndex = 1;
            isMoving = true; 
        }
    }

    /// <summary>
    /// Stop the agent.
    /// </summary>
    private void StopAgent()
    {
        isMoving = false;
        path = new Cell[0];
        pathIndex = 0;
    }


    private void Update() => MoveAgent();

    private void OnDrawGizmos()
    {
        if(isMoving)
        {
            Gizmos.color = Color.yellow; 
            for (int i = 0; i < path.Length - 2; i++)
            {
                Gizmos.DrawLine(path[i].Position + GridData.Offset, path[i + 1].Position + GridData.Offset); 
            }
        }
    }
    #endregion
}
