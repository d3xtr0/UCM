﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UltimateCheatmenu
{
    class GrassCutterOv : NeoGrassCutter
    {
        /*
        public static void GrowEdit(Vector3 worldPosition, float radius, int detailLayer = -1)
        {
            float num = radius * radius;
            Terrain activeTerrain = Terrain.activeTerrain;
            if (!activeTerrain)
            {
                return;
            }
            Vector3 position = activeTerrain.GetPosition();
            TerrainData terrainData = activeTerrain.terrainData;
            int detailResolution = terrainData.detailResolution;
            DetailPrototype[] detailPrototypes = terrainData.detailPrototypes;
            Vector2 vector = new Vector2(terrainData.size.x / (float)detailResolution, terrainData.size.z / (float)terrainData.detailResolution);
            int num2 = (int)(radius / vector.x);
            int num3 = (int)(radius / vector.y);
            int num4 = num2 * 2 + 1;
            int num5 = num3 * 2 + 1;
            int num6 = (int)((worldPosition.x - position.x) / terrainData.size.x * (float)detailResolution);
            int num7 = (int)((worldPosition.z - position.z) / terrainData.size.z * (float)detailResolution);
            int num8 = num6 - num2;
            int num9 = num7 - num3;

            NeoGrassCutter.data1x1[0, 0] = 100;
            for (int i = 0; i < num4; i++)
            {
                int num10 = num8 + i;
                if (num10 >= 0 && num10 < detailResolution)
                {
                    for (int j = 0; j < num5; j++)
                    {
                        int num11 = num9 + j;
                        if (num11 >= 0 && num11 < detailResolution)
                        {
                            Vector2 vector2 = new Vector2((float)(i - num2) * vector.x, (float)(j - num3) * vector.y);
                            if (vector2.sqrMagnitude < num)
                            {
                                if (detailLayer < 0)
                                {
                                    for (int k = 0; k < detailPrototypes.Length; k++)
                                    {
                                        int[,] array2 = terrainData.GetDetailLayer(num10, num11, activeTerrain.terrainData.detailWidth, activeTerrain.terrainData.detailHeight, k);
                                        terrainData.SetDetailLayer(num10, num11, k, array2);
                                    }
                                }
                                else if (detailLayer < detailPrototypes.Length)
                                {
                                    terrainData.SetDetailLayer(num10, num11, detailLayer, NeoGrassCutter.data1x1);
                                }
                            }
                        }
                    }
                }
            }
        }
        */
    }
}
