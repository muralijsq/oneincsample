using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OneIncSample.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; private set; }

        [Required]
        [MaxLength(128)]
        public string? FirstName { get; set; }

        [MaxLength(128)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(User), nameof(ValidateAge))]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be a valid ten-digit number.")]
        public string? PhoneNumber { get; set; }

        public int Age => CalculateAge(DateOfBirth);

        private int CalculateAge(DateTime dateOfBirth)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }

        public static ValidationResult? ValidateAge(DateTime dateOfBirth, ValidationContext context)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > DateTime.Today.AddYears(-age)) age--;
            return age >= 18 ? ValidationResult.Success : new ValidationResult("User must be 18 years or older.");

        }
    }
}
