using ShopCoreApp.Infrastructure.Enums;
using ShopCoreApp.Infrastructure.Interfaces;
using ShopCoreApp.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopCoreApp.Data.Entities
{
    [Table("Blogs")]
    public class Blog : DomainEntity<int>, ISwitchable, IDateTracking, IHasSeoMetaData
    {
        public Blog()
        {
        }

        public Blog(string name, string thumbnailImage,
           string description, string content, bool? homeFlag, bool? hotFlag,
           string tags, Status status, string seoPageTitle,
           string seoAlias, string seoMetaKeyword,
           string seoMetaDescription)
        {
            Name = name;
            Image = thumbnailImage;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            Tags = tags;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
        }

        public Blog(int id, string name, string thumbnailImage,
             string description, string content, bool? homeFlag, bool? hotFlag,
             string tags, Status status, string seoPageTitle,
             string seoAlias, string seoMetaKeyword,
             string seoMetaDescription)
        {
            Id = id;
            Name = name;
            Image = thumbnailImage;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            HotFlag = hotFlag;
            Tags = tags;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyword;
            SeoDescription = seoMetaDescription;
        }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [MaxLength(256)]
        public string Image { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? HomeFlag { set; get; }
        public bool? HotFlag { set; get; }
        public int? ViewCount { set; get; }
        public string Tags { get; set; }
        public virtual ICollection<BlogTag> BlogTags { set; get; }

        [MaxLength(256)]
        public string SeoPageTitle { get; set; }

        [MaxLength(256)]
        [Column(TypeName = "varchar(256)")]
        public string SeoAlias { get; set; }

        [MaxLength(256)]
        public string SeoKeywords { get; set; }

        [MaxLength(256)]
        public string SeoDescription { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public Status Status { get; set; }
    }
}