using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationSystem : MonoBehaviour
{
    [SerializeField] private GameObject points;

    private List<GameObject> pointList = new List<GameObject>();

    // 시각화 리스트
    private List<GameObject> movePoints = new List<GameObject>();
    public List<Vector3> turningPointList = new List<Vector3>();

    // 벡터 계산 리스트
    private List<Vector3> wayPointList = new List<Vector3>();

    private NavMeshAgent navMeshAgent;
    private NavMeshPath navMeshPath;

    [SerializeField] private int targetPoint;
    public GameObject turningPoint;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        int pointCount = points.transform.childCount;

        for (int i = 0; i < pointCount; i++)
        {
            pointList.Add(points.transform.GetChild(i).gameObject);
        }

        // 타겟 포인트 색 변경으로 시각화
        pointList[targetPoint].GetComponent<MeshRenderer>().material.color = Color.yellow;

        navMeshPath = new NavMeshPath();
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            targetPoint++;

            navMeshAgent.CalculatePath(pointList[targetPoint].transform.position, navMeshPath);

            foreach (Vector3 point in navMeshPath.corners)
            {
                Debug.Log("targetPoint: " + (targetPoint + 1) + "\ncorner: " + point);
            }
            targetPoint = 15;
            navMeshAgent.CalculatePath(pointList[15].transform.position, navMeshPath);

            foreach (Vector3 point in navMeshPath.corners)
                Debug.Log("targetPoint: " + (targetPoint + 1) + "\ncorner: " + point);
        }
        */


    }

    public void OnTriggerEnter(Collider other)
    {
        // 웨이포인트 위치 시각화
        DeleteTurningPoints();

        Debug.Log("Point " + other.name + " Collision!");

        //루트 재계산
        UpdatePathList();

        foreach (var p in navMeshPath.corners)
        {
            Debug.Log("targetPoint: " + (targetPoint + 1) + "\ncorner: " + p);
            turningPointList.Add(p);
        }
        MakeTurningPoints();
        // 시각화 끝

        
    }

    // 웨이포인트 위치 확인
    private void MakeTurningPoints()
    {
        if (turningPointList.Count == 0)
        {
            return;
        }
        foreach (var p in turningPointList)
        {
            movePoints.Add(Instantiate(turningPoint, p, transform.rotation));
        }
        turningPointList.Clear();
    }
    private void DeleteTurningPoints()
    {
        if (movePoints.Count == 0)
        {
            return;
        }
        for (int i = 0; i < movePoints.Count; i++)
        {
            Destroy(movePoints[i]);
        }
        movePoints.Clear();
    }
    // 웨이포인트 위치확인 끝
    
    // 루트 재계산 및 웨이포인트 리스트 업데이트
    private void UpdatePathList()
    {
        wayPointList.Clear();

        navMeshAgent.CalculatePath(pointList[targetPoint].transform.position, navMeshPath);

        foreach (var waypoint in navMeshPath.corners)
        {
            wayPointList.Add(waypoint);
        }
    }

    void CalculateVector(Collider crossPoint)
    {
        
    }
}
