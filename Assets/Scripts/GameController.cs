using Assets.Scripts;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static SimulationState simulationState = SimulationState.Stopped;
    private int currentGeneration = 0;
    private int maxGeneration = int.MaxValue;
    private float timer;
    private const float timeBetweenGenerations = 2;
    private TMP_Text maxGenerationInputField;
    private TMP_Text currentGenerationText;

    private GameObject cellContainer;
    public static Cell[,] cells;

    public const int ROW_COUNT = 7;
    public const int COLUMN_COUNT = 16;


    private void Start()
    {
        cellContainer = GameObject.Find("GamePanel");
        currentGenerationText = GameObject.Find("GenerationText").GetComponent<TMP_Text>();
        InitializeCells();
    }

    private void Update()
    {
        if(simulationState == SimulationState.Started)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenGenerations)
            {
                timer = 0;
                UpdateCells(); 
            }

        }
    }

    private void InitializeCells()
    {
        cells = new Cell[COLUMN_COUNT, ROW_COUNT];
        Cell[] cellList = cellContainer.GetComponentsInChildren<Cell>();
        int cellListIterator = 0;
        
        for (int y = 0; y < ROW_COUNT; y++)
        {
            for(int x = 0; x < COLUMN_COUNT; x++)
            {
                cells[x, y] = cellList[cellListIterator];

                cellList[cellListIterator].cellPosX = x;
                cellList[cellListIterator].cellPosY = y;

                cellListIterator++;
            }
        }

    }

   public void OnStartButtonPressed()
    {
        
        for (int y = 0; y < ROW_COUNT; y++)
        {
            for (int x = 0; x < COLUMN_COUNT; x++)
            {
                cells[x, y].CalculateDestinationCellState();
            }
        }

        currentGeneration = 0;

        simulationState = SimulationState.Started;
    }

    void UpdateCells()
    {
        Debug.Log("Updating Cells");
        for (int y = 0; y < ROW_COUNT; y++)
        {
            for (int x = 0; x < COLUMN_COUNT; x++)
            {
                cells[x, y].SetCellStateFromDestination();
            }
        }

        for (int y = 0; y < ROW_COUNT; y++)
        {
            for (int x = 0; x < COLUMN_COUNT; x++)
            {
                cells[x, y].CalculateDestinationCellState();
            }
        }

        currentGeneration++;
        currentGenerationText.text = currentGeneration.ToString();
    }

    public void OnPauseButtonPressed()
    {

    }

   
}
