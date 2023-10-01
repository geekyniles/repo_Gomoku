using Gomoku.Models;

namespace Gomoku.Objects
{
    public interface IGomoku
    {
        public string PlaceAStone(PlaceAStoneRequestModel request);
        public string DrawBoard();
    }
}
