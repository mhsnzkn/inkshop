using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
    public class UserEditDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
        [EmailAddress(ErrorMessage = "Email adresi uygun degil")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre boş bırakılamaz")]
        [StringLength(100, ErrorMessage = "{0} en az {2} en fazla {1} karakter olmalı", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Şifre (Tekrar)")]
        [Compare("Password", ErrorMessage = "Şifreler aynı değil")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Rol boş bırakılamaz")]
        [Display(Name = "Rol")]
        public string Role { get; set; }
        [Display(Name = "Personel")]
        public int? PersonnelId { get; set; }
    }
}
