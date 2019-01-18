using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Maze : MonoBehaviour {

    [System.Serializable]
    public class Cell
    {
        public GameObject north;
        public GameObject east;
        public GameObject west;
        public GameObject south;

        public GameObject floor;

        public int id;
    }


    public NavMeshSurface surface;

    public Transform stairs;



    public List<GameObject> walls;
    public List<float> probabilityWalls;

    public List<GameObject> floors;
    public List<float> probabilityFloors;

    public GameObject topFloor;


    public GameObject forge;
    public GameObject objective;

    public int xSize = 5;
    public int ySize = 5;

    public float scale = 1.0f;

    private Vector3 initialPos;

    private GameObject wallHolder;
    private GameObject floorHolder;
    private GameObject topFloorHolder;

    private GameObject mazeHolder;

    public List<Cell> cells;

        
    // Use this for initialization
    void Start ()
    {
        CreateMaze();
        PutADoor();
        surface.BuildNavMesh();
    }


    //Place Wall with probability
    void PlaceAWall(Vector3 position, Quaternion rotation)
    {

        List<float> myLuck = new List<float>();


        int i = 0;
        foreach(float p in probabilityWalls)
        {
            float probabilty = Random.Range(0, 100);
            myLuck.Add(probabilty * probabilityWalls[i]);
            i++;
        }
        i = 0;
        int iWall = 0;
        foreach (float p in myLuck)
        {
            
            if(p > myLuck[iWall])
            {
                iWall = i;
            }
            i++;
        }
        GameObject placedWall = Instantiate(walls[iWall], position, rotation);
        placedWall.transform.parent = wallHolder.transform;
    }

    //Place floor with probability
    void PlaceAFloor(Vector3 position, Quaternion rotation)
    {

        List<float> myLuck = new List<float>();


        int i = 0;
        foreach (float p in probabilityFloors)
        {
            float probabilty = Random.Range(0, 100);
            myLuck.Add(probabilty * probabilityFloors[i]);
            i++;
        }
        i = 0;
        int iFloor = 0;
        foreach (float p in myLuck)
        {

            if (p > myLuck[iFloor])
            {
                iFloor = i;
            }
            i++;
        }
        GameObject placedFloor = Instantiate(floors[iFloor], position, rotation);
        placedFloor.transform.parent = floorHolder.transform;
    }

    //Place floor with probability
    void PlaceATopFloor(Vector3 position, Quaternion rotation)
    {

        GameObject placedFloor = Instantiate(topFloor, position, rotation);
        placedFloor.transform.parent = topFloorHolder.transform;
    }

    //Place Forge and objective on the map
    void PlaceForgeAndObjective()
    {


        //4 forges at each corner
        List<Cell> premierCadran = new List<Cell>();
        for(int j =0; j < ySize / 2; j++)
        {
            for (int i = 0; i < xSize / 2; i++)
            {
                premierCadran.Add(cells[i + j* xSize]);
            }
        }

        List<Cell> deuxiemeCadran = new List<Cell>();
        for (int j = 0; j < ySize / 2; j++)
        {
            for (int i = xSize / 2; i < xSize; i++)
            { 
                deuxiemeCadran.Add(cells[i + j * xSize]);
            }
        }


        List<Cell> troisiemeCadran = new List<Cell>();

        for (int j = ySize / 2; j < ySize; j++)
        {
            for (int i = 0; i < xSize / 2; i++)
            {
                troisiemeCadran.Add(cells[i + j * xSize]);
            }
        }

        List<Cell> quatriemeCadran = new List<Cell>();
        for (int j = ySize / 2; j < ySize; j++)
        {
            for (int i = xSize / 2; i < xSize; i++)
            {
                quatriemeCadran.Add(cells[i + j * xSize]);
            }
        }

        //First quarter

        //Get three cells to put two objectives and a forge
        int[] indexCells = { Random.Range(0, premierCadran.Count), Random.Range(0, premierCadran.Count), Random.Range(0, premierCadran.Count) };

        if (indexCells[0] == indexCells[1] || indexCells[0] == indexCells[2] || indexCells[1] == indexCells[2])
        {
            while (indexCells[0] == indexCells[1] || indexCells[0] == indexCells[2] || indexCells[1] == indexCells[2])
            {
                indexCells[0] = Random.Range(0, premierCadran.Count);
                indexCells[1] = Random.Range(0, premierCadran.Count);
                indexCells[2] = Random.Range(0, premierCadran.Count);
            }
        }

        //Forge
        Cell c = premierCadran[indexCells[0]];
        Vector3 position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(forge, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        //Objectives
        c = premierCadran[indexCells[1]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(objective, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        c = premierCadran[indexCells[2]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(objective, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;


        //Second quarter

        //Get three cells to put two objectives and a forge
        indexCells[0] = Random.Range(0, deuxiemeCadran.Count);
        indexCells[1] = Random.Range(0, deuxiemeCadran.Count);
        indexCells[2] = Random.Range(0, deuxiemeCadran.Count);

        if (indexCells[0] == indexCells[1] || indexCells[0] == indexCells[2] || indexCells[1] == indexCells[2])
        {
            while (indexCells[0] == indexCells[1] || indexCells[0] == indexCells[2] || indexCells[1] == indexCells[2])
            {
                indexCells[0] = Random.Range(0, deuxiemeCadran.Count);
                indexCells[1] = Random.Range(0, deuxiemeCadran.Count);
                indexCells[2] = Random.Range(0, deuxiemeCadran.Count);
            }
        }

        //Forge
        c = deuxiemeCadran[indexCells[0]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(forge, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        //Objectives
        c = deuxiemeCadran[indexCells[1]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(objective, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        c = deuxiemeCadran[indexCells[2]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(objective, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        //Third quarter

        //Get three cells to put two objectives and a forge
        indexCells[0] = Random.Range(0, troisiemeCadran.Count);
        indexCells[1] = Random.Range(0, troisiemeCadran.Count);
        indexCells[2] = Random.Range(0, troisiemeCadran.Count);

        if (indexCells[0] == indexCells[1] || indexCells[0] == indexCells[2] || indexCells[1] == indexCells[2])
        {
            while (indexCells[0] == indexCells[1] || indexCells[0] == indexCells[2] || indexCells[1] == indexCells[2])
            {
                indexCells[0] = Random.Range(0, troisiemeCadran.Count);
                indexCells[1] = Random.Range(0, troisiemeCadran.Count);
                indexCells[2] = Random.Range(0, troisiemeCadran.Count);
            }
        }

        //Forge
        c = troisiemeCadran[indexCells[0]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(forge, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        //Objectives
        c = troisiemeCadran[indexCells[1]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(objective, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        c = troisiemeCadran[indexCells[2]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(objective, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        //Last quarter

        //Get three cells to put two objectives and a forge
        indexCells[0] = Random.Range(0, quatriemeCadran.Count);
        indexCells[1] = Random.Range(0, quatriemeCadran.Count);
        indexCells[2] = Random.Range(0, quatriemeCadran.Count);

        if (indexCells[0] == indexCells[1] || indexCells[0] == indexCells[2] || indexCells[1] == indexCells[2])
        {
            while (indexCells[0] == indexCells[1] || indexCells[0] == indexCells[2] || indexCells[1] == indexCells[2])
            {
                indexCells[0] = Random.Range(0, quatriemeCadran.Count);
                indexCells[1] = Random.Range(0, quatriemeCadran.Count);
                indexCells[2] = Random.Range(0, quatriemeCadran.Count);
            }
        }

        //Forge
        c = quatriemeCadran[indexCells[0]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(forge, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        //Objectives
        c = quatriemeCadran[indexCells[1]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(objective, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

        c = quatriemeCadran[indexCells[2]];
        position = c.floor.transform.position;
        Destroy(c.floor);
        c.floor = Instantiate(objective, position, Quaternion.identity);
        c.floor.transform.localScale = new Vector3(scale, scale, scale);
        c.floor.transform.parent = floorHolder.transform;

    }

    //Create Maze with the direction 
    void CreateMaze()
    {
        mazeHolder = new GameObject();


        //Set the scale walls
        foreach(GameObject g in walls)
        {
            g.transform.localScale = new Vector3(scale, scale, scale);
        }

        //Set the scale floors
        foreach (GameObject g in floors)
        {
            g.transform.localScale = new Vector3(scale, scale, scale);
        }

        CreateFloor();
        CreateTopFloor();
        CreateWalls();
        RemoveWalls();
        PlaceForgeAndObjective();

        surface.BuildNavMesh();
        mazeHolder.name = "Maze";




    }


    //Create floor at first
    void CreateFloor()
    {
        floorHolder = new GameObject();

        floorHolder.name = "Floor";
        floorHolder.transform.parent = mazeHolder.transform;


        initialPos = new Vector3((-xSize / 2), -scale / 2, (-ySize / 2) - scale / 2);
        Vector3 myPos = initialPos;

        for (int iZ = 0; iZ < ySize; iZ++)
        {
            for (int iX = 0; iX < xSize; iX++)
            {
                myPos = new Vector3(initialPos.x + (iX * scale), -scale/2, initialPos.z + (iZ * scale) + scale);
                PlaceAFloor(myPos, Quaternion.identity);
            }
        }
    }

    //Create floor at first
    void CreateTopFloor()
    {
        topFloorHolder = new GameObject();

        topFloorHolder.name = "Top-Floor";
        topFloorHolder.transform.parent = mazeHolder.transform;


        initialPos = new Vector3((-xSize / 2), -scale / 2, (-ySize / 2) - scale / 2);
        Vector3 myPos = initialPos;

        for (int iZ = 0; iZ < ySize; iZ++)
        {
            for (int iX = 0; iX < xSize; iX++)
            {
                myPos = new Vector3(initialPos.x + (iX * scale) + scale / 2, scale / 2, initialPos.z + (iZ * scale) + scale / 2);
                PlaceATopFloor(myPos, Quaternion.Euler(180.0f, 0.0f, 0.0f));
            }
        }
    }
    //Create Walls before creating the maze
    void CreateWalls()
    {
        wallHolder = new GameObject();
        wallHolder.name = "Wall";
        wallHolder.transform.parent = mazeHolder.transform;

        initialPos = new Vector3((-xSize / 2) + scale / 2, 0.0f, (-ySize / 2) + scale / 2);
        Vector3 myPos = initialPos;


        //First axis (x)
        for(int iZ = 0; iZ < ySize; iZ++)
        {
            for(int iX = 0; iX <= xSize; iX++)
            {
                myPos = new Vector3(initialPos.x + (iX * scale) - scale / 2, 0.0f, initialPos.z + (iZ * scale) - scale / 2);
                PlaceAWall(myPos, Quaternion.identity);
            }
        }

        //Second axis (z)
        for (int iZ = 0; iZ <= ySize; iZ++)
        {
            for (int iX = 0; iX < xSize; iX++)
            {
                myPos = new Vector3(initialPos.x + (iX * scale), 0.0f, initialPos.z + (iZ * scale) - scale);
                PlaceAWall(myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f));
            }
        }
        CreateCells();
    }

    //Create Cells
    void CreateCells()
    {
        
        int childrensW = wallHolder.transform.childCount;
        GameObject[] walls = new GameObject[childrensW];

        int childrensF = floorHolder.transform.childCount;
        GameObject[] floors = new GameObject[childrensF];

        cells = new List<Cell>();

        
        //Gets all the children wall
        for (int iChild = 0; iChild < childrensW; iChild++)
        {
            walls[iChild] = wallHolder.transform.GetChild(iChild).gameObject;
        }

        //Gets all the children floor
        for (int iChild = 0; iChild < childrensF; iChild++)
        {
            floors[iChild] = floorHolder.transform.GetChild(iChild).gameObject;
        }


        //Walls for each cells
        for (int iCell = 0; iCell < xSize * ySize; iCell++)
        {
            Cell aCell = new Cell();

            aCell.floor = floors[iCell];
            aCell.id = iCell;
            aCell.west = walls[iCell + (iCell / xSize)];
            aCell.east = walls[iCell + (iCell / xSize) + 1];
            aCell.south = walls[iCell + (xSize + 1) * ySize];
            aCell.north = walls[iCell + (xSize + 1) * ySize + xSize];
            cells.Add(aCell);
        }
        
    }

    //Remove a Wall
    bool RemoveAWall(int iCell)
    {
        List<int> lpossibility = new List<int>();
        //South
        if (iCell > xSize && cells[iCell].south != null && cells[iCell].id != cells[iCell - xSize].id)
        {
            lpossibility.Add(0);
        }
        //North
        if (iCell < xSize * ySize - xSize - 1 && cells[iCell].north != null && cells[iCell].id != cells[iCell + xSize].id)
        {
            lpossibility.Add(1);
        }
        //East
        if ((iCell + 1)  % xSize != 0 && cells[iCell].east != null && cells[iCell].id != cells[iCell + 1].id)
        {
            lpossibility.Add(2);
        }
        //West
        if (iCell % xSize > 0 && cells[iCell].west != null && cells[iCell].id != cells[iCell - 1].id)
        {
            lpossibility.Add(3);
        }
        if (lpossibility.Count > 0)
        {
            int side = Random.Range(0, lpossibility.Count);
            switch (lpossibility[side])
            {
                //South
                case 0:
                    Destroy(cells[iCell].south);
                    ReplaceID(cells[iCell - xSize].id, cells[iCell].id);
                    break;

                //North
                case 1:
                    Destroy(cells[iCell].north);
                    ReplaceID(cells[iCell + xSize].id, cells[iCell].id);
                    break;
                //East
                case 2:
                    Destroy(cells[iCell].east);
                    ReplaceID(cells[iCell + 1].id, cells[iCell].id);
                    break;

                //West
                case 3:
                    Destroy(cells[iCell].west);
                    ReplaceID(cells[iCell - 1].id, cells[iCell].id);
                    break;


            }
            return true;
        }
        return false;
    }

    //Remove Walls to create the maze
    void RemoveWalls()
    {
        int iWallRemove = 0;
        int iCell;
        while (iWallRemove < xSize * ySize - 1)
        {
            iCell = Random.Range(0, xSize * ySize);
            if (RemoveAWall(iCell))
            {
                iWallRemove++;
            }
        }
    }


    //Replace the id of a cell 
    void ReplaceID(int idToRemove, int idToReplace)
    {
        //Replace all  id cell link to the id 
        foreach (Cell c in cells)
        {
            if (c.id == idToRemove)
            {
                c.id = idToReplace;
            }
        }
        
    }

    //Remove a wall to "Create a door"
    void PutADoor()
    {
       int positionCell = (xSize) * (ySize / 2) -1 ;
       print("Wall remove from Cell # " + positionCell);
       Destroy(cells[positionCell].east);
       Vector3 distance = stairs.position - cells[positionCell].floor.transform.position;
       mazeHolder.transform.position = mazeHolder.transform.position + distance;


    }

	// Update is called once per frame
	void Update ()
    {
  
    }
}
