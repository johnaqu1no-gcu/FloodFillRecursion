/*
 * John Aquino
 * CST-250
 * 10/12/26
 * Flood Fill Recursion
 * Activity 3
 */

using FloodFillRecursion.Models;

// Declare and initialize
// Create a new BoardModel
BoardModel board = new BoardModel(20, 3);
int startRow = -1, startCol = -1;

// Print the board to the console
Utility.PrintBoard(board);

// Prompt the user for the starting row (1 - 20)
Console.Write("Enter the row to start the flood fill at: ");
// Remove 1 from the input to get 0-19 range for row
startRow = Utility.ReadIntFromConsole() - 1;

// Prompt the user for the starting column (1 - 20)
Console.Write("Enter the column to start the flood fill at: ");
// Remove 1 from the input to get 0-19 range for col
startCol = Utility.ReadIntFromConsole() - 1;

// Call the four-way flood fill method using the board
board = Utility.FloodFill(board, startRow, startCol);

// Print the new board
Console.WriteLine();
Console.WriteLine("Four-way flood fill:");
Utility.PrintBoard(board);
int fourWayCount = Utility.CountFilled(board);
Console.WriteLine("Cells filled: " + fourWayCount);

// Wipe the fill off the same board so the eight-way run starts from the
// exact same walls. Running it on a brand new board would give a different
// random layout and the two pictures would not be comparable.
Utility.ResetFill(board);

// Call the eight-way flood fill method from the same starting cell
board = Utility.FloodFillEightWay(board, startRow, startCol);

// Print the new board
Console.WriteLine();
Console.WriteLine("Eight-way flood fill:");
Utility.PrintBoard(board);
int eightWayCount = Utility.CountFilled(board);
Console.WriteLine("Cells filled: " + eightWayCount);

// The difference is the evidence the activity asks for. Anything above zero
// means the diagonals got into a region the four-way fill could not reach.
Console.WriteLine();
Console.WriteLine("Eight-way reached " + (eightWayCount - fourWayCount) + " more cells than four-way.");

static class Utility
{
    /// <summary>
    /// Print the board to the console
    /// </summary>
    /// <param name="board"></param>
    public static void PrintBoard(BoardModel board)
    {
        // Make sure the color of the column numbers is white
        Console.ForegroundColor = ConsoleColor.White;
        // Start the column numbers row with spaces to keep the numbers aligned
        Console.Write("  ");

        // Loop to add column numbers for the board
        for (int colNum = 0; colNum < board.Size; colNum++)
        {
            // Print the colNum with a 2-character width
            Console.Write($" {colNum + 1,2}");
        }

        Console.WriteLine();

        // Loop through the rows of the board
        for (int row = 0; row < board.Size; row++)
        {
            // Print each row number
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{row + 1,2}");

            // Loop through the columns of the board
            for (int col = 0; col < board.Size; col++)
            {
                // Check if the current cell is a wall
                if (board.Grid[row, col].Contents == "W")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(" W ");
                }
                else if (board.Grid[row, col].Contents == "E")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" . ");
                }
                else if (board.Grid[row, col].Contents == "F")
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" ~ ");
                }
                else
                {
                    Console.Write("   ");
                }
            }

            // Use a WriteLine to start a new row
            Console.WriteLine();
        }

        Console.ForegroundColor = ConsoleColor.White;
    }

    /// <summary>
    /// Perform a flood fill algorithm on the given row and col
    /// </summary>
    /// <param name="board"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    internal static BoardModel FloodFill(BoardModel board, int row, int col)
    {
        // Check if the cell is on the board
        if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
        {
            // If the cell is not on the board, end the method
            return board;
        }

        // If the cell is a wall, end the method
        if (board.Grid[row, col].Contents == "W")
        {
            return board;
        }
        // If the cell has already been filled, end the method
        else if (board.Grid[row, col].Contents == "F")
        {
            return board;
        }
        // Else, fill the cell
        else
        {
            board.Grid[row, col].Contents = "F";
        }

        // Call the flood fill method to the north
        board = FloodFill(board, row - 1, col);

        // Call the flood fill method to the east
        board = FloodFill(board, row, col + 1);

        // Call the flood fill method to the south
        board = FloodFill(board, row + 1, col);

        // Call the flood fill method to the west
        board = FloodFill(board, row, col - 1);

        return board;
    } 

    /// <summary>
    /// Perform an eight-way flood fill, which adds the four diagonals
    /// </summary>
    /// <param name="board"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    internal static BoardModel FloodFillEightWay(BoardModel board, int row, int col)
    {
        // Same three base cases as the four-way version. Nothing about the
        // stopping conditions changes when the diagonals get added.
        if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
        {
            return board;
        }

        if (board.Grid[row, col].Contents == "W")
        {
            return board;
        }
        else if (board.Grid[row, col].Contents == "F")
        {
            return board;
        }
        else
        {
            board.Grid[row, col].Contents = "F";
        }

        // Call the flood fill method to the north
        board = FloodFillEightWay(board, row - 1, col);

        // Call the flood fill method to the east
        board = FloodFillEightWay(board, row, col + 1);

        // Call the flood fill method to the south
        board = FloodFillEightWay(board, row + 1, col);

        // Call the flood fill method to the west
        board = FloodFillEightWay(board, row, col - 1);

        // Call the flood fill method to the northwest
        board = FloodFillEightWay(board, row - 1, col - 1);

        // Call the flood fill method to the northeast
        board = FloodFillEightWay(board, row - 1, col + 1);

        // Call the flood fill method to the southwest
        board = FloodFillEightWay(board, row + 1, col - 1);

        // Call the flood fill method to the southeast
        board = FloodFillEightWay(board, row + 1, col + 1);

        return board;
    }

    /// <summary>
    /// Turn every filled cell back to empty so the same board can be
    /// filled a second time
    /// </summary>
    /// <param name="board"></param>
    internal static void ResetFill(BoardModel board)
    {
        for (int row = 0; row < board.Size; row++)
        {
            for (int col = 0; col < board.Size; col++)
            {
                // Walls stay put, only the fill gets wiped
                if (board.Grid[row, col].Contents == "F")
                {
                    board.Grid[row, col].Contents = "E";
                }
            }
        }
    }

    /// <summary>
    /// Count how many cells on the board are filled
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    internal static int CountFilled(BoardModel board)
    {
        int count = 0;

        for (int row = 0; row < board.Size; row++)
        {
            for (int col = 0; col < board.Size; col++)
            {
                if (board.Grid[row, col].Contents == "F")
                {
                    count++;
                }
            }
        }

        return count;
    }

    /// <summary>
    /// Read an integer number from the console
    /// </summary>
    /// <returns></returns>
    internal static int ReadIntFromConsole()
    {
        // Declare and initialize
        int num = -1;

        // Check if the current input is valid
        while (!int.TryParse(Console.ReadLine(), out num))
        {
            // Inform the user of invalid input and prompt the user again
            Console.Write("Invalid input. Please enter an integer: ");
        }

        return num;
    } 
}
