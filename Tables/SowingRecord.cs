using Microsoft.Data.SqlClient;

public class SowingRecord : SqlItem<SowingRecord>
{

    private int id;
    private Database database; 
    private Crop crop;
    private Field field;
    private Worker worker;
    private DateTime date;

    public int Id { get => id; }
    public Database Database { get => database; }

    // using @field because field is a baned keyword :(
    public Field Field { get => @field; }
    public Worker Worker { get => worker; }
    public DateTime Date { get => date; }
    public Crop Crop { get => crop; }

    public SowingRecord(Crop crop, Field field, Worker worker, DateTime date, Database database)
    {
        this.crop = crop;
        this.field = field;
        this.worker = worker;
        this.date = date;
        this.id = -1;
        this.database = database;
    }

    public void Insert()
    {
        if(id != - 1) throw new DatabaseException("SowingRecord is already in database");
        
        using var command = new SqlCommand("INSERT INTO SowingRecords (date, idCrop, idWorker, idField) VALUES (@date, @idCrop, @idWorker, @idField)", database.Connection);
        command.Parameters.AddWithValue("@date", date);
        command.Parameters.AddWithValue("@idCrop", crop.Id);
        command.Parameters.AddWithValue("@idWorker", worker.Id);
        command.Parameters.AddWithValue("@idField", field.Id);
        command.ExecuteNonQuery(); 
        
        // Get the id 
        using var command2 = new SqlCommand("SELECT TOP 1 idSowingRecord FROM SowingRecords ORDER BY idSowingRecord DESC", database.Connection);
        var result = command2.ExecuteScalar();
        id = Convert.ToInt32(result); 
    }

    public void Remove()
    {
        if(id == -1) throw new DatabaseException("SowingRecord not in database");
        
        using var command = new SqlCommand("DELETE FROM SowingRecords WHERE idSowingRecord = @idSowingRecord", database.Connection);
        command.Parameters.AddWithValue("@idSowingRecord", id);
        command.ExecuteNonQuery();
        id = -1;
    }
}
