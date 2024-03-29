﻿using System.Linq;
using System.Collections.Generic;

using System.Reflection;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(CompositeCollider2D))]
public class ShadowCaster2DTileMap : MonoBehaviour
{

    [Space]
    [SerializeField]
    private bool selfShadows = true;
    private CompositeCollider2D tilemapCollider;


    static readonly FieldInfo meshField = typeof(ShadowCaster2D).GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly FieldInfo shapePathField = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
    static readonly MethodInfo generateShadowMeshMethod = typeof(ShadowCaster2D)
                                    .Assembly
                                    .GetType("UnityEngine.Experimental.Rendering.Universal.ShadowUtility")
                                    .GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);
 
    private void Start()
    {
        Generate();
    }
    [ContextMenu("Generate")]
    public void Generate()
    {
        DestroyAllChildren();

        tilemapCollider = GetComponent<CompositeCollider2D>();

        for (int i = 0; i < tilemapCollider.pathCount; i++)
        {
            Vector2[] pathVertices = new Vector2[tilemapCollider.GetPathPointCount(i)];
            tilemapCollider.GetPath(i, pathVertices);
            GameObject shadowCaster = new GameObject("shadow_caster_" + i);
            shadowCaster.transform.parent = gameObject.transform;
            ShadowCaster2D shadowCasterComponent = shadowCaster.AddComponent<ShadowCaster2D>();
            shadowCasterComponent.selfShadows = this.selfShadows;
            Vector3[] testPath = new Vector3[pathVertices.Length];
            for (int j = 0; j < pathVertices.Length; j++)
             {
                testPath[j] = pathVertices[j];
            }
            List<Vector3> vectorList = new List<Vector3>(testPath);
            for (int k = 0; k < vectorList.Count - 1; k++)
            {
                if (Vector3.Distance(vectorList[k], vectorList[k + 1]) > 1)
                {
                    Vector3 pointBetween = (vectorList[k] + vectorList[k + 1]) / 2;
                    vectorList.Insert(k + 1, pointBetween);
                    k--;
                } 
            }
            testPath = vectorList.ToArray();
            shapePathField.SetValue(shadowCasterComponent, testPath);
            meshField.SetValue(shadowCasterComponent, new Mesh());
            generateShadowMeshMethod.Invoke(shadowCasterComponent, new object[] { meshField.GetValue(shadowCasterComponent), shapePathField.GetValue(shadowCasterComponent) });
        }

        // Debug.Log("Generate");

    }
    public void DestroyAllChildren()
    {

        var tempList = transform.Cast<Transform>().ToList();
        foreach (var child in tempList)
        {
            DestroyImmediate(child.gameObject);
        }

    }

}