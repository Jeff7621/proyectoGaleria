

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
        string contrasena = contrase�aEntry.Text;
        string confirmarContrasena = ConfirmarContrasenaEntry.Text;

        // Validaciones b�sicas
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(confirmarContrasena))
        {
            await DisplayAlert("Error", "Por favor, completa todos los campos.", "OK");
            return;
        }

        if (contrasena != confirmarContrasena)
        {
            await DisplayAlert("Error", "Las contrase�as no coinciden.", "OK");
            return;
        }

        // Verificar si el correo ya est� registrado
        var usuarioExistente = await _fotoDatabase.GetUsuarioByCorreoAsync(email);
        if (usuarioExistente != null)
        {
            await DisplayAlert("Error", "El correo ya est� registrado.", "OK");
            return;

        }

        // Crear y guardar el nuevo usuario
        var nuevoUsuario = new Usuario
        {
            NombreUsuario = nombre,
            Email = email,
            Contrase�a = contrasena
        };

        await _fotoDatabase.SaveUsuarioAsync(nuevoUsuario);
        await DisplayAlert("�xito", "Usuario registrado correctamente.", "OK");

        
        // Navegar a la p�gina de inicio de sesi�n
        await Navigation.PushAsync(new MainPage());

        // Opcional: Remover p�ginas anteriores del stack
        Navigation.RemovePage(this);


    }



}