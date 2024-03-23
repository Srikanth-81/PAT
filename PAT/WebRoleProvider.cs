using PAT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace PAT
{
    public class WebRoleProvider : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var context = new DbContexts();
            var result = (from admin in context.Admin
                          join role in context.roles on admin.RoleID equals role.RoleID
                          where admin.AdminID == username
                          select role.RoleName).ToArray();

            var result1 = (from doctor in context.Doctors
                           join role in context.roles on doctor.RoleID equals role.RoleID
                           where doctor.DoctorID == username
                           select role.RoleName).ToArray();

            var result2 = (from patient in context.Patients
                           join role in context.roles on patient.RoleID equals role.RoleID
                           where patient.PatientID == username
                           select role.RoleName).ToArray();

            var result3 = (from clerk in context.Clerks
                           join role in context.roles on clerk.RoleID equals role.RoleID
                           where clerk.ClerkID == username
                           select role.RoleName).ToArray();

            return ((result.Concat(result1).Concat(result2)).Concat(result3)).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}