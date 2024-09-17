

namespace Galleria;

public partial class ListaRegistros : ContentPage
{
    private FotoDatabase _fotoDatabase;
    private int _usuario;

    public ListaRegistros()
	{
        InitializeComponent();
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "fotos.db3");
        _fotoDatabase = new FotoDatabase(dbPath);
        
	}

    private async void ListaUsuarios_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        var usuarioR = (Usuario)e.Item;
        var action = await DisplayActionSheet("Action", "Cancel", null, "Edit", "Delete");

        switch (action)
        {
            case "Edit":
                _usuario = usuarioR.Id;
                usuarioEntryField.Text = usuarioR.NombreUsuario;
                emailEntryField.Text = usuarioR.Email;
                contraseñaEntryField.Text = usuarioR.Contraseña;
                break;

            case "Delete":
                await _fotoDatabase.GetEliminarUsuario(usuarioR);
                ListaUsuarios.ItemsSource = await _fotoDatabase.Getlista_usuario();
                break;
        }
    }

    private async void GuardarButton_Clicked(object sender, EventArgs e)
    {
        if (_usuario == 0)
        {
            //agrega cliente
            await _fotoDatabase.GetGuardarUsuario(new Usuario
            {
                NombreUsuario = usuarioEntryField.Text,
                Email = emailEntryField.Text,
                Contraseña = contraseñaEntryField.Text
            });
        }
        else
        {
            //Edita cliente
            await _fotoDatabase.GetEditarUsuario(new Usuario
            {
                Id = _usuario,
                NombreUsuario = usuarioEntryField.Text,
                Email = emailEntryField.Text,
                Contraseña = contraseñaEntryField.Text
            });
            _usuario = 0;
        }
        usuarioEntryField.Text = string.Empty;
        emailEntryField.Text = string.Empty;
        contraseñaEntryField.Text = string.Empty;

        ListaUsuarios.ItemsSource = await _fotoDatabase.Getlista_usuario();
    }

    private async void actualizarButton_Clicked(object sender, EventArgs e)
    {
        ListaUsuarios.ItemsSource = await _fotoDatabase.Getlista_usuario();
    }
}