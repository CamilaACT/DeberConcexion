using System;
using System.Data;
using System.Data.SqlClient;

public class Singleton
{
    private static Singleton instance;
    private SqlConnection connection;

    private Singleton()
    {
        string connectionString = "Data Source=(local); Initial Catalog=Facturacion; trusted_connection=yes; TrustServerCertificate=True;";
        connection = new SqlConnection(connectionString);
    }

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }

    public void OpenConnection()
    {
        if (connection.State != ConnectionState.Open)
        {
            connection.Open();
        }
    }

    public void CloseConnection()
    {
        if (connection.State != ConnectionState.Closed)
        {
            connection.Close();
        }
    }

    public void DisplayData()
    {
        OpenConnection();
        // Aquí puedes ejecutar una consulta SQL y mostrar los datos en pantalla
        string query = "SELECT * FROM dbo.Cliente"; // Actualiza con el nombre de tu tabla real
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            // Leer y mostrar los datos
            int id = reader.GetInt32(0); // Reemplaza 0 con el índice de columna correcto
            string DNI = reader.GetString(1); // Reemplaza 1 con el índice de columna correcto
            string Nombre = reader.GetString(2); // Reemplaza 2 con el índice de columna correcto
            string Direccion = reader.GetString(3); // Reemplaza 3 con el índice de columna correcto
            string Telefono = reader.GetString(4); // Reemplaza 4 con el índice de columna correcto
            string EstadoCivil = reader.GetString(4); // Reemplaza 5 con el índice de columna correcto

            Console.WriteLine("ID: {0}, DNI: {1}, Nombre: {2}, Direccion: {3}, Telefono: {4}, EstadoCivil: {5}", id, DNI, Nombre, Direccion, Telefono, EstadoCivil);
        }

        reader.Close();
        CloseConnection();
    }
}

public class Program
{
    public static void Main()
    {
        // Obtener la instancia de Singleton
        Singleton dbConnection = Singleton.Instance;

        // Mostrar los datos en pantalla
        dbConnection.DisplayData();

        // Esperar a que el usuario presione una tecla antes de salir
        Console.ReadKey();
    }
}
