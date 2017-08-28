using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper3D
{
    [RequireComponent(typeof(Renderer))]
    public class Block : MonoBehaviour
    {
        public int x, y, z;
        public bool isMine;
        public float mineChance = .05f;
        [Header("References")]
        public Color[] textColours;
        public TextMesh textElement;
        public Transform mine;

        private bool isRevealed = false;
        private Renderer rend;

        void Awake()
        {
            rend = GetComponent<Renderer>();
        }

        // Use this for initialization
        void Start()
        {
            //Detatch element from block
            textElement.transform.SetParent(null);
            //Randomly decide if mine or not
            isMine = Random.value < mineChance;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void UpdateText(int adjacentMines)
        {
            //Are there adjacent mines?
            if (adjacentMines > 0)
            {
                //Set tet to amount of mines
                textElement.text = adjacentMines.ToString();

                //Check if adjacentMines are within textColor's array
                if (adjacentMines >= 0 && adjacentMines < textColours.Length)
                {
                    //Set text color to whatever was preset
                    textElement.color = textColours[adjacentMines];
                }
            }
        }

        public void Reveal(int adjacentMines)
        {
			//Flags the block as being revealed
			isRevealed = true;
			//Checks if block is a mine
			if (isMine)
			{
				//Activate the references mine
				mine.gameObject.SetActive(true);
				//Detatch mine from children
				mine.SetParent(null);
			} else
			{
				//Updates the text to display adjacentMines
				UpdateText(adjacentMines);
			}
			//Deactivates the block
			gameObject.SetActive(false);
        }
    }
}
