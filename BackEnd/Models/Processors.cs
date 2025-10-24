namespace BackEnd.Models
{
    public class Processors : Product
    {
        public string Model { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
        public float BaseClockGHz { get; set; }
        public float BoostClockGHz { get; set; }
        public string SocketType { get; set; }
        public int TDP { get; set; }
        public bool IntegratedGraphics { get; set; }
        public string Architecture { get; set; }
        public string Lithography { get; set; }


    }
}
