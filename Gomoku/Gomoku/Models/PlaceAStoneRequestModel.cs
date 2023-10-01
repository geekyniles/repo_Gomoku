namespace Gomoku.Models
{
    public class PlaceAStoneRequestModel
    {
        public string? Player { get; set; }
        public int StoneRow { get; set; }
        public int StoneColumn { get; set;}
    }
}
