using System.ComponentModel.DataAnnotations;

namespace EmployeeNavigatorV3.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Nombre { get; set; }

        [Required]
        public byte[] Archivo { get; set; }
        public string Extension { get; set; }
        //public DateTime FechaCreacion { get; set; }
        //public string CreadoPor { get; set; }
        //public string CreadorId { get; set; }
    }
}
