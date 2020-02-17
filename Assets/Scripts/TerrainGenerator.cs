using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{
    [Range(0, 1)]
    public double SmoothnessFactor = 0.5d;
    public int FoodCount = 5;

    [Header("Objects")]
    [SerializeField] private Tilemap TerrainTileMap;
    [SerializeField] private RuleTile LendTile;
    [SerializeField] private GameObject[] Foods;
    [SerializeField] private GameObject Finish;

    [Header("ObjectsStorages")]
    [SerializeField] private GameObject FoodObjectStorage;
    [SerializeField] private GameObject CheckpointObjectStorage;

    private int _width = 100;
    private int _maxHeight = 10;

    private int[] _heights;

    private void Start()
    {
        GenerateLevel();
    }

    public void ReGenerateLevel()
    {
        ClearLevel();
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        _heights = GenerateHeights(_width, _maxHeight, 3);
        GenerateTerrainByHeights(_heights);
        GenerateCheckpoints(_heights);
        GenerateFood(_heights);
    }

    private void ClearLevel()
    {

    }

    private int[] GenerateHeights(int width, int height, int firstHeight)
    {
        int[] heights = new int[width];
        int groundHeight = firstHeight;

        System.Random random = new System.Random();
        for (int x = 0; x < width; x++)
        {
            heights[x] = groundHeight;

            if (x % 2 == 1)
            {
                double variant = random.NextDouble();

                if (variant >= 0d && variant <= SmoothnessFactor)
                {
                    //nothing
                }
                else if (variant > SmoothnessFactor && variant <= SmoothnessFactor + (0.4d * (1d - SmoothnessFactor)))
                {
                    if (groundHeight > 2)
                    {
                        groundHeight--;
                    }
                    else
                    {
                        groundHeight++;
                    }
                }
                else if (variant > SmoothnessFactor + (0.4d * (1d - SmoothnessFactor)) && variant <= SmoothnessFactor + (0.8d * (1d - SmoothnessFactor)))
                {
                    if (groundHeight < _maxHeight - 2)
                    {
                        groundHeight++;
                    }
                    else
                    {
                        groundHeight--;
                    }
                }
                else if (variant > SmoothnessFactor + (0.8d * (1d - SmoothnessFactor)) && variant <= 1d)
                {
                    if (groundHeight > _maxHeight / 2)
                    {
                        groundHeight -= Random.Range(2, 4);
                    }
                    else
                    {
                        groundHeight += Random.Range(2, 4);
                    }
                }
            }
        }

        return heights;
    }

    private void GenerateTerrainByHeights(int[] heights)
    {
        for (int x = 0; x < heights.Length; x++)
        {
            for (int y = 0; y < heights[x]; y++)
            {
                TerrainTileMap.SetTile(new Vector3Int(x, y, 0), LendTile);
            }
        }
    }

    private void GenerateCheckpoints(int[] heights)
    {
        int width = heights.Length - 1;
        int height = heights[width] + 1;

        GameObject finishInstans = Instantiate(Finish, new Vector3(width, height, 0), Quaternion.identity);
        finishInstans.transform.parent = CheckpointObjectStorage.transform;
    }

    private void GenerateFood(int[] heights)
    {
        int distanceBetweenFood = heights.Length / (FoodCount + 2);

        int lessFoodPosition = distanceBetweenFood;

        for (int i = 0; i < FoodCount; i++)
        {
            GameObject food = Foods[Random.Range(0, Foods.Length)];

            int xPosition = lessFoodPosition + Random.Range(0, distanceBetweenFood);
            lessFoodPosition += distanceBetweenFood;

            int yPosition = Random.Range(heights[xPosition], _maxHeight);

            GameObject foodInstans = Instantiate(food, new Vector3(xPosition + .5f, yPosition + .5f, 0), Quaternion.identity);
            foodInstans.transform.parent = FoodObjectStorage.transform;
        }
    }
}
