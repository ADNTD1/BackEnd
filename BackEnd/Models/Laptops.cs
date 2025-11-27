using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Laptop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Especificaciones principales
        public string Processor { get; set; }          
        public int RAM { get; set; }                       
        public string RAMType { get; set; }                
        public int Storage { get; set; }                   
        public string StorageType { get; set; }            
        public string GPU { get; set; }                    

        // Pantalla
        public decimal ScreenSize { get; set; }            
        public string ScreenResolution { get; set; }       
        public int RefreshRate { get; set; }               

        // Conectividad
        public bool HasWifi6 { get; set; }
        public bool HasBluetooth { get; set; }
        public string Ports { get; set; }                  

        // Construcción y diseño
        public decimal Weight { get; set; }                
        public string Material { get; set; }               
        public string Color { get; set; }

        // Batería
        public int BatteryCapacity { get; set; }           
        public string ChargerPower { get; set; }           

        // Sistema operativo
        public string OperatingSystem { get; set; }

        // Extras
        public bool HasBacklitKeyboard { get; set; }
        public bool HasFingerprintReader { get; set; }
        public bool HasWebcam { get; set; }
        public bool HasSpeakers { get; set; }

        // Relación con Product (si es que la tabla Laptop se relaciona con Product como 1:1)
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
