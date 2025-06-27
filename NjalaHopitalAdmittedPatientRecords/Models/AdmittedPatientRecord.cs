using System;
using System.ComponentModel.DataAnnotations;




public class AdmittedPatientRecord
{
    [Key]
    public required string ID { get; set; }

    [Required, MaxLength(50)]
    public required string FirstName { get; set; }

    [Required, MaxLength(50)]
    public required string LastName { get; set; }

    [Required, MaxLength(15)]
    public required string Gender { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    public int PhoneNumber { get; set; }

    [Required, MaxLength(50)]
    public required string Occupation { get; set; }

    [Required, MaxLength(50)]
    public required string HomeAddress { get; set; }

    [Required, MaxLength(50)]
    public required string SicknessDiagnosed { get; set; }

    [Required]
    public DateTime DateAdmitted { get; set; }

    [Required]
    public DateTime DateDischarged { get; set; }
}