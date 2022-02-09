using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaREAL.ViewModels
{
    public class UserRegistrationRequest
    {
        [DisplayName("Namn")]
        [StringLength(100, ErrorMessage = "Namn kan inte vara mer än 100 tecken")]
        [Required(ErrorMessage = "Namn är obligatoriskt")]
        public string Name { get; set; }

        [DisplayName("Adress")]
        [StringLength(50, ErrorMessage = "Adress kan inte vara mer än 50 tecken")]
        [Required(ErrorMessage = "Adress är obligatoriskt")]
        public string Address { get; set; }

        [DisplayName("Postnr")]
        [StringLength(20, ErrorMessage = "Postnr kan inte vara mer än 20 tecken")]
        [Required(ErrorMessage = "Postnr är obligatoriskt")]
        public string PostCode { get; set; }

        [DisplayName("Postort")]
        [StringLength(100, ErrorMessage = "Postort kan inte vara mer än 100 tecken")]
        [Required(ErrorMessage = "Postort är obligatoriskt")]
        public string City { get; set; }

        [DisplayName("Användarnamn")]
        [StringLength(20, ErrorMessage = "Användarnamn kan inte vara mer än 20 tecken")]
        [Required(ErrorMessage = "Användarnamn är obligatoriskt")]
        public string Username { get; set; }

        [DisplayName("Lösenord")]
        [StringLength(20, ErrorMessage = "Lösenord kan inte vara mer än 20 tecken")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$", 
                            ErrorMessage = "Lösenordet måste vara minst åtta tecken, minst en av gemen/versal/siffra/specialtecken.")]
        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        public string Password { get; set; }

        [StringLength(50, ErrorMessage = "Email kan inte vara mer än 50 tecken")]
        public string Email { get; set; } = null!;

        [DisplayName("Telefonnr")]
        [StringLength(50, ErrorMessage = "Telefonnr kan inte vara mer än 50 tecken")]
        public string PhoneNumber { get; set; } = null!;
    }
}
