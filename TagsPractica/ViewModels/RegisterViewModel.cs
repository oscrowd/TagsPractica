using System.ComponentModel.DataAnnotations;

namespace TagsPractica.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Имя")]
        public string? userName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string? password { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? email { get; set; }

        /*
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        public string? PasswordReg { get; set; }

        [Required]
        [Compare("PasswordReg", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string? PasswordConfirm { get; set; }

        */
    }
}
