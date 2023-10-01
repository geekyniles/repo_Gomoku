namespace Gomoku.Models
{
    public class PlayerModel
    {
        public string? Name { get; set; }
        public string? StoneMark { get; set; }

        public PlayerModel(string name, string stoneMark)
        {
            this.Name = name;
            this.StoneMark = stoneMark;
        }
    }
}
