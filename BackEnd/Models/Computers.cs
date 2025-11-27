using Microsoft.Identity.Client;

namespace BackEnd.Models
{
    public class Computers : Product
    {
        public string  Cpu { get; set; } 

        public string Disk { get; set; }
        
        public string Gpu {  get; set; }
        
        public string TotalRam { get; set; }

        public string MemType { get; set; }

        public string Psu { get; set; }

        public string Os { get; set; }

        public string Case { get; set; }


    }
}
