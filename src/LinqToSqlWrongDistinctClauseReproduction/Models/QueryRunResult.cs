using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinqToSqlWrongDistinctClauseReproduction.Models
{
    [Table("QueryRunResult")]
    public class QueryRunResult
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid PropertyId { get; set; }

        [Required]
        public virtual Property Property { get; set; }

        [Required]
        public Guid QueryRunId { get; set; }

        [Required]
        public virtual QueryRun QueryRun { get; set; }

        [Column("IsNew")]
        public bool IsNew { get; set; }
    }
}