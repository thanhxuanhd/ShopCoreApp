using Microsoft.AspNetCore.Identity;
using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Infrastructure.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCoreApp.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        public AppUser()
        {
        }

        public AppUser(Guid id, string fullName, string userName,
            string email, string phoneNumber, string avatar, Status status)
        {
            Id = id;
            FullName = fullName;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Avatar = avatar;
            Status = status;
        }

        public string FullName { get; set; }
        public DateTime? BirthDay { get; set; }
        public decimal Balance { get; set; }
        public string Avatar { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}