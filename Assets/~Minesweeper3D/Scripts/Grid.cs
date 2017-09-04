using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper3D
{
    public class Grid : MonoBehaviour
    {
        public GameObject blockPrefab;
        //The grid's dimensions
        public int width = 10;
        public int height = 10;
        public int depth = 10;
        public float spacing = 1.2f; //How much spacing between each block

        //Multi-Dimensional array storing the blocks (inn this case 3D)
        private Block[,,] blocks;

        void Start()
        {
            //Generate blocks on startup
            GenerateBlocks();
        }

        void Update()
        {
            RemoveBlock();
        }

        void OnDrawGizmos()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Gizmos.DrawRay(ray.origin, ray.direction * 100);
        }
        void RemoveBlock()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 100);

                if (Physics.Raycast(ray, out hit))
                {
                    Block block = hit.collider.gameObject.GetComponent<Block>();
                    if (block != null)
                    {
                        SelectBlock(block);
                    }
                }

            }
        }

        //Spawns a block at position and returns the block component
        Block SpawnBlock(Vector3 pos)
        {
            //Instantiate clone
            GameObject clone = Instantiate(blockPrefab);
            //Set position
            clone.transform.position = pos;
            //Get block component
            Block currentBlock = clone.GetComponent<Block>();
            //Return it
            return currentBlock;
        }

        //Spawns blocks in a grid-like fashion
        void GenerateBlocks()
        {
            //Create a 3D array to store all the blocks
            blocks = new Block[width, height, depth];

            //Loop through the X, Y and Z axis of the 3D array
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        //Calculate half size using array dimensions
                        Vector3 halfSize = new Vector3(width / 2, height / 2, depth / 2);
                        //Make sure to offset by half(so that elements are centered)
                        halfSize -= new Vector3(.5f, .5f, .5f);
                        //Create poosition for element to pivot around grid zero
                        Vector3 pos = new Vector3(x - halfSize.x, y - halfSize.y, z - halfSize.z);
                        //Apply spacing
                        pos *= spacing;
                        //Spawn the block at the position
                        Block block = SpawnBlock(pos);
                        //Attach block to grid as a child
                        block.transform.SetParent(transform);
                        //Store array coordinate inside the blbock itself
                        block.x = x;
                        block.y = y;
                        block.z = z;
                        //Store block in the array at coordinates
                        blocks[x, y, z] = block;
                    }
                }
            }
        }

        //Count adjacent mines at element
        public int GetAdjacentMineCountAt(Block b)
        {
            int count = 0;
            //Loop through all element and have each axis go between -1 to 1
            for (int x = -1; x <= 1; x++)
            {
                //Calculate adjacent element's index
                int desiredX = b.x + x;

                for (int y = -1; y <= 1; y++)
                {
                    //Calculate adjacent element's index
                    int desiredY = b.y + y;

                    for (int z = -1; z <= 1; z++)
                    {
                        //Calculate adjacent element's index
                        int desiredZ = b.z + z;

                        if (desiredX >= 0 && desiredX < width && desiredY >= 0 && desiredY < height && desiredZ >= 0 && desiredZ < depth)
                        {
                            //If the element at index is a mine
                            if (blocks[desiredX, desiredY, desiredZ].isMine)
                            {
                                //Increment count by 1
                                count++;
                            }
                        }


                    }
                }
            }
            return count;
        }

        // Flood fill function to uncover all the empty elements
        public void FFuncover(int x, int y, int z, bool[,,] visited)
        {
            // Coordinates in range?
            if (x >= 0 && y >= 0 && z >= 0 && x < width && y < height && z < depth)
            {
                // Visited already?
                if (visited[x, y, z]) return;

                // Uncover element
                Block block = blocks[x, y, z];
                int adjacentMines = GetAdjacentMineCountAt(block);
                block.Reveal(adjacentMines);

                // Close to a mine?
                if (adjacentMines > 0) return;

                // Set visited flag
                visited[x, y, z] = true;

                // Perform recursion in each axis to detect adjacent elements
                FFuncover(x - 1, y, z - 1, visited);
                FFuncover(x + 1, y, z - 1, visited);
                FFuncover(x, y - 1, z - 1, visited);
                FFuncover(x, y + 1, z - 1, visited);
                FFuncover(x, y, z - 1, visited);
                FFuncover(x - 1, y, z, visited);
                FFuncover(x + 1, y, z, visited);
                FFuncover(x, y - 1, z, visited);
                FFuncover(x, y + 1, z, visited);
                FFuncover(x - 1, y, z + 1, visited);
                FFuncover(x + 1, y, z + 1, visited);
                FFuncover(x, y - 1, z + 1, visited);
                FFuncover(x, y + 1, z + 1, visited);
                FFuncover(x, y, z + 1, visited);
            }
        }

        // Uncovers all mines that are in the grid
        public void UncoverMines()
        {
            //Loop through all elemenets in array
            foreach (Block block in blocks)
            {
                //If block is mine; reveal
                if (block.isMine)
                {
                    block.Reveal(GetAdjacentMineCountAt(block));
                }
            }
        }

        // Takes in a block selected by the user in some way to reveal it
        public void SelectBlock(Block selectedBlock)
        {
            int selectedAdjacentMinesCount = GetAdjacentMineCountAt(selectedBlock);
            // Reveal the selected block
            selectedBlock.Reveal(selectedAdjacentMinesCount);

            // If the selected block is a mine
            if (selectedBlock.isMine)
            {
                // Uncover all other mines
                UncoverMines();
            }
            else if (selectedAdjacentMinesCount <= 0)
            {
                // Perform flood fill to reveal all empty blocks
                FFuncover(selectedBlock.x, selectedBlock.y, selectedBlock.z, new bool[width, height, depth]);
            }
        }
    }
}