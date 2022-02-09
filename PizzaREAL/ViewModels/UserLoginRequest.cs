using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PizzaREAL.ViewModels
{
    public class UserLoginRequest
    {
        [DisplayName("Användarnamn")]
        [StringLength(20, ErrorMessage = "Användarnamn kan inte vara mer än 20 tecken")]
        [Required(ErrorMessage = "Användarnamn är obligatoriskt")]
        public string Username { get; set; }

        [DisplayName("Lösenord")]
        [StringLength(20, ErrorMessage = "Lösenord kan inte vara mer än 20 tecken")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$",
                            ErrorMessage = "Lösenordet måste vara minst åtta tecken, " +
                                           "minst en av gemen/versal/siffra/specialtecken.")]
        [Required(ErrorMessage = "Lösenord är obligatoriskt")]
        public string Password { get; set; }
    }
}
