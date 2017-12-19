using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Infrastructure.Interfaces;
using ShopCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ShopCoreApp.Data.Entities
{
    public class Announcement : DomainEntity<string>, ISwitchable, IDateTracking
    {

        public Announcement()
        {
            AnnouncementUsers = new List<AnnouncementUser>();
        }

        [Required]
        [StringLength(250)]
        public string Title { set; get; }

        [StringLength(250)]
        public string Content { set; get; }

        public Guid UserId { set; get; }

        [ForeignKey("UserId")]
        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<AnnouncementUser> AnnouncementUsers { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }
    }
}
