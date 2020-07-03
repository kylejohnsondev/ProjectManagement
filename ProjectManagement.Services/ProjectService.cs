﻿using ProjectManagement.Data;
using ProjectManagement.Data.Entities;
using ProjectManagement.Models.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Services
{
    public class ProjectService
    {
        private readonly Guid _userId;

        public ProjectService(Guid userId)
        {
            _userId = userId;
        }

        // Create Project
        public bool CreateProject(ProjectCreate model)
        {
            var entity =
                new Project()
                {
                    OwnerId = _userId,
                    ProjectName = model.ProjectName,
                    ProjectDetails = model.ProjectDetails,
                    ProjectStatus = model.ProjectStatus,
                    EmployeeId = model.EmployeeId,
                    CustomerId = model.CustomerId,
                    ProjectStartDate = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Projects.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get All Projects
        public IEnumerable<ProjectList> GetProjects()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Projects
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                                e =>
                                    new ProjectList
                                    {
                                        ProjectId = e.ProjectId,
                                        ProjectName = e.ProjectName,
                                        ProjectStartDate = e.ProjectStartDate
                                    }
                                );
                return query.ToArray();
            }
        }

        // Get Single Project
        public ProjectDetail GetProjectById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Projects
                        .Single(e => e.ProjectId == id && e.OwnerId == _userId);
                return
                    new ProjectDetail
                    {
                        ProjectId = entity.ProjectId,
                        ProjectName = entity.ProjectName,
                        ProjectDetails = entity.ProjectDetails,
                        ProjectStatus = entity.ProjectStatus,
                        EmployeeId = entity.EmployeeId,
                        CustomerId = entity.CustomerId,
                        ProjectStartDate = entity.ProjectStartDate,
                        ProjectUpdated = entity.ProjectUpdated
                    };
            }
        }

        // Update Project
        public bool UpdateProject(ProjectUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Projects
                        .Single(e => e.ProjectId == model.ProjectId && e.OwnerId == _userId);

                entity.ProjectName = model.ProjectName;
                entity.ProjectDetails = model.ProjectDetails;
                entity.ProjectStatus = model.ProjectStatus;
                entity.EmployeeId = model.EmployeeId;
                entity.CustomerId = model.CustomerId;
                entity.ProjectUpdated = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        // Delete Project
        public bool DeleteProject(int projectId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Projects
                        .Single(e => e.ProjectId == projectId && e.OwnerId == _userId);

                ctx.Projects.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}