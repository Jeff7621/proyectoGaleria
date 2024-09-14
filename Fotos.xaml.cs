namespace Galleria;

public partial class Fotos : ContentPage
{
    private FotoDatabase _fotoDatabase;

    public Fotos()
	{
		InitializeComponent();

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "fotos.db3");
        _fotoDatabase = new FotoDatabase(dbPath);
    }

    // Método que se ejecuta cada vez que la página aparece
    protected override void OnAppearing()
    {
        base.OnAppearing();
        CargarFotos();
    }


    private async void OnAgregarFotoClicked(object sender, EventArgs e)
    {
        if (App.CurrentUser == null)
        {
            await DisplayAlert("Error", "Debe iniciar sesión para agregar fotos.", "OK");
            await Navigation.PushAsync(new MainPage());
            return;
        }

        var result = await FilePicker.Default.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Selecciona una foto"
        });

        if (result != null)
        {
            var newFoto = new Foto
            {
                Nombre = result.FileName,
                RutaImagen = result.FullPath,
                Fecha = DateTime.Now,
                UsuarioId = App.CurrentUser.Id // Asigna el ID del usuario actual
            };

            await _fotoDatabase.SaveFotoAsync(newFoto);
            CargarFotos();
        }
    }



    private async void CargarFotos()
    {
        if (App.CurrentUser == null)
        {
            await DisplayAlert("Error", "No se ha identificado ningún usuario.", "OK");
            await Navigation.PushAsync(new MainPage());
            return;
        }

        List<Foto> fotos = await _fotoDatabase.GetFotosAsync();
        // Filtrar fotos por el UsuarioId actual
        FotosCollectionView.ItemsSource = fotos.Where(f => f.UsuarioId == App.CurrentUser.Id).ToList();
    }

    private async void OnFotoTapped(object sender, EventArgs e)
    {
        // Obtener el ID de la foto desde CommandParameter
        int fotoId = 0;
        if (sender is Image image && image.GestureRecognizers.FirstOrDefault() is TapGestureRecognizer tapGesture)
        {
            if (tapGesture.CommandParameter is int id)
            {
                fotoId = id;
            }
        }

        await Navigation.PushAsync(new DetallePage(fotoId));
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());

        // Opcional: Remover páginas anteriores del stack
        Navigation.RemovePage(this);
    }
}