using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManager : MonoBehaviour
{
    public delegate void OnEnemyRegistered();
    public OnEnemyRegistered onEnemyRegisteredCallback;
    public delegate void OnEnemyUnregistered();
    public OnEnemyUnregistered onEnemyUnregisteredCallback;

    #region singleton
    public static EnemyManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    
    public List<GameObject> enemies = new List<GameObject>();



    public void RegisterEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy)) return;
        enemies.Add(enemy);
        onEnemyRegisteredCallback?.Invoke();
    }

    public void UnregisterEnemy(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            onEnemyUnregisteredCallback?.Invoke();
            Debug.Log("Enemy unregistered. Remaining enemies: " + enemies.Count);
        }
    }
}
