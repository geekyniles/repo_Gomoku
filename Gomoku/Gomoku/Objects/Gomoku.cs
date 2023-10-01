using Gomoku.Models;
using Gomoku.Utilities;

namespace Gomoku.Objects
{
    public sealed class Gomoku : IGomoku
    {
        private static Gomoku? _instance = null;
        private string[,]? _board;
        private List<PlayerModel>? _players;

        /// <summary>
        /// Getter for the Gomoku singleton instance
        /// </summary>
        public static Gomoku Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Gomoku();
                }
                return _instance;
            }
        }

        private Gomoku()
        {
            _board = new string[Constants.BOARD_HEIGHT_WIDTH, Constants.BOARD_HEIGHT_WIDTH];

            _players = new List<PlayerModel>();
            var player1 = new PlayerModel(Constants.PLAYER1_NAME, Constants.PLAYER1_STONEMARK);
            var player2 = new PlayerModel(Constants.PLAYER2_NAME, Constants.PLAYER2_STONEMARK);

            _players.Add(player1);
            _players.Add(player2);
        }

        /// <summary>
        /// Places the stone on a target coordinate by the current player
        /// </summary>
        /// <param name="request">Current player and target coordinates</param>
        /// <returns>Result of this turn</returns>
        public string PlaceAStone(PlaceAStoneRequestModel request)
        {
            string result = string.Empty;

            if (!IsValidTurn(request, ref result)) { return result; }

            var currPlayer = _players?.Where(x => x.Name == request.Player).FirstOrDefault();
            // Place the stone on the coordinates
            _board![request.StoneRow, request.StoneColumn] = currPlayer?.StoneMark!;

            if (DoesCurrentPlayerWin(request.StoneRow, request.StoneColumn, currPlayer?.StoneMark!))
            {
                // Reset the board for next round
                _board = new string[Constants.BOARD_HEIGHT_WIDTH, Constants.BOARD_HEIGHT_WIDTH];
                return string.Format(Constants.RESULT_PLAYER_WINS, currPlayer?.Name);
            }

            return string.Format(Constants.RESULT_PLAYER_TURN_FINISHED, currPlayer?.Name);
        }

        /// <summary>
        /// Displays the whole board including the stones placed
        /// </summary>
        /// <returns>string containing the displayed board</returns>
        public string DrawBoard()
        {
            var boardStatus = string.Empty;

            for (int i = 0; i < Constants.BOARD_HEIGHT_WIDTH; i++)
            {
                for (int j = 0; j < Constants.BOARD_HEIGHT_WIDTH; j++)
                {
                    boardStatus += string.IsNullOrEmpty(_board?[i, j]) ? "- " : _board?[i, j] + " ";
                }
                boardStatus += "\n";
            }

            return boardStatus;
        }

        private bool IsValidTurn(PlaceAStoneRequestModel request, ref string result)
        {
            // 1. Check if coordinates are valid
            // 2. Check if stone is already existing on the target coordinate
            // 3. Check if unknown player is playing
            if ((request.StoneRow >= Constants.BOARD_HEIGHT_WIDTH || request.StoneColumn >= Constants.BOARD_HEIGHT_WIDTH)
               || (!string.IsNullOrEmpty(_board![request.StoneRow, request.StoneColumn]))
               || (_players?.Where(x => x.Name == request.Player).FirstOrDefault() == null))
            {
                result = string.Format(Constants.RESULT_PLAYER_TURN_INVALID, request.Player);
                return false;
            }

            return true;
        }

        private bool DoesCurrentPlayerWin(int stoneRow, int stoneColumn, string stoneMark)
        {
            if (IsThere5ConsecutiveStonesInRow(stoneRow, stoneMark)) { return true; }
            if (IsThere5ConsecutiveStonesInColumn(stoneColumn, stoneMark)) { return true; }
            if (IsThere5ConsecutiveStonesInLeftDiagonal(stoneRow, stoneColumn, stoneMark)) { return true; }
            if (IsThere5ConsecutiveStonesInRightDiagonal(stoneRow, stoneColumn, stoneMark)) { return true; }

            return false;
        }

        private bool IsThere5ConsecutiveStonesInRow(int stoneRow, string stoneMark)
        {
            int consecutiveStonesCounter = 0;

            for (int i = 0; i < Constants.BOARD_HEIGHT_WIDTH; i++)
            {
                if (string.Compare(_board![stoneRow, i], stoneMark) == 0)
                {
                    consecutiveStonesCounter++;
                    if (consecutiveStonesCounter == Constants.NUMBER_OF_CONSECUTIVE_STONES_TO_WIN)
                    { return true; }
                }
                else { consecutiveStonesCounter = 0; }
            }

            return false;
        }

        private bool IsThere5ConsecutiveStonesInColumn(int stoneColumn, string stoneMark)
        {
            int consecutiveStonesCounter = 0;

            for (int i = 0; i < Constants.BOARD_HEIGHT_WIDTH; i++)
            {
                if (string.Compare(_board![i, stoneColumn], stoneMark) == 0)
                {
                    consecutiveStonesCounter++;
                    if (consecutiveStonesCounter == Constants.NUMBER_OF_CONSECUTIVE_STONES_TO_WIN)
                    { return true; }
                }
                else { consecutiveStonesCounter = 0; }
            }

            return false;
        }

        private bool IsThere5ConsecutiveStonesInLeftDiagonal(int stoneRow, int stoneColumn, string stoneMark)
        {
            int consecutiveStonesCounter = 0, xCoor, yCoor;
            int diff = stoneRow - stoneColumn;

            // Get the top coordinate of the diagonal to start from
            if (diff >= 0) { xCoor = diff; yCoor = 0; }
            else { xCoor = 0; yCoor = Math.Abs(diff); }

            // Iterate diagonally
            while (xCoor < Constants.BOARD_HEIGHT_WIDTH && yCoor < Constants.BOARD_HEIGHT_WIDTH)
            {
                if (string.Compare(_board![xCoor, yCoor], stoneMark) == 0)
                {
                    consecutiveStonesCounter++;
                    if (consecutiveStonesCounter == Constants.NUMBER_OF_CONSECUTIVE_STONES_TO_WIN)
                    {
                        return true;
                    }
                }
                else { consecutiveStonesCounter = 0; }

                xCoor++; yCoor++;
            }

            return false;
        }

        private bool IsThere5ConsecutiveStonesInRightDiagonal(int stoneRow, int stoneColumn, string stoneMark)
        {
            int consecutiveStonesCounter = 0, xCoor, yCoor;
            int sum = stoneRow + stoneColumn;

            // Get the top coordinate of the diagonal to start from
            if (sum > Constants.BOARD_HEIGHT_WIDTH - 1)
            {
                xCoor = sum - Constants.BOARD_HEIGHT_WIDTH - 1;
                yCoor = Constants.BOARD_HEIGHT_WIDTH - 1;
            }
            else { xCoor = 0; yCoor = sum; }

            // Iterate diagonally
            while (xCoor < Constants.BOARD_HEIGHT_WIDTH && yCoor > 0)
            {
                if (string.Compare(_board![xCoor, yCoor], stoneMark) == 0)
                {
                    consecutiveStonesCounter++;
                    if (consecutiveStonesCounter == Constants.NUMBER_OF_CONSECUTIVE_STONES_TO_WIN)
                    {
                        return true;
                    }
                }
                else { consecutiveStonesCounter = 0; }

                xCoor++; yCoor--;
            }

            return false;
        }

    }
}
