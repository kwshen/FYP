﻿using UnityEngine;
using System.Collections;

public class SardineBoidsController : MonoBehaviour
{
    public GameObject sardinePrefab;
    GameObject[] sardines;
    public int maxXNum = 2;
    public int maxYNum = 3;
    public int maxZNum = 4;
    public Vector3 meanPos;
    public int sardineCount;
    public float rotateSpeed = 1f;
    public GameObject meanDummy;
    public GameObject river;
    public float depthRecover = 2;

    void Start()
    {
        river = GameObject.FindGameObjectWithTag("Water");


        sardineCount = maxZNum * maxYNum * maxXNum;
        sardines = new GameObject[sardineCount];
        for (int k = 0; k < maxZNum; k++)
        {
            for (int j = 0; j < maxYNum; j++)
            {
                for (int i = 0; i < maxXNum; i++)
                {
                    int sNum = k * maxXNum * maxYNum + j * maxXNum + i;
                    sardines[sNum] = (GameObject)GameObject.Instantiate(sardinePrefab, transform.position + Vector3.right * i + Vector3.up * j + Vector3.forward * k, transform.rotation);
                    Collider[] cols = sardines[k * maxXNum * maxYNum + j * maxXNum + i].GetComponentsInChildren<Collider>();
                    foreach (Collider col in cols)
                    {
                        col.name = "SardineCol";
                    }
                }
            }
        }
    }

    void Update()
    {
        meanPos = Vector3.zero;
        for (int i = 0; i < sardineCount; i++)
        {
            meanPos = meanPos + sardines[i].transform.position;
        }
        meanPos = meanPos / sardineCount;

        // Ensure the meanDummy does not exceed the river height
        if (meanPos.y > river.transform.position.y - depthRecover)
        {
            meanPos.y = river.transform.position.y - depthRecover;
        }

        meanDummy.transform.position = meanPos;

        for (int i = 0; i < sardineCount; i++)
        {
            Vector3 targetRelPos = meanPos - sardines[i].transform.position;
            targetRelPos.Normalize();
            float dottigawa = Vector3.Dot(targetRelPos, sardines[i].transform.right);

            Rigidbody iwasirigid = sardines[i].GetComponent<Rigidbody>();
            iwasirigid.AddTorque(sardines[i].transform.up * dottigawa * rotateSpeed);
            sardines[i].GetComponent<Animator>().SetFloat("Turn", dottigawa * rotateSpeed);

            dottigawa = Vector3.Dot(targetRelPos, sardines[i].transform.up);
            iwasirigid.AddTorque(-sardines[i].transform.right * dottigawa * rotateSpeed);
            sardines[i].GetComponent<Animator>().SetFloat("Up", dottigawa * rotateSpeed);
        }
    }
}

