namespace proyectoGaleria
{
    public partial class MainPage : ContentPage
    {
    
        private FotoDatabase _fotoDatabase;

        public MainPage()
        {
            InitializeComponent();
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "fotos1.db3");
            _fotoDatabase = new FotoDatabase(dbPath);

        }

        private async void logginBTN_Clicked(object sender, EventArgs e)
        {
            string correo = correoEntry.Text?.Trim();
            string contrasena = contrasenaEntry.Text;

            if (correo == "SuperUsuario" && contrasena == "Ingresar1234")
            {
                await DisplayAlert("Inicio de sesion", "Inicio de sesion exitosa!", "OK");
                await Navigation.PushAsync(new ListaRegistros());
            }

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
                return;
            }

            var usuario = await _fotoDatabase.GetUsuarioAsync(correo, contrasena);
            if (usuario != null)
            {
                // Guardar el usuario actual en una variable estática o servicio
                App.CurrentUser = usuario;

                await DisplayAlert("Éxito", "Inicio de sesión correcto.", "OK");

                // Navegar a la MainPage y limpiar el stack de navegación
                await Navigation.PushAsync(new Fotos());

                    // Opcional: Remover páginas anteriores del stack
                Navigation.RemovePage(this);
            }
            else
            {
                await DisplayAlert("Error", "Correo o contraseña incorrectos.", "OK");
            }
        }




        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Registro());
        }

        private void passwordEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            string newValue = e.NewTextValue;
            Resultado.Text = " " + newValue + " ";
        }
   
    }
}




