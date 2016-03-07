using TouchScript.Behaviors;
using TouchScript.Gestures;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Transform Cube;
    public Transform Sphere;
    public Transform CubeGoal;
    public Transform SphereGoal;
    public Camera Camera;
    public float GoalRadius;
    public float BorderSize;
    private TransformGesture CubeTransformGesture;
    private TransformGesture SphereTransformGesture;
    private Vector3 CubeSpawnPosition;
    private Vector3 SphereSpawnPosition;

    // Use this for initialization
    void Start()
    {
        CubeTransformGesture = (TransformGesture)Cube.GetComponent<TransformGesture>();
        SphereTransformGesture = (TransformGesture)Sphere.GetComponent<TransformGesture>();
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        var cubeGoalDistance = (Cube.position - CubeGoal.position).magnitude;
        var sphereGoalDistance = (Sphere.position - SphereGoal.position).magnitude;
        if (CubeTransformGesture.State == Gesture.GestureState.Possible)
            Cube.position = Vector3.Lerp(Cube.position, CubeSpawnPosition, .1f);
        if (SphereTransformGesture.State == Gesture.GestureState.Possible)
            Sphere.position = Vector3.Lerp(Sphere.position, SphereSpawnPosition, .1f);
        if (cubeGoalDistance < GoalRadius && sphereGoalDistance < GoalRadius)
            Reset();
    }

    private void Reset()
    {
        CubeTransformGesture.enabled = false;
        SphereTransformGesture.enabled = false;
        Randomize();
        CubeTransformGesture.enabled = true;
        SphereTransformGesture.enabled = true;
    }

    private void Randomize()
    {
        var frustumHeight = 2.0f * Mathf.Abs(Camera.transform.position.z) * Mathf.Tan(Camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        var frustumWidth = frustumHeight * Camera.aspect;
        Cube.position = RandomPosition(frustumWidth, frustumHeight, Cube.localScale.x);
        Sphere.position = RandomPosition(frustumWidth, frustumHeight, Sphere.localScale.x);
        CubeGoal.position = RandomPosition(frustumWidth, frustumHeight, CubeGoal.localScale.x);
        SphereGoal.position = RandomPosition(frustumWidth, frustumHeight, Cube.localScale.x);
        CubeSpawnPosition = Cube.position;
        SphereSpawnPosition = Sphere.position;
    }

    private Vector3 RandomPosition(float x, float y, float size)
    {
        size = size / 2;
        x = x / 2 - size - BorderSize;
        y = x / 2 + size - BorderSize;
        return new Vector3(Random.Range(-x, x), Random.Range(-y, y));
    }
}
