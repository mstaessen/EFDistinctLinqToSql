using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinqToSqlWrongDistinctClauseReproduction.Models
{
    [Table("Property")]
    public class Property
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; protected set; }

        [Required]
        [Column("Source")]
        public DataSource Source { get; set; }

        [Required]
        [Column("ForeignId")]
        public string ForeignId { get; set; }

        [Required]
        [Column("CreationDate")]
        public DateTime CreationDate { get; set; }

        [Column("LastUpdate")]
        public DateTime? LastUpdate { get; set; }

        public DateTime? RemovalDate { get; set; }

        [Column("Details")]
        public string Details { get; set; }

        [Column("IsRemoved")]
        public bool IsRemoved { get; set; }
    }
}