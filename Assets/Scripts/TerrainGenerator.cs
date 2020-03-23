using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainGenerator : MonoBehaviour
{
    [Header("GeneratedMapParameters")]
    [Range(0, 1)]
    [SerializeField] private double _smoothnessFactor = 0.5d;
    [SerializeField] private int _initFoodCount = 5;
    [SerializeField] private int _initWidth = 100;
    [SerializeField] private int _maxHeight = 12;
    [SerializeField] private int _finishWidth = 8;

    [Header("Objects")]
    [SerializeField] private Tilemap _terrainTileMap;
    [SerializeField] private RuleTile _lendTile;
    [SerializeField] private GameObject _finishCheckpoint;
    [SerializeField] private GameObject _finishPortal;
    [SerializeField] private GameObject[] _foods;

    [Header("ObjectsStorages")]
    [SerializeField] private GameObject _foodObjectStorage;
    [SerializeField] private GameObject _checkpointObjectStorage;
    [SerializeField] private GameObject _portalObjectStorage;

    private int _width;
    private int _foodCount;

    private int[] _heights;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        _width = _initWidth + (Bag.Instance.TrophyCount * 10);
        _heights = GenerateHeights(_width, _maxHeight, 5);

        GenerateTerrainByHeights(_heights);

        GenerateFinishByHeights(_heights);

        _foodCount = _initFoodCount + (Bag.Instance.TrophyCount / 2);
        GenerateFoodByHeights(_heights, _foodCount);
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

                if (variant >= 0d && variant <= _smoothnessFactor)
                {
                    //nothing
                }
                else if (variant > _smoothnessFactor && variant <= _smoothnessFactor + (0.4d * (1d - _smoothnessFactor)))
                {
                    if (groundHeight > 3)
                    {
                        groundHeight--;
                    }
                    else
                    {
                        groundHeight++;
                    }
                }
                else if (variant > _smoothnessFactor + (0.4d * (1d - _smoothnessFactor)) && variant <= _smoothnessFactor + (0.8d * (1d - _smoothnessFactor)))
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
                else if (variant > _smoothnessFactor + (0.8d * (1d - _smoothnessFactor)) && variant <= 1d)
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
                _terrainTileMap.SetTile(new Vector3Int(x, y, 0), _lendTile);
            }
        }
    }

    private void GenerateFinishByHeights(int[] heights)
    {
        int widthWithFinish = _width + _finishWidth;
        int finishHeight = heights[_width - 1];

        for (int x = _width; x < widthWithFinish; x++)
        {
            for (int y = 0; y < finishHeight; y++)
            {
                _terrainTileMap.SetTile(new Vector3Int(x, y, 0), _lendTile);
            }
        }

        GameObject finishInstans = Instantiate(_finishCheckpoint, new Vector3(_width, finishHeight, 0), Quaternion.identity);
        finishInstans.transform.parent = _checkpointObjectStorage.transform;

        GameObject finishPortalInstans = Instantiate(_finishPortal, new Vector3(widthWithFinish - 1, finishHeight, 0), Quaternion.identity);
        finishPortalInstans.transform.parent = _portalObjectStorage.transform;
    }

    private void GenerateFoodByHeights(int[] heights, int foodCount)
    {
        int distanceBetweenFood = heights.Length / (foodCount + 2);

        int lessFoodPosition = distanceBetweenFood;

        for (int i = 0; i < foodCount; i++)
        {
            GameObject food = _foods[Random.Range(0, _foods.Length)];

            int xPosition = lessFoodPosition + Random.Range(0, distanceBetweenFood);
            lessFoodPosition += distanceBetweenFood;

            int yPosition = Random.Range(heights[xPosition], _maxHeight);

            GameObject foodInstans = Instantiate(food, new Vector3(xPosition + .5f, yPosition + .5f, 0), Quaternion.identity);
            foodInstans.transform.parent = _foodObjectStorage.transform;
        }
    }
}
