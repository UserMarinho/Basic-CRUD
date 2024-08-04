using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Runtime.CompilerServices;

namespace DBConnection.Entities
{
    class Student
    {
        [Key]
        [Column("id", TypeName = "int")]
        public int Id { get; private set; }

        [Required]
        [Column("cpf", TypeName = "varchar(11)")]
        public string CPF { get; set; }

        [Required]
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Required]
        [Column("birthdate", TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [Column("email", TypeName = "varchar(255)")]
        public string? Email { get; set; }

        public Student()
        {
        }

        public Student(string cpf, string name, DateTime birthDate)
        {
            this.CPF = cpf;
            this.Name = name;
            this.BirthDate = birthDate;
            this.Email = "não informado";
        }

        public Student(string cpf, string name, string email, DateTime birthDate)
        {
            this.CPF = cpf;
            this.Name = name;
            this.BirthDate = birthDate;
            this.Email = email;
        }

        public int GetYearsOld()
        {
            DateTime now = DateTime.Now;
            int yearsOld = now.Year - this.BirthDate.Year;
            if (now.DayOfYear < this.BirthDate.DayOfYear)
            {
                yearsOld -= 1;
            }
            return yearsOld;
        }

        public override string ToString()
        {
            return $"--------------------\n" +
                $"ID: {this.Id}\n" +
                $"Name: {this.Name}\n" +
                $"CPF: {this.CPF}\n" +
                $"Birth date: {this.BirthDate.ToString("dd/MM/yyyy")}\n" +
                $"Email: {this.Email}\n" +
                $"Age: {GetYearsOld()}\n" +
                $"--------------------";
        }
    }
}
