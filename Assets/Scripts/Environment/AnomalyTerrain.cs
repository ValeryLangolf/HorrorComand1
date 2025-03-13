using UnityEngine;

[RequireComponent(typeof(TerrainCollider))]
public class AnomalyTerrain : MonoBehaviour 
{
    private Terrain _terrain;

    public float Width { get; private set; }
    public float Height { get; private set; }

    private void Awake() =>
        _terrain = GetComponent<Terrain>();

    private void Start()
    {
        Width = _terrain.terrainData.size.x; 
        Height = _terrain.terrainData.size.z;
    }
}