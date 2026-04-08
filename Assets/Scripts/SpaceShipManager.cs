using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SpaceShipManager : MonoBehaviour
{
[SerializeField]
private Health playerHealth;
[SerializeField]
private int numberOfSpaceShips = 5;
[SerializeField]
private InstantiatePoolObjects spaceshipPool;
[SerializeField]
private InstantiatePoolObjects bulletPool;
[SerializeField]
private float timeToSpawn = 15f;
[SerializeField]
private UnityEvent<Transform> onShipDestroyed;
public void OnDestroyShip(Transform transform)
    {
        onShipDestroyed.Invoke(transform);
    }
    private void Start()
    {
        StartCoroutine(SpawnSpaceships());
    } 
    private IEnumerator SpawnSpaceships()
    {
        numberOfSpaceShips--;
        yield return new WaitForSeconds(timeToSpawn);
        spaceshipPool.InstantiateObject(transform);
        EnemySpaceShip.spaceship = spaceshipPool.GetCurrentObject().GetComponent<EnemySpaceShip>();
        spaceship.TargetHealth = playerHealth;
        spaceship.BulletPool = bulletPool;
        spaceship.OnDestroyed.AddListener(OnDestroyShip);
        if (numberOfSpaceShips > 0)
        {
            StartCoroutine(SpawnSpaceships());
        }
    }
}
