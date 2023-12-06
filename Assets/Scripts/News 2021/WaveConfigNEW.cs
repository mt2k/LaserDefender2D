using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config NEW", fileName = "New Wave Config NEW")]
public class WaveConfigNEW : MonoBehaviour
{
    [SerializeField] Transform pathPrefabs;
    [SerializeField] float moveSpeed = 5f;

    public Transform GetStartingWavePont()
    {
        return pathPrefabs.GetChild(0);
    }

    public List<Transform> GetWayponts()
    {
        List<Transform> waypoints = new List<Transform>();
        foreach (Transform item in pathPrefabs)
        {
            waypoints.Add(item);
        }
        return waypoints;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
