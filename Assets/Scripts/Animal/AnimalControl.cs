using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/*
 *  Resouces/ Data / Animal
 *  Scriptable Object의 애니멀 정보 값 
 */


[RequireComponent(typeof(NavMeshAgent))]
public class AnimalControl : MonoBehaviour
{
    [ReadOnly] private NavMeshAgent navAgnet;
    [ReadOnly] private float wanderDistance = 3;

    [OnValueChanged("LoadAnimal")]
    [InlineEditor]
    public AnimalData data;

    private void Start()
    {
        if (navAgnet == null)
            navAgnet = GetComponent<NavMeshAgent>();

        if (data != null)
            LoadAnimal(data);
    }
    
    private void LoadAnimal(AnimalData animalData)
    {
        foreach (Transform child in transform)
        {
            if(Application.isEditor)
                DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
        }

        var visuals = Instantiate(data.animalModel, transform, true);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;

        if (navAgnet == null)
            navAgnet = GetComponent<NavMeshAgent>();

        navAgnet.speed = data.moveSpeed;
    }

    private void Update()
    {
        if (data == null)
            return;
        if (navAgnet.remainingDistance < 1f)
            GetNewDestination();
    }
    
    /// <summary>
    /// agent -> 충돌을 감지하면 새로운 경로로 설정한다. (가던 방향에 부딪히면 다른 방향으로 간다.)
    /// </summary>
    private void GetNewDestination()
    {
        var nextDestination = transform.position;
        nextDestination += wanderDistance * new Vector3(Random.Range(-1f, 1f),Random.Range(-1f, 1f),Random.Range(-1f, 1f));

        NavMeshHit hit;
        if (NavMesh.SamplePosition(nextDestination, out hit, 3f, NavMesh.AllAreas))
            navAgnet.SetDestination(hit.position);
    }


}
