using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Domain.Entities
{
    public class Planet
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Climate { get; set; }
        public virtual string Terrain { get; set; }
    }
}
