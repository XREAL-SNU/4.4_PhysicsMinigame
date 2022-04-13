using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// [RequireComponent(typeof(Animator))] // Requires animator with parameter "flySpeed" catering for 0, 1 (idle, flap)
[RequireComponent(typeof(Rigidbody))] // Requires Rigidbody to move around

public class RandomFlyer : MonoBehaviour, MosquitoObserver
{
    [SerializeField] float idleSpeed, turnSpeed, switchSeconds, idleRatio;
    [SerializeField] Vector2 animSpeedMinMax, moveSpeedMinMax, changeAnimEveryFromTo, changeTargetEveryFromTo;
    [SerializeField] Transform homeTarget, flyingTarget;
    [SerializeField] Vector2 radiusMinMax;
    [SerializeField] Vector2 yMinMax;
    [SerializeField] public bool returnToBase = false;
    [SerializeField] public float randomBaseOffset = 5, delayStart = 0f;

    // private Animator animator;
    private Rigidbody body;
    [System.NonSerialized]
    public float changeTarget = 0f, changeAnim = 0f, timeSinceTarget = 0f, timeSinceAnim = 0f, prevAnim, currentAnim = 0f, prevSpeed, speed, zturn, prevz,
        turnSpeedBackup;
    private Vector3 rotateTarget, position, direction, velocity, randomizedBase;
    private Quaternion lookRotation;
    [System.NonSerialized] public float distanceFromBase, distanceFromTarget;


    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;

    Transform wall1_t;
    Transform wall2_t;
    Transform wall3_t;
    Transform wall4_t;
    Dictionary<Transform, float> distmap = new Dictionary<Transform, float>();

    protected string gameState;
    private bool IsStop = false;
    protected int gameRound;
    protected bool IsEnd = false;

    public void GameStateUpdate(GameManager.GameState gameState)
    {
        this.gameState = gameState.ToString();

    }

    public void RoundUpdate(int gameRound)
    {
        this.gameRound = gameRound;
    }
    public void IsEndUpdate()
    {
        this.IsEnd = true;
    }

    void Start()
    {
        // Inititalize
        GameManager.Instance().AddMosquito(this.GetComponent<RandomFlyer>());

        // animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        turnSpeedBackup = turnSpeed;
        direction = Quaternion.Euler(transform.eulerAngles) * (Vector3.forward);
        if (delayStart < 0f) body.velocity = idleSpeed * direction;

        wall1_t = wall1.transform;
        wall2_t = wall2.transform;
        wall3_t = wall3.transform;
        wall4_t = wall4.transform;

        distmap.Add(wall1_t, 0F);
        distmap.Add(wall2_t, 0F);
        distmap.Add(wall3_t, 0F);
        distmap.Add(wall4_t, 0F);

    }

    void FixedUpdate()
    {
        if (gameState == "Track" && !IsEnd)
        {
            IsStop = false;
            // Wait if start should be delayed (useful to add small differences in large flocks)
            if (delayStart > 0f)
            {
                delayStart -= Time.fixedDeltaTime;
                return;
            }
            // Calculate distances
            distanceFromBase = Vector3.Magnitude(randomizedBase - body.position);
            distanceFromTarget = Vector3.Magnitude(flyingTarget.position - body.position);
            // Allow drastic turns close to base to ensure target can be reached
            if (returnToBase && distanceFromBase < 10f)
            {
                if (turnSpeed != 300f && body.velocity.magnitude != 0f)
                {
                    turnSpeedBackup = turnSpeed;
                    turnSpeed = 300f;
                }
                else if (distanceFromBase <= 2f)
                {
                    body.velocity = Vector3.zero;
                    turnSpeed = turnSpeedBackup;
                    return;
                }
            }
            // Time for a new animation speed
            if (changeAnim < 0f)
            {
                prevAnim = currentAnim;
                currentAnim = ChangeAnim(currentAnim);
                changeAnim = Random.Range(changeAnimEveryFromTo.x, changeAnimEveryFromTo.y);
                timeSinceAnim = 0f;
                prevSpeed = speed;
                if (currentAnim == 0) speed = idleSpeed;
                else speed = Mathf.Lerp(moveSpeedMinMax.x, moveSpeedMinMax.y, (currentAnim - animSpeedMinMax.x) / (animSpeedMinMax.y - animSpeedMinMax.x));
            }
            // Time for a new target position
            if (changeTarget < 0f)
            {
                rotateTarget = ChangeDirection(body.transform.position);
                if (returnToBase) changeTarget = 0.2f; else changeTarget = Random.Range(changeTargetEveryFromTo.x, changeTargetEveryFromTo.y);
                timeSinceTarget = 0f;
            }
            // Turn when approaching height limits
            // ToDo: Adjust limit and "exit direction" by object's direction and velocity, instead of the 10f and 1f - this works in my current scenario/scale
            if (body.transform.position.y < yMinMax.x + 10f ||
                body.transform.position.y > yMinMax.y - 10f)
            {
                if (body.transform.position.y < yMinMax.x + 10f) rotateTarget.y = 1f; else rotateTarget.y = -1f;
            }
            //body.transform.Rotate(0f, 0f, -prevz, Space.Self); // If required to make Quaternion.LookRotation work correctly, but it seems to be fine
            zturn = Mathf.Clamp(Vector3.SignedAngle(rotateTarget, direction, Vector3.up), -45f, 45f);
            // Update times
            changeAnim -= Time.fixedDeltaTime;
            changeTarget -= Time.fixedDeltaTime;
            timeSinceTarget += Time.fixedDeltaTime;
            timeSinceAnim += Time.fixedDeltaTime;

            // Rotate towards target
            if (rotateTarget != Vector3.zero) lookRotation = Quaternion.LookRotation(rotateTarget, Vector3.up);
            Vector3 rotation = Quaternion.RotateTowards(body.transform.rotation, lookRotation, turnSpeed * Time.fixedDeltaTime).eulerAngles;
            body.transform.eulerAngles = rotation;
            // Rotate on z-axis to tilt body towards turn direction
            float temp = prevz;
            if (prevz < zturn) prevz += Mathf.Min(turnSpeed * Time.fixedDeltaTime, zturn - prevz);
            else if (prevz >= zturn) prevz -= Mathf.Min(turnSpeed * Time.fixedDeltaTime, prevz - zturn);
            // Min and max rotation on z-axis - can also be parameterized
            prevz = Mathf.Clamp(prevz, -45f, 45f);
            // Remove temp if transform is rotated back earlier in FixedUpdate
            body.transform.Rotate(0f, 0f, prevz - temp, Space.Self);
            // Move flyer
            direction = Quaternion.Euler(transform.eulerAngles) * Vector3.forward;
            if (returnToBase && distanceFromBase < idleSpeed)
            {
                body.velocity = Mathf.Min(idleSpeed, distanceFromBase) * direction;
            }
            else body.velocity = Mathf.Lerp(prevSpeed, speed, Mathf.Clamp(timeSinceAnim / switchSeconds, 0f, 1f)) * direction;
            // Hard-limit the height, in case the limit is breached despite of the turnaround attempt
            if (body.transform.position.y < yMinMax.x || body.transform.position.y > yMinMax.y)
            {
                position = body.transform.position;
                position.y = Mathf.Clamp(position.y, yMinMax.x, yMinMax.y);
                body.transform.position = position;
            }
        }
        // gamestate is Catch
        else if (gameState == "Catch" && !IsEnd)
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
            // Find closest wall from current position
            distmap[wall1_t] = Vector3.Distance(body.transform.position, wall1_t.position);
            distmap[wall2_t] = Vector3.Distance(body.transform.position, wall2_t.position);
            distmap[wall3_t] = Vector3.Distance(body.transform.position, wall3_t.position);
            distmap[wall4_t] = Vector3.Distance(body.transform.position, wall4_t.position);

            var closestWall = distmap.OrderBy(kvp => kvp.Value).Last().Key;

            //ray cast to closestwall and attack to the wall
            RaycastHit hit;
            int layerMask = 1 << LayerMask.NameToLayer("Wall");

            if (Physics.Raycast(transform.position, (body.transform.position - closestWall.position).normalized, out hit, 100f, layerMask) && !IsStop)
            {
                body.transform.position = hit.point;
                IsStop = true;
                Debug.Log($"{name} stop!");
            }

        }

    }

    // Select a new animation speed randomly
    private float ChangeAnim(float currentAnim)
    {
        float newState;
        if (Random.Range(0f, 1f) < idleRatio) newState = 0f;
        else
        {
            newState = Random.Range(animSpeedMinMax.x, animSpeedMinMax.y);
        }
        return newState;
    }

    // Select a new direction to fly in randomly
    private Vector3 ChangeDirection(Vector3 currentPosition)
    {
        Vector3 newDir;
        if (returnToBase)
        {
            randomizedBase = homeTarget.position;
            randomizedBase.y += Random.Range(-randomBaseOffset, randomBaseOffset);
            newDir = randomizedBase - currentPosition;
        }
        else if (distanceFromTarget > radiusMinMax.y)
        {
            newDir = flyingTarget.position - currentPosition;
        }
        else if (distanceFromTarget < radiusMinMax.x)
        {
            newDir = currentPosition - flyingTarget.position;
        }
        else
        {
            // 360-degree freedom of choice on the horizontal plane
            float angleXZ = Random.Range(-Mathf.PI, Mathf.PI);
            // Limited max steepness of ascent/descent in the vertical direction
            float angleY = Random.Range(-Mathf.PI / 48f, Mathf.PI / 48f);
            // Calculate direction
            newDir = Mathf.Sin(angleXZ) * Vector3.forward + Mathf.Cos(angleXZ) * Vector3.right + Mathf.Sin(angleY) * Vector3.up;
        }
        return newDir.normalized;
    }

}