// This calls was generated by ChatGPT
// I was too lazy to do it myself :)

/// <summary>
/// Exception that should be thrown when any operation related with the <see cref="Database"/> occurs
/// </summary>
class DatabaseException : Exception
{
    // Default constructor
    public DatabaseException()
        : base("An error occurred in the database operation.")
    {
    }

    // Constructor that takes a custom message
    public DatabaseException(string message)
        : base(message)
    {
    }

    // Constructor that takes a custom message and inner exception
    public DatabaseException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    // Constructor that takes an error code (optional) along with a message
    public DatabaseException(string message, int errorCode)
        : base($"{message} Error Code: {errorCode}")
    {
    } 
} 

