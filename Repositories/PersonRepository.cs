using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using PermitRegistrationSystem.Models;
using PermitRegistrationSystem.Repositories.Abstract;

namespace PermitRegistrationSystem.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly IDbConnection _connection;

    public PersonRepository(IDbConnection connection)
    {
        _connection = connection;
    }
    public void Create(Person person)
    {
       _connection.Open();
       string query = "INSERT INTO persons (name,surname,phone_number) VALUES (@Name, @Surname, @PhoneNumber)";
       _connection.Execute(query, person);
       _connection.Close();
       
    }

    public void Update(Person person)
    {
        _connection.Open();

        string query = "UPDATE persons SET ";

        List<string> updateColumns = new List<string>();
        DynamicParameters parameters = new DynamicParameters();

        if (person.Name != null)
        {
            updateColumns.Add("name = @Name");
            parameters.Add("@Name", person.Name);
        }

        if (person.Surname != null)
        {
            updateColumns.Add("surname = @Surname");
            parameters.Add("@Surname", person.Surname);
        }

        if (person.PhoneNumber != null)
        {
            updateColumns.Add("phone_number = @PhoneNumber");
            parameters.Add("@PhoneNumber", person.PhoneNumber);
        }

        query += string.Join(", ", updateColumns);
        query += " WHERE id = @Id";
        parameters.Add("@Id", person.Id);

        _connection.Execute(query, parameters);

        _connection.Close();

    }

    public void Delete(int id)
    {
       _connection.Open();
       string query = "DELETE FROM persons WHERE id = @Id";
        _connection.Execute(query, new{Id = id});
       _connection.Close();
    }

    public IList<Person> GetAll()
    {
       _connection.Open();
       string query = "SELECT * FROM persons";
       IList<Person> result = _connection.Query<Person>(query).ToList();
       _connection.Close();
       return result;

    }

    public Person GetById(int id)
    {
        _connection.Open();
        string query = "SELECT * FROM persons WHERE id = @Id";
        Person result = _connection.QueryFirstOrDefault<Person>(query, new{Id = id});
        _connection.Close();
        return result;
    }

    

    public void GetPersonWithPermissions(int Id)
    {
        throw new NotImplementedException();
    }
}