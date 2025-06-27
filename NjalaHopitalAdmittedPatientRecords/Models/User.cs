using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace NjalaHopitalAdmittedPatientRecords.Models
{
    [Table("Registration")] // 👈 This is the key fix
    public class Registration
    {
        public int Id { get; set; } // Primary Key
        required
        public string UserName { get; set; }
        
        required
        public string Email { get; set; }
        required
            
            public string Password { get; set; } // ⚠️ In production, always hash passwords!
    }
}



