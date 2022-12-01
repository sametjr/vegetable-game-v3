using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private float minx, maxx, miny, maxy, minz, maxz;
    [SerializeField, Range(1, 10)] private int howManyForEachToSpawn = 3;
    [SerializeField] private GameObject spawnParticlePrefab;
    private List<GameObject> objectsInScene;

    private void Start()
    {
        objectsInScene = new List<GameObject>();
    }


    public Vector3 GetRandomPosition()
    {
        return new Vector3(UnityEngine.Random.Range(minx, maxx), UnityEngine.Random.Range(miny, maxy), UnityEngine.Random.Range(minz, maxz));
    }

    public Quaternion GetRandomQuaternion()
    {
        return new Quaternion(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), 1);
    }



    public void SpawnObjects()
    {
        for (int j = 0; j < ObjectPool.queues.Count; j++)
        {
            // for (int i = 0; i < howManyForEachToSpawn; i++)
            // {
            //     ObjectPool.GetVegetableFromPool(j, GetRandomPosition(), GetRandomQuaternion());
            // }
            StartCoroutine(SpawnVegetable(j));
        }
    }

    private IEnumerator SpawnVegetable(int j)
    {

        for (int i = 0; i < howManyForEachToSpawn; i++)
        {
            Vector3 randomPos = GetRandomPosition();
            GameObject veg = ObjectPool.GetVegetableFromPool(j, randomPos, GetRandomQuaternion());
            objectsInScene.Add(veg);
            GameObject particle = Instantiate(spawnParticlePrefab, randomPos, Quaternion.identity);
            Vector3 prevScale = veg.transform.localScale;
            veg.transform.localScale = Vector3.zero;
            LeanTween.scale(veg, prevScale, .4f).setEaseInOutBounce().setOnComplete(() => Destroy(particle));

            yield return new WaitForSecondsRealtime(.4f);
        }
    }

    public void ClearScene()
    {
        foreach (GameObject _g in objectsInScene)
        {
            ObjectPool.SendObjectBackToQueue(_g);
        }
        objectsInScene.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) SpawnObjects();
        if (Input.GetKeyDown(KeyCode.S)) ClearScene();
    }
}
