
namespace LinuxPackages.Web.Mvc.ViewModels.Profile
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using LinuxPackages.Data.Models;
    using Infrastructure.Mappings;

    public class ChangeAvatarViewModel : IMapFrom<Avatar>
    {
        [Required]
        [Display(Name = "New avatar")]
        [UIHint("SingleFileUpload")]
        public HttpPostedFileBase Contents { get; set; }
    }
}