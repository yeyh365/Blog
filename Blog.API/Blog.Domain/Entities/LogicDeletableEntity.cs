using Blog.Domain.DBFilter;
namespace Blog.Domain.Entities
{
    using Blog.Domain.Repositories;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public abstract class LogicDeletableEntity : IDeleteFilter, IEntityCreate,IEntityUpdate
    {
        [Column("deleted", TypeName = "bit")]
        [Required]
        public bool Deleted { get; set; }

        [Column("createdby")]
        public string? CreatedBy
        {
            get;set;
        }
        [Column("created")]
        public DateTime? Created { get; set; }

        [Column("updatedby")]
        public string? UpdatedBy
        {
            get;set;
        }

        [Column("updated")]
        public DateTime? Updated { get; set; }
    }
}