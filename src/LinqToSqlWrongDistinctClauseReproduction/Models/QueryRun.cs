using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LinqToSqlWrongDistinctClauseReproduction.Models
{
    [Table("QueryRun")]
    public class QueryRun
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid QueryDefinitionId { get; set; }

        /// <summary>
        /// Gets or sets the Query for which this property was found. 
        /// </summary>
        [Required]
        public virtual QueryDefinition QueryDefinition { get; set; }

        public virtual ICollection<QueryRunResult> Results { get; set; }

        [Required]
        [Column("DateTime")]
        public DateTime DateTime { get; set; }

        [Column("HasErrors")]
        public bool HasErrors { get; set; }

        [Column("Message")]
        public string Message { get; set; }
    }
}