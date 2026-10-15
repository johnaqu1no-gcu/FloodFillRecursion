/*
 * John Aquino
 * CST-250
 * 10/12/26
 * Flood Fill Recursion
 * Activity 3
 */

namespace FloodFillRecursion.Models
{
    internal class BoardModel
    {
        // BoardModel Properties
        public int Size { get; set; }
        public CellModel[,] Grid { get; set; }
        public int NumShapes { get; set; }

        /// <summary>
        /// Parameterized constructor for BoardModel
        /// </summary>
        /// <param name="size"></param>
        /// <param name="numShapes"></param>
        public BoardModel(int size, int numShapes)
        {
            Size = size;
            NumShapes = numShapes;
            Grid = new CellModel[Size, Size];

            // Set up the Grid
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    Grid[row, col] = new CellModel(row, col, "E");
                }
            }

            PlaceShapes();
            PlaceSplitWall();
        }

        /// <summary>
        /// Place random square shapes on the board as walls
        /// </summary>
        private void PlaceShapes()
        {
            Random random = new Random();
            int shapeSize = Size / 2;
            int row = 0;
            int col = 0;

            // Create three shapes
            for (int shapes = 0; shapes < NumShapes; shapes++)
            {
                // Generate the row and col for the
                // top left corner of the square
                row = random.Next(0, Size - shapeSize + 1);
                col = random.Next(0, Size - shapeSize + 1);

                for (int offset = 0; offset < shapeSize; offset++)
                {
                    // Top Wall
                    Grid[row, col + offset].Contents = "W";
                    // Bottom Wall
                    Grid[row + shapeSize - 1, col + offset].Contents = "W";
                    // Left Wall
                    Grid[row + offset, col].Contents = "W";
                    // Right Wall
                    Grid[row + offset, col + shapeSize - 1].Contents = "W";
                }
            }
        }

        /// <summary>
        /// Build a wall that splits the board in two, with one diagonal step
        /// in the middle of it
        /// </summary>
        private void PlaceSplitWall()
        {
            // The wall runs straight down one column for the top half, then
            // jumps one column over for the bottom half. That one-cell jump is
            // the whole point: the two sides end up touching only at a corner,
            // so four-way fill can never cross but eight-way fill can.
            int middle = Size / 2;

            // Top half of the wall
            for (int row = 0; row < middle; row++)
            {
                Grid[row, middle].Contents = "W";
            }

            // Bottom half, shifted one column to the right
            for (int row = middle; row < Size; row++)
            {
                Grid[row, middle + 1].Contents = "W";
            }

            // Clear an open lane down each side of the wall. Without these the
            // random squares can seal the corner off and the two fills come
            // out identical, which would prove nothing.
            for (int row = 0; row < Size; row++)
            {
                Grid[row, middle - 1].Contents = "E";
                Grid[row, middle + 2].Contents = "E";
            }

            // Open the top row too, so wherever the user starts along the top
            // they can always reach the lane on their side.
            for (int col = 0; col < Size; col++)
            {
                if (col != middle)
                {
                    Grid[0, col].Contents = "E";
                }
            }

            // The two cells either side of the step. They are diagonal
            // neighbors of each other, and this is the only way across.
            Grid[middle - 1, middle + 1].Contents = "E";
            Grid[middle, middle].Contents = "E";
        }
    }
}
