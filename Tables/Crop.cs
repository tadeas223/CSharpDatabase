using Microsoft.Data.SqlClient;

public class Crop : SqlItem
{
    private int id;
    private Database database;
    private string name;
    private float price;

    public int Id { get => id; }
    public Database Database { get => database; }
    public string Name { get => name; }
    public float Price { get => price; }
    

    public Crop(string name, float price, Database database) 
    {
        this.id = -1;
        this.database = database;
        this.name = name;
        this.price = price;
    }

    public Crop(int id, string name, float price, Database database)
    {
        
        this.id = id;
        this.database = database;
        this.name = name;
        this.price = price;
    }

    public void Insert() 
    {
        if(id != - 1) throw new DatabaseException("Crop is already in database");

        using var command = new SqlCommand("INSERT INTO Crops (name, price) VALUES (@name, @price)", database.Connection);
        command.Parameters.AddWithValue("@name", name);
        command.Parameters.AddWithValue("@price", price);
        command.ExecuteNonQuery(); 
        
        // Get the id 
        using var command2 = new SqlCommand("SELECT TOP 1 idCrop FROM Crops ORDER BY idCrop DESC", database.Connection);
        var result = command2.ExecuteScalar();
        id = Convert.ToInt32(result);

    }

    public void Remove() 
    {
        if(id == -1) throw new DatabaseException("Crop not in database");
        
        using var command = new SqlCommand("DELETE FROM Crops WHERE idCrop = @idCrop", database.Connection);
        command.Parameters.AddWithValue("@idCrop", id);
        command.ExecuteNonQuery();
        id = -1;
    }
    
    public static Crop GetById(int id, Database database)
    {
        using var command = new SqlCommand("SELECT idCrop, name, price FROM Crops WHERE idCrop = @id", database.Connection);
        command.Parameters.AddWithValue("@id", id);
        var reader = command.ExecuteReader();
        if(reader.Read())
        {
            return new Crop(reader.GetInt32(0), reader.GetString(1), reader.GetFloat(2), database);
        }
        else
        {
            throw new DatabaseException($"Crop with id {id} not found"); 
        }
    }
}
