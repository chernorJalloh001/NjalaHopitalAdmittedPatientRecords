using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NjalaHopitalAdmittedPatientRecords.Models
{
    [Table("Login")] // 👈 This is the key fix
    public class Login
    {
        [Key]
        public required string UserName { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string PassWord { get; set; }
    }
}

