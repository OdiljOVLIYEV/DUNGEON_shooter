using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class DynamicNavMeshUpdater : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;

    void Start()
    {
        // Dastlab NavMesh yaratish
        navMeshSurface.BuildNavMesh();
    }

    void Update()
    {
        // Har bir freymda NavMesh yangilanishi
        navMeshSurface.BuildNavMesh();
    }

    // Ob'ekt qo'shilganda yoki olib tashlanganda NavMesh yangilash
    void OnTransformChildrenChanged()
    {
        navMeshSurface.BuildNavMesh();
    }
}