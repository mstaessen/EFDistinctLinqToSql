using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace LinqToSqlWrongDistinctClauseReproduction.Models
{
    [Table("QueryDefinition")]
    public class QueryDefinition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public Guid Id { get; protected set; }

        [Required]
        [StringLength(255)]
        [Column("Name")]
        public string Name { get; set; }

        [Required]
        [Column("Query")]
        public string QueryString { get; set; }

        [Column("IsArchived")]
        public bool IsArchived { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public virtual User User { get; set; }

        public virtual ICollection<QueryRun> Runs { get; set; }
    }
}