using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler 
{
    public CellState currentCellState;
    public CellState destinationCellState;
    public string cellPosition;
    public int livingNeighbors;
    public int cellPosX;
    public int cellPosY;

    

    void Start()
    {
        currentCellState = CellState.Dead;
        destinationCellState = CellState.Dead;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Cell Clicked!");

        if(GameController.simulationState == SimulationState.Started)
        {
            return;
        }

        SwitchCellState();

    }

    public void SwitchCellState()
    {
        if (currentCellState == CellState.Dead)
        {
            currentCellState = CellState.Alive;
            gameObject.GetComponent<Image>().color = new Color(0.7843138f, 0.3529412f, 0.3607843f, 1f);
        }
        else
        {
            currentCellState = CellState.Dead;
            gameObject.GetComponent<Image>().color = new Color(0.07450981f, 0.1058824f, 0.1372549f, 1f);
        }
    }

    private void SetCellColor()
    {
        if (currentCellState == CellState.Dead)
        {
            gameObject.GetComponent<Image>().color = new Color(0.07450981f, 0.1058824f, 0.1372549f, 1f);
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(0.7843138f, 0.3529412f, 0.3607843f, 1f);   
        }
    }


    public void SetCellStateFromDestination()
    {
        currentCellState = destinationCellState;
        SetCellColor();
    }

    public void CalculateDestinationCellState()
    {
        int livingNeighbors = 0;
        livingNeighbors += CheckLeftCell();
        livingNeighbors += CheckRightCell();
        livingNeighbors += CheckUpperCell();
        livingNeighbors += CheckLowerCell();
        livingNeighbors += CheckULCell();
        livingNeighbors += CheckURCell();
        livingNeighbors += CheckLLCell();
        livingNeighbors += CheckLRCell();


        //1.A living cell with two or three living neighbors survives to the next generation;
        //2.A living cell with fewer than two living neighbors dies from underpopulation;
        //3.A living cell with more than three living neighbors dies from overpopulation;
        //4.A dead cell with exactly three living neighbors becomes a living cell by reproduction.

        if (currentCellState == CellState.Alive)
        {
            if (livingNeighbors == 2 || livingNeighbors == 3)
            {
                destinationCellState = CellState.Alive;
            }
            else
            { 
                destinationCellState = CellState.Dead;
            }
        }
        else
        {
            if(livingNeighbors == 3)
            {
                destinationCellState = CellState.Alive;
            }
            else
            {
                destinationCellState = CellState.Dead;
            }
        }


    }

    private int CheckLeftCell()
    {
        if (cellPosX == 0)
        {
            return 0;
        }

        if (GameController.cells[cellPosX - 1, cellPosY].currentCellState == CellState.Alive)
        {
            return 1;
        }

        return 0;
    }

    private int CheckRightCell()
    {
        if (cellPosX >= GameController.COLUMN_COUNT - 1)
        {
            return 0;
        }
        if (GameController.cells[cellPosX + 1, cellPosY].currentCellState == CellState.Alive)
        {
            return 1;
        }

        return 0;
    }

    private int CheckUpperCell()
    {
        if (cellPosY == 0)
        {
            return 0;
        }
        if (GameController.cells[cellPosX, cellPosY - 1].currentCellState == CellState.Alive)
        {
            return 1;
        }

        return 0;
    }

    private int CheckLowerCell()
    {
        if (cellPosY >= GameController.ROW_COUNT - 1)
        {
            return 0;
        }
        if (GameController.cells[cellPosX, cellPosY + 1].currentCellState == CellState.Alive)
        {
            return 1;
        }

        return 0;
    }

    private int CheckULCell()
    {
        if ((cellPosY == 0) || cellPosX == 0)
        {
            return 0;
        }
        if (GameController.cells[cellPosX - 1, cellPosY - 1].currentCellState == CellState.Alive)
        {
            return 1;
        }

        return 0;
    }

    private int CheckURCell()
    {
        if ((cellPosY == 0) || (cellPosX >= GameController.COLUMN_COUNT - 1))
        {
            return 0;
        }
        if (GameController.cells[cellPosX + 1, cellPosY - 1].currentCellState == CellState.Alive)
        {
            return 1;
        }

        return 0;

    }

    private int CheckLLCell()
    {
        if ((cellPosY >= GameController.ROW_COUNT - 1) || cellPosX == 0)
        {
            return 0;
        }
        if (GameController.cells[cellPosX - 1, cellPosY + 1].currentCellState == CellState.Alive)
        {
            return 1;
        }

        return 0;
    }

    private int CheckLRCell()
    {
        if ((cellPosY >= GameController.ROW_COUNT - 1) || cellPosX >= GameController.COLUMN_COUNT - 1)
        {
            return 0;
        }
        if (GameController.cells[cellPosX + 1, cellPosY + 1].currentCellState == CellState.Alive)
        {
            return 1;
        }

        return 0;
    }




}
