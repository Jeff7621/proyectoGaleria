

using System.Security.Cryptography;
using System.Text;

namespace Galleria;

public partial class Registro : ContentPage
{
    private FotoDatabase _fotoDatabase;
    private int _usuario;

    public Registro()
    {
        InitializeComponent();
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "fotos.db3");
        _fotoDatabase = new FotoDatabase(dbPath);

    }

    private void ConpasswordEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        string newValue = e.NewTextValue;
        Resultado.Text = " " + newValue + "";
    }

    private async void GuardarBTN_Clicked(object sender, EventArgs e)
    {

        string nombre = usuarioEntry.Text?.Trim();
        string email = correoEntry.Text?.Trim();
        string contrasena = contraseñaEntry.Text;
        string confirmarContrasena = ConfirmarContrasenaEntry.Text;

        // Validaciones básicas
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(confirmarContrasena))
        {
            await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
            return;
        }

        if (contrasena != confirmarContrasena)
        {
            await DisplayAlert("Error", "Las contraseñas no coinciden.", "OK");
            return;
        }

        // Verificar si el correo ya está registrado
        var usuarioExistente = await _fotoDatabase.GetUsuarioByCorreoAsync(email);
        if (usuarioExistente != null)
        {
            await DisplayAlert("Error", "El correo ya está registrado.", "OK");
            return;

        }

        // Crear y guardar el nuevo usuario
        var nuevoUsuario = new Usuario
        {
            NombreUsuario = nombre,
            Email = email,
            Contraseña = contrasena
        };

        await _fotoDatabase.SaveUsuarioAsync(nuevoUsuario);
        await DisplayAlert("Éxito", "Usuario registrado correctamente.", "OK");

        
        // Navegar a la página de inicio de sesión
        await Navigation.PushAsync(new MainPage());

        // Opcional: Remover páginas anteriores del stack
        Navigation.RemovePage(this);


    }



}