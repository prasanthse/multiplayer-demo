using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    #region VARIABLES
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float spawnMinX;
    [SerializeField] private float spawnMaxX;
    [SerializeField] private float spawnMinY;
    [SerializeField] private float spawnMaxY;
    #endregion

    internal void Spawn()
    {
        Vector3 pos = new Vector3(Random.Range(spawnMinX, spawnMaxX), Random.Range(spawnMinY, spawnMaxY), 1);

        Server.Instance.InstantiateGameObject(playerPrefab.name, pos, Quaternion.identity);
    }
}