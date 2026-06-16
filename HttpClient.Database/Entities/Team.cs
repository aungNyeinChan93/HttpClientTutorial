using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HttpClient.Database.Entities
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }

        public string Name { get; set; } = string.Empty;

        [ForeignKey(nameof(ManagerId))]
        public int? ManagerId { get; set; }

        public Manager? Manager { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
