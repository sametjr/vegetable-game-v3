using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public enum Vegetables
    {
        beet, brocoli, caper, carrot, corn, eggplant, mushroom, onion, pumpkin, tomato
    }
    [SerializeField] private VegetableData vegetableData;
    [SerializeField] private int howManyForEach = 3;
    public static List<Queue<GameObject>> queues;

    private void Start()
    {
        InitVariables();
        InstantiateObjects();
        // Debug.Log(Enum.GetNames(typeof(Vegetables)));
        // foreach (string s in Enum.GetNames(typeof(Vegetables)))
        // {
        //     Debug.Log(s);
        // }
    }

    private void InitVariables()
    {
        queues = new List<Queue<GameObject>>();
    }

    private void InstantiateObjects()
    {
        for (int vegCount = 0; vegCount < vegetableData.vegetables.Count; vegCount++)
        {
            Queue<GameObject> currentVegetableQueue = new Queue<GameObject>();

            for (int i = 0; i < howManyForEach; i++)
            {
                GameObject veg = Instantiate(vegetableData.vegetables[vegCount].obj, Vector3.one, Quaternion.identity);
                ArrangeObject(veg, currentVegetableQueue, i, vegCount);
            }

            queues.Add(currentVegetableQueue);
        }
    }

    private void ArrangeObject(GameObject _veg, Queue<GameObject> _vegQueue, int _index, int _vegCount)
    {
        ArrangePerformance(_veg);
        _veg.transform.parent = this.transform;
        _veg.transform.localScale /= 1.5f;
        _veg.AddComponent<VegetableLinker>().@enum = (ObjectPool.Vegetables)_vegCount;
        _veg.transform.GetChild(0).gameObject.AddComponent<TouchMechanism>();
        _veg.transform.GetChild(0).gameObject.AddComponent<MeshCollider>();
        _veg.SetActive(false);
        _vegQueue.Enqueue(_veg);
    }
    private void ArrangePerformance(GameObject g)
    {
        MeshRenderer mr = g.transform.GetChild(0).GetComponent<MeshRenderer>();
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        mr.receiveShadows = false;
        mr.allowOcclusionWhenDynamic = false;
        mr.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
        mr.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;

    }

    public static void SendObjectBackToQueue(GameObject _veg)
    {
        _veg.transform.GetChild(0).GetComponent<TouchMechanism>().isActive = true;
        _veg.SetActive(false);
        queues[(int)_veg.GetComponent<VegetableLinker>().@enum].Enqueue(_veg);
    }

    public static GameObject GetVegetableFromPool(int _veg, Vector3 _pos, Quaternion _rot)
    {
        // Debug.Log("I AM GETOBJECTFROMPOOL");
        GameObject vegetable = queues[((int)_veg)].Dequeue();
        vegetable.transform.position = _pos;
        vegetable.transform.localRotation = _rot;
        vegetable.SetActive(true);
        return vegetable;
    }
}
