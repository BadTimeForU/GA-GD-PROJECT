using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ClearNavMeshSurface:Editor
{
    [MenuItem("NavMesh/Debug/Force Cleanup NavMesh")]
    public static void ForceCleanupNavMesh()
    {
        if (Application.isPlaying)
            return;

        NavMesh.RemoveAllNavMeshData();
    }
}
