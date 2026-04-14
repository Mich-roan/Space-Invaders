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
private UnityEvent onInstantiateShip;
[SerializeField]
private float timeToSpawn = 15f;
[SerializeField]
private UnityEvent<Transform> onShipDestroyed;
[SerializeField]
private UnityEvent onAllShipsDestroyed;
private int destroyedSpaceships = 0;
public void OnDestroyShip(Transform transform)
    {
        destroyedSpaceships++;
        onShipDestroyed.Invoke(transform);
        if (destroyedSpaceships >= numberOfSpaceShips)
        {
            onAllShipsDestroyed?.Invoke();
        }
    }
    public void StartShips()
    {
        StartCoroutine(SpawnSpaceships());
    } 
    public void StopShips()
    {
        StopAllCoroutines();
        spaceshipPool.DeactivateAllObjects();
    }

    private IEnumerator SpawnSpaceships()
    {
        numberOfSpaceShips--;
        yield return new WaitForSeconds(timeToSpawn);
        onInstantiateShip?.Invoke();
        spaceshipPool.InstantiateObject(transform);
        EnemySpaceShip spaceship = spaceshipPool.GetCurrentObject().GetComponent<EnemySpaceShip>();
        spaceship.TargetHealth = playerHealth;
        spaceship.BulletPool = bulletPool;
        spaceship.OnDestroyed.AddListener(OnDestroyShip);
        if (numberOfSpaceShips > 0)
        {
            StartCoroutine(SpawnSpaceships());
        }
    }
}
