using HttpClient.Database.Entities;
using HttpClient.domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace HttpClient.domain.Mappers
{
    public static class Mapper
    {
        public static TeamDto Change(this Team team)
        {
            return new TeamDto
            {
                TeamId = team.TeamId,
                Name = team.Name,
                CreatedAt = team.CreatedAt,
                Manager = team.Manager != null ? new ManagerDto
                {
                    ManagerId = team.Manager.ManagerId,
                    Name = team.Manager.Name,
                    Age = team.Manager.Age,
                    CreateAt = team.Manager.CreateAt,
                    Country = team.Manager.Country,
                } : null
            };
        }

        public static ManagerDto Change(this Manager manager)
        {
            return new ManagerDto
            {
                ManagerId = manager.ManagerId,
                Name = manager.Name,
                Age = manager.Age,
                Country = manager.Country,
                Team = manager.Team != null ? new TeamDto
                {
                    TeamId = manager.Team.TeamId,
                    Name = manager.Team.Name,
                    CreatedAt = manager.Team.CreatedAt
                } : null,
                CreateAt = manager.CreateAt
            };
        }
    }
}
