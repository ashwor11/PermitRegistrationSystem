using System.Configuration;
using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using PermitRegistrationSystem.Models;
using PermitRegistrationSystem.Repositories.Abstract;

namespace PermitRegistrationSystem.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        
        private readonly IDbConnection _connection;

            public PermissionRepository(IDbConnection connection)
            {
                _connection = connection;
            }


            public void Create(Permission permission)
            {
                _connection.Open();
                string query =
                    "INSERT INTO permissions (start_date, end_date, person_id,  reason) VALUES (@StartDate, @EndDate, @PersonId, @Reason)";
                _connection.Execute(query, permission);
                _connection.Close();
                
            }

            public void Update(Permission permission)
            {
            _connection.Open();

            string query = "UPDATE permissions SET ";
            List<string> updateColumns = new List<string>();
            DynamicParameters parameters = new DynamicParameters();

            if (permission.StartDate != default)
            {
                updateColumns.Add("start_date = @StartDate");
                parameters.Add("@StartDate", permission.StartDate);
            }

            if (permission.EndDate != default)
            {
                updateColumns.Add("end_date = @EndDate");
                parameters.Add("@EndDate", permission.EndDate);
            }

            if (permission.PersonId != default)
            {
                updateColumns.Add("person_id = @PersonId");
                parameters.Add("@PersonId", permission.PersonId);
            }


            if (permission.Reason != null && permission.Reason != "")
            {
                updateColumns.Add("reason = @Reason");
                parameters.Add("@Reason", permission.Reason);
            }

            query += string.Join(", ", updateColumns);
            query += " WHERE id = @Id";
            parameters.Add("@Id", permission.Id);

            _connection.Execute(query, parameters);

            _connection.Close();
        }

            public void Delete(int id)
            {
            _connection.Open();
            string query =
                "DELETE FROM permissions WHERE id = @Id";
            _connection.Execute(query, new{Id = id});
            _connection.Close();
        }

            public IList<Permission> GetAll()
            {
                _connection.Open();
                string query = "SELECT * FROM permissions";
                IList<Permission> permissions = _connection.Query<Permission>(query).ToList();
                _connection.Close();
                return permissions;
            }

           


            public Permission GetById(int id)
            {
                _connection.Open();
                string query = "SELECT * FROM permissions WHERE id = @Id";
                Permission permission = _connection.QueryFirstOrDefault<Permission>(query, new{Id = id});
                _connection.Close();
                return permission;
            }

            public PermissionByUserDto GetByPersonId(int personId)
            {

                string query = "SELECT * FROM persons WHERE id = @PersonId";
                Person person = _connection.QueryFirstOrDefault<Person>(query, new {PersonId = personId});

                query = "SELECT * FROM permissions WHERE person_id = @PersonId";
                 IList<Permission> permissions = _connection.Query<Permission>(query, new { PersonId = personId}).ToList();
                 _connection.Close();
                 PermissionByUserDto permissionByUserDto = new PermissionByUserDto()
                 {
                     PersonId = person.Id,
                     Name = person.Name,
                     Surname = person.Surname,
                     PhoneNumber = person.PhoneNumber,
                     Permissions = permissions,
                 };
                 return permissionByUserDto;
            }
    }

        
    
}
