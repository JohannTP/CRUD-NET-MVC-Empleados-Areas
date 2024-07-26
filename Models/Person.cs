using System.ComponentModel.DataAnnotations;

namespace EmployeeNavigatorV3.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo codigo es requerido")]
        public string Code { get; set; }

        [Required(ErrorMessage = "El campo DNI es requerido")]
        public string Dni { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El campo apellido es requerido")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El campo correo es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo edad es requerido")]
        public int Age { get; set; }

        [Required(ErrorMessage = "El campo fec. nacimiento es requerido")]
        public DateTime DateOfBirth { get; set; }

        //field area
        [Required(ErrorMessage = "Seleccione el area del empleado")]
        public string Area { get; set; }
        //public int IdArea { get; set; }
        //fiel uni  
        [Required(ErrorMessage = "Seleccione la UNI del empleado")]
        public string Uni { get; set; }

        //fiel profession
        [Required(ErrorMessage = "Seleccione la profesion del empleado")]
        public string Profession { get; set; }
        //public int IdProfession { get; set; }

        //fiel
        [Required(ErrorMessage = "Seleccione el cargo del empleado")]
        public string Position { get; set; }

        public int Status { get; set; }

        //field direccion
        //public int IdAddress { get; set; }
    }
}
