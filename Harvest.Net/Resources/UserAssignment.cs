﻿using Harvest.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Harvest.Net
{
    public partial class HarvestRestClient
    {
        // https://github.com/harvesthq/api/blob/master/Sections/User%20Assignment.md

        /// <summary>
        /// List all user assignments on a project. Makes a GET request to the Projects/User_Assignments resource.
        /// </summary>
        /// <param name="projectId">The Id of the project for which to retrieve assignemnts</param>
        /// <param name="updatedSince">An optional filter on the updated-at property</param>
        public IList<UserAssignment> ListUserAssignments(long projectId, DateTime? updatedSince = null)
        {
            var request = Request("projects/" + projectId + "/user_assignments");

            if (updatedSince != null)
                request.AddParameter("updated_since", updatedSince.Value.ToString("yyyy-MM-dd HH:mm"));

            return Execute<List<UserAssignment>>(request);
        }

        /// <summary>
        /// Retrieve an user assignment on a project. Makes a GET request to the Projects/User_Assignments resource.
        /// </summary>
        /// <param name="projectId">The Id of the project for which to retrieve an assignemnt</param>
        /// <param name="userAssignmentId">The Id of the assignment to retrieve</param>
        public UserAssignment UserAssignment(long projectId, long userAssignmentId)
        {
            var request = Request("projects/" + projectId + "/user_assignments/" + userAssignmentId);

            return Execute<UserAssignment>(request);
        }

        /// <summary>
        /// Assigns an user to a project. Makes both a POST and a GET request to the Projects/User_Assignments resource.
        /// </summary>
        /// <param name="projectId">The Id of the project to which to add the user</param>
        /// <param name="userId">The Id of the user to add</param>
        public UserAssignment CreateUserAssignment(long projectId, long userId)
        {
            var request = Request("projects/" + projectId + "/user_assignments", RestSharp.Method.POST);

            request.AddBody(new UserAssignmentCreateOptions()
            {
                Id = userId
            });

            return Execute<UserAssignment>(request);
        }

        /// <summary>
        /// Remove an user from a project. Makes a DELETE request to the Projects/User_Assignments resource.
        /// </summary>
        /// <param name="projectId">The Id of the project from which to remove the user</param>
        /// <param name="userAssignmentId">The Id of the user assignment to remove</param>
        public bool DeleteUserAssignment(long projectId, long userAssignmentId)
        {
            var request = Request("projects/" + projectId + "/user_assignments/" + userAssignmentId, RestSharp.Method.DELETE);

            var result = Execute(request);

            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }

        /// <summary>
        /// Update an user assignment on a project. Makes a PUT and a GET request to the Projects/User_Assignments resource.
        /// </summary>
        /// <param name="projectId">The ID of the project to update</param>
        /// <param name="userAssignmentId">The ID of the user assignment to update</param>
        /// <param name="userId">The ID of the user assigned</param>
        /// <param name="deactivated">Whether the user assignment is inactive</param>
        /// <param name="hourlyRate">The hourly rate</param>
        /// <param name="budget">The budget</param>
        /// <param name="isProjectManager">Whether this user is a project manager</param>
        public UserAssignment UpdateUserAssignment(long projectId, long userAssignmentId, long userId, bool? deactivated = null, decimal? hourlyRate = null, decimal? budget = null, bool? isProjectManager = null)
        {
            var options = new UserAssignmentOptions()
            {
                UserId = userId,
                ProjectId = projectId,
                Deactivated = deactivated,
                HourlyRate = hourlyRate,
                Budget = budget,
                IsProjectManager = isProjectManager
            };

            return UpdateUserAssignment(projectId, userAssignmentId, options);
        }

        /// <summary>
        /// Updates an user assignment on a project. Makes a PUT and a GET request to the Projects/User_Assignments resource.
        /// </summary>
        /// <param name="projectId">The ID of the project to update</param>
        /// <param name="userAssignmentId">The ID of the user assignment to update</param>
        /// <param name="options">The options to be updated</param>
        public UserAssignment UpdateUserAssignment(long projectId, long userAssignmentId, UserAssignmentOptions options)
        {
            var request = Request("projects/" + projectId + "/user_assignments/" + userAssignmentId, RestSharp.Method.PUT);

            request.AddBody(options);

            return Execute<UserAssignment>(request);
        }
    }
}
