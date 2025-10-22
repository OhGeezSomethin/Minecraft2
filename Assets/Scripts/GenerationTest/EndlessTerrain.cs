using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EndlessTerrain : MonoBehaviour
{
    const float viewerThresholdChunkUpdate = 25f;
    const float sqrViewerThresholdChunkUpdate = viewerThresholdChunkUpdate * viewerThresholdChunkUpdate;
    const float edgeTillColliderGeneration = 5;

    public int colliderLODIndex;
    public LODInfo[] detailLevels;
    public static float maxViewDist;

    public Transform viewer;
    public Material mapMaterial;

    public static Vector2 viewerPos;
    Vector2 viewerPosOld;
    static MapGenerator mapGenerator;
    float meshWorldSize;
    int chunksVisibleInView;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    static List<TerrainChunk> visibleTerrainChunks = new List<TerrainChunk>();

    void Start()
    {
        mapGenerator = FindFirstObjectByType<MapGenerator>();

        maxViewDist = detailLevels[detailLevels.Length - 1].visibleDistanceThreshold;
        meshWorldSize = mapGenerator.meshSettings.meshWorldSize;
        chunksVisibleInView = Mathf.RoundToInt(maxViewDist / meshWorldSize);

        UpdateVisibleChunks();
    }

    void Update()
    {
        viewerPos = new Vector2(viewer.position.x, viewer.position.z);

        if (viewerPos != viewerPosOld)
        {
            foreach(var chunk in visibleTerrainChunks)
            {
                chunk.UpdateCollisionMesh();
            }
        }

        if ((viewerPos - viewerPosOld).sqrMagnitude > sqrViewerThresholdChunkUpdate)
        {
            viewerPosOld = viewerPos;
            UpdateVisibleChunks();
        }
    }

    void UpdateVisibleChunks()
    {
        HashSet<Vector2> alreadyUpdatedChunkCoords = new HashSet<Vector2>();
        for(int i = visibleTerrainChunks.Count - 1; i >= 0; i--)
        {
            alreadyUpdatedChunkCoords.Add(visibleTerrainChunks[i].coord);
            visibleTerrainChunks[i].UpdateTerrainChunk();
        }

        int currentChunkCoordX = Mathf.RoundToInt(viewerPos.x / meshWorldSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPos.y / meshWorldSize);

        for(int yOffset = -chunksVisibleInView; yOffset <= chunksVisibleInView; yOffset++)
        {
            for(int xOffset = -chunksVisibleInView; xOffset <= chunksVisibleInView; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                if (!alreadyUpdatedChunkCoords.Contains(viewedChunkCoord))
                {
                    if (terrainChunkDictionary.ContainsKey(viewedChunkCoord))
                    {
                        terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                    }
                    else
                    {
                        terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, meshWorldSize, detailLevels, colliderLODIndex, transform, mapMaterial));
                    }
                }
            }
        }
    }
    public class TerrainChunk
    {
        public Vector2 coord;

        GameObject meshObj;
        Vector2 sampleCenter;
        Bounds bounds;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        MeshCollider meshCollider;

        HeightMap mapData;
        bool mapDataReceived;
        int previousLODIndex = -1;

        LODInfo[] detailLevels;
        LODMesh[] lodMeshes;
        int colliderLODIndex;
        bool hasSetCollider;

        public TerrainChunk(Vector2 coord, float meshWorldSize, LODInfo[] detailLevels, int colliderLODIndex, Transform parent, Material material)
        {
            this.coord = coord;
            this.detailLevels = detailLevels;
            this.colliderLODIndex = colliderLODIndex;

            sampleCenter = coord * meshWorldSize / mapGenerator.meshSettings.meshScale;
            Vector2 position = coord * meshWorldSize;
            bounds = new Bounds(position, Vector2.one * meshWorldSize);

            meshObj = new GameObject("Terrain Chunk");
            meshRenderer = meshObj.AddComponent<MeshRenderer>();
            meshFilter = meshObj.AddComponent<MeshFilter>();
            meshCollider = meshObj.AddComponent<MeshCollider>();
            meshRenderer.material = material;

            meshObj.transform.position = new Vector3(position.x, 0, position.y);
            meshObj.transform.parent = parent;
            SetVisible(false);

            lodMeshes = new LODMesh[detailLevels.Length];

            for (int i = 0; i < detailLevels.Length; i++)
            {
                lodMeshes[i] = new LODMesh(detailLevels[i].lod);
                lodMeshes[i].updateCallback += UpdateTerrainChunk;

                if (i == colliderLODIndex) lodMeshes[i].updateCallback += UpdateCollisionMesh;
            }

            mapGenerator.RequestHeightMap(sampleCenter, OnMapDataRecieved);
        }

        void OnMapDataRecieved(HeightMap mapData)
        {
            this.mapData = mapData;
            mapDataReceived = true;

            UpdateTerrainChunk();
        }

        public void UpdateTerrainChunk()
        {
            if (mapDataReceived)
            {
                float viewerDistFromNearEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPos));

                bool wasVisible = IsVisible();
                bool visible = viewerDistFromNearEdge <= maxViewDist;

                if (visible)
                {
                    int lodIndex = 0;

                    for (int i = 0; i < detailLevels.Length - 1; i++)
                    {
                        if (viewerDistFromNearEdge > detailLevels[i].visibleDistanceThreshold)
                        {
                            lodIndex = i + 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (lodIndex != previousLODIndex)
                    {
                        LODMesh lodMesh = lodMeshes[lodIndex];
                        if (lodMesh.hasMesh)
                        {
                            previousLODIndex = lodIndex;
                            meshFilter.mesh = lodMesh.mesh;
                        }
                        else if (!lodMesh.hasRequestedMesh)
                        {
                            lodMesh.RequestMesh(mapData);
                        }
                    }
                }
                if (wasVisible != visible)
                {
                    if (visible)
                    {
                        visibleTerrainChunks.Add(this);
                    } else
                    {
                        visibleTerrainChunks.Remove(this);
                    }
                        SetVisible(visible);
                }
            }
        }

        public void UpdateCollisionMesh()
        {
            if (!hasSetCollider)
            {
                float sqrDstFromViewerToEdge = bounds.SqrDistance(viewerPos);

                if (sqrDstFromViewerToEdge < detailLevels[colliderLODIndex].sqrVisibleDstThreshold)
                {
                    if (!lodMeshes[colliderLODIndex].hasRequestedMesh)
                    {
                        lodMeshes[colliderLODIndex].RequestMesh(mapData);
                    }
                }

                if (sqrDstFromViewerToEdge < edgeTillColliderGeneration * edgeTillColliderGeneration)
                {
                    if (lodMeshes[colliderLODIndex].hasMesh)
                    {
                        meshCollider.sharedMesh = lodMeshes[colliderLODIndex].mesh;
                        hasSetCollider = true;
                    }
                }
            }
        }

        public void SetVisible(bool visible)
        {
            meshObj.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObj.activeSelf;
        }
    }

    class LODMesh
    {
        public Mesh mesh;
        public bool hasRequestedMesh;
        public bool hasMesh;
        int lod;
        public event System.Action updateCallback;

        public LODMesh(int lod)
        {
            this.lod = lod;
        }

        void OnMeshDataReceived(MeshData meshData)
        {
            mesh = meshData.CreateMesh();
            hasMesh = true;

            updateCallback();
        }

        public void RequestMesh(HeightMap mapData)
        {
            hasRequestedMesh = true;
            mapGenerator.RequestMeshData(mapData, lod, OnMeshDataReceived);
        }
    }

    [System.Serializable] 
    public struct LODInfo
    {
        [Range(0, MeshSettings.numSupportedLODs - 1)] public int lod;
        public float visibleDistanceThreshold;

        public float sqrVisibleDstThreshold
        {
            get
            {
                return visibleDistanceThreshold; 
            }
        }
    }
}
