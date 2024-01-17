using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using TagsPractica.DAL.Models;

namespace TagsPractica.API.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [Display(Name = "Имя")]
        public string? userName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        public string? password { get; set; }

        [Required(ErrorMessage = "Обязательно подтвердите пароль")]
        [Compare("password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль", Prompt = "**********")]
        public string? passwordConfirm { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string? email { get; set; }

        //[Required]
        [Display(Name = "Роль")]
        public int roleId { get; set; }
        
        //[ForeignKey("roleId")]
        //public virtual Role Role { get; set; }

        //[Required]
        //[Display(Name = "Роль2")]
        //public IEnumerable<SelectListItem> Roles { get; set; }

        [Display(Name = "existUser")]
        [DefaultValue(false)]
        public bool existUser { get; set; }

        //[Required]
        [Display(Name = "Id")]
        public string? Id { get; set; }



        //https://mahedee.net/uses-of-dropdown-list-and-radio-button-in-asp-net-mvc-application-with-entity-framework/

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
