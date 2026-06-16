using HttpClient.Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HttpClient.domain.Dtos
{
    public class TeamDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = string.Empty;

        public ManagerDto? Manager { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
