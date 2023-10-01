using Gomoku.Objects;
using Gomoku.Utilities;
using Gomoku.Models;

namespace Gomoku.Tests
{
    [TestFixture]
    public class Tests
    {
        IGomoku gomoku;
        private string _player1Name;
        private string _player2Name;

        [SetUp]
        public void Setup()
        {
            gomoku = Objects.Gomoku.Instance;
            _player1Name = Constants.PLAYER1_NAME;
            _player2Name = Constants.PLAYER2_NAME;
        }

        [Test]
        public void When_NonWinningValidTurn_Expect_PlayerTurnFinished()
        {
            // Arrange
            var requestModel = new PlaceAStoneRequestModel();
            requestModel.Player = _player1Name;
            requestModel.StoneRow = 10;
            requestModel.StoneColumn = 11;
            var expectedResult = string.Format(Constants.RESULT_PLAYER_TURN_FINISHED, _player1Name);

            // Act
            var actualResult = gomoku.PlaceAStone(requestModel);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void When_WinningValidTurnHorizontal_Expect_PlayerWins()
        {
            // Arrange
            var expectedResult = string.Format(Constants.RESULT_PLAYER_WINS, _player1Name);
            string actualResult = string.Empty;

            // Act
            for (int i = 0; i < 5; i++)
            {
                actualResult = gomoku.PlaceAStone(new PlaceAStoneRequestModel()
                               {
                                   Player = _player1Name,
                                   StoneRow = 0,
                                   StoneColumn = i
                               });
            }

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void When_WinningValidTurnVertical_Expect_PlayerWins()
        {
            // Arrange
            var expectedResult = string.Format(Constants.RESULT_PLAYER_WINS, _player1Name);
            string actualResult = string.Empty;

            // Act
            for (int i = 0; i < 5; i++)
            {
                actualResult = gomoku.PlaceAStone(new PlaceAStoneRequestModel()
                {
                    Player = _player1Name,
                    StoneRow = i,
                    StoneColumn = 0
                });
            }

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void When_WinningValidTurnLeftDiagonal_Expect_PlayerWins()
        {
            // Arrange
            int startingXCoordinate = 2, startingYCoordinate = 0;
            var expectedResult = string.Format(Constants.RESULT_PLAYER_WINS, _player1Name);
            string actualResult = string.Empty;

            // Act
            for (int i = 0; i < 5; i++)
            {
                actualResult = gomoku.PlaceAStone(new PlaceAStoneRequestModel()
                {
                    Player = _player1Name,
                    StoneRow = startingXCoordinate + i,
                    StoneColumn = startingYCoordinate + i
                });
            }

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void When_WinningValidTurnRightDiagonal_Expect_PlayerWins()
        {
            // Arrange
            int startingXCoordinate = 1, startingYCoordinate = 5;
            var expectedResult = string.Format(Constants.RESULT_PLAYER_WINS, _player1Name);
            string actualResult = string.Empty;

            // Act
            for (int i = 0; i < 5; i++)
            {
                actualResult = gomoku.PlaceAStone(new PlaceAStoneRequestModel()
                {
                    Player = _player1Name,
                    StoneRow = startingXCoordinate + i,
                    StoneColumn = startingYCoordinate - i
                });
            }

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void When_CoordinateOutOfBounds_Expect_InvalidPlayerTurn()
        {
            // Arrange
            var requestModel = new PlaceAStoneRequestModel();
            requestModel.Player = _player1Name;
            requestModel.StoneRow = 15;
            requestModel.StoneColumn = 15;
            var expectedResult = string.Format(Constants.RESULT_PLAYER_TURN_INVALID, _player1Name);

            // Act
            var actualResult = gomoku.PlaceAStone(requestModel);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void When_CoordinateAlreadyOccupiedByAStone_Expect_InvalidPlayerTurn()
        {
            // Arrange
            var requestModelPlayer1 = new PlaceAStoneRequestModel();
            requestModelPlayer1.Player = _player1Name;
            requestModelPlayer1.StoneRow = 5;
            requestModelPlayer1.StoneColumn = 5;

            var requestModelPlayer2 = new PlaceAStoneRequestModel();
            requestModelPlayer2.Player = _player2Name;
            requestModelPlayer2.StoneRow = 5;
            requestModelPlayer2.StoneColumn = 5;

            var expectedResultPlayer1 = string.Format(Constants.RESULT_PLAYER_TURN_FINISHED, _player1Name);
            var expectedResultPlayer2 = string.Format(Constants.RESULT_PLAYER_TURN_INVALID, _player2Name);
            string actualResultPlayer1, actualResultPlayer2;

            // Act
            actualResultPlayer1 = gomoku.PlaceAStone(requestModelPlayer1);
            actualResultPlayer2 = gomoku.PlaceAStone(requestModelPlayer2);

            // Assert
            Assert.That(actualResultPlayer1, Is.EqualTo(expectedResultPlayer1));
            Assert.That(actualResultPlayer2, Is.EqualTo(expectedResultPlayer2));
        }

        [Test]
        public void When_PlayerNotRecognized_Expect_InvalidPlayerTurn()
        {
            // Arrange
            var player3Name = "Player3";
            var requestModel = new PlaceAStoneRequestModel();
            requestModel.Player = player3Name;
            requestModel.StoneRow = 11;
            requestModel.StoneColumn = 11;
            var expectedResult = string.Format(Constants.RESULT_PLAYER_TURN_INVALID, player3Name);

            // Act
            var actualResult = gomoku.PlaceAStone(requestModel);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }
    }
}