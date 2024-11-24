using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SQLite03
{
    
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private string _nombre;
        private Trabajador _trabajadorSeleccionado;
        
        public string Nombre
        {
            get => _nombre;
            set
            {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged(nameof(Nombre));
                }
            }
        }
        private ObservableCollection<Trabajador> _ocTrabajadores;
        public ObservableCollection<Trabajador> OcTrabajadores
        {
            get { return _ocTrabajadores;}
            set
            {
                _ocTrabajadores = value;
                OnPropertyChanged();
            }
        }
        public MainPage()
        {
            InitializeComponent();
            // Lista de los trabajadores
            OcTrabajadores = new ObservableCollection<Trabajador>();


            // Creamos la ruta de la base de datos en el directorio de nuestro proyecto
            // No lo haríamos en producción pero en pruebas es más cómodo
            // tener localizada la base de datos y poder examinarla
            // con otros programas para ver los cambios producidos

            // En mi caso, devuelve:
            // "D:\\Datos\\proyectos_DI2425\\ud04part01ExempleSQLite\\SQLite03\\bin\\Debug\\net8.0-windows10.0.19041.0\\win10-x64\\AppX\\"
            string rutaDirectorioApp = System.AppContext.BaseDirectory;

            DirectoryInfo directorioApp = new DirectoryInfo(rutaDirectorioApp);

            // El objeto directorio será ahora:
            // D:\Datos\proyectos_DI2425\ud04part01ExempleSQLite\SQLite03
            directorioApp = directorioApp.Parent.Parent.Parent.Parent.Parent.Parent;

            // Creamos la ruta completa del fichero de la base de datos
            // En mi ejemplo:
            // D:\Datos\proyectos_DI2425\ud04part01ExempleSQLite\SQLite03\empresa.db
            string databasePath = Path.Combine(directorioApp.FullName, "empresa.db");

            // Creamos la cadena de conexión 
            string connectionString = $"Data Source={databasePath};Version=3;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                // Creamos la tabla de trabajador
                CrearTablaTrabajador(connection);

                // Insertamos unos registros de ejemplo
                InsertarDatosEjemplo(connection);

                // Creamos la consulta y la ejecutamos
                string sql = "SELECT * FROM Trabajador";
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                
                // Recorremos los registros devueltos del SELECT
                while (reader.Read())
                {
                    int idTrabajador = reader.GetInt32(0);
                    string nombreTrabajador = reader.GetString(1);
                    string apellidosTrabajador = reader.GetString(2);

                    // Creamos un objeto Trabajador y lo añadimos al Observable Collection
                    Trabajador trabajador = new Trabajador
                    {
                        Id = idTrabajador,
                        Nombre = nombreTrabajador,
                        Apellidos = apellidosTrabajador,
                    };

                    // Añadimos el trabajador al Observable Collection
                    OcTrabajadores.Add(trabajador);
                }

                reader.Close();
                connection.Close();
            }
            
            BindingContext = this;
        }
        private void OnAddTrabajadorClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(entryNombre.Text) && !string.IsNullOrWhiteSpace(entryApellido.Text))
            {
                var nuevoTrabajador = new Trabajador
                {
                    Nombre = entryNombre.Text,
                    Apellidos = entryApellido.Text
                };

                OcTrabajadores.Add(nuevoTrabajador);

                entryNombre.Text = string.Empty;
                entryApellido.Text = string.Empty;


            }
        }
        private void OnDeleteTrabajadorClicked(object sender, EventArgs e)
        {
            for (int i = 0; i < _ocTrabajadores.Count; i++)
            {
                if ((_ocTrabajadores.ElementAt(i).Nombre == entryNombre.Text) && (_ocTrabajadores.ElementAt(i).Apellidos == entryApellido.Text))
                {
                    _ocTrabajadores.RemoveAt(i);
                }

            }
        }
        private void OnTrabajadorSelected(object sender, SelectionChangedEventArgs e)
        {
            _trabajadorSeleccionado = e.CurrentSelection.FirstOrDefault() as Trabajador;

            if (_trabajadorSeleccionado != null)
            {
                entryNombre.Text = _trabajadorSeleccionado.Nombre;
                entryApellido.Text = _trabajadorSeleccionado.Apellidos;
                ((Button)FindByName("OnUpdateTrabajadorClicked")).IsEnabled = true;
                ((Button)FindByName("OnDeleteTrabajadorClicked")).IsEnabled = true;
            }
        }

        private void CrearTablaTrabajador(SQLiteConnection connection)
        {
            // Creamos la tabla Trabajador en caso de que no exista
            // Su clave principal es un autonumérico
            string queryCrearTablaTrabajador = "CREATE TABLE IF NOT EXISTS Trabajador (" +
                                     "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                     "nombre TEXT, " +
                                     "apellidos TEXT)";
            EjecutarNonQuery(connection, queryCrearTablaTrabajador);
        }
        private async void OnUpdateTrabajadorClicked(object sender, EventArgs e)
        {
            if (_trabajadorSeleccionado != null && !string.IsNullOrWhiteSpace(entryNombre.Text) && !string.IsNullOrWhiteSpace(entryApellido.Text))
            {
                _trabajadorSeleccionado.Nombre = entryNombre.Text;
                _trabajadorSeleccionado.Apellidos = entryApellido.Text;


                // Actualizar la lista en la interfaz
                int index = _ocTrabajadores.IndexOf(_trabajadorSeleccionado);
                _ocTrabajadores[index] = _trabajadorSeleccionado;

                entryNombre.Text = string.Empty;
                entryApellido.Text = string.Empty;
                
                ((Button)FindByName("OnUpdateTrabajadorClicked")).IsEnabled = false;
                ((Button)FindByName("OnDeleteTrabajadorClicked")).IsEnabled = false;
            }
        }
        private void InsertarDatosEjemplo(SQLiteConnection connection)
        {
            
            EjecutarNonQuery(connection, "insert into Trabajador (nombre, apellidos) values ('Ana', 'Gómez')");
            EjecutarNonQuery(connection, "insert into Trabajador (nombre, apellidos) values ('Juan', 'Pérez')");
        }

        private void EjecutarNonQuery(SQLiteConnection connection, string query)
        {
            // Este método ejecuta órdenes SQL que no devuelven consultas (Non-query command)

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                command.ExecuteNonQuery();
            }
        }

    }
}