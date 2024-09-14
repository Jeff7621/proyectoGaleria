using System.Threading.Tasks;
namespace Galleria;

public partial class DetallePage : ContentPage
{
    private FotoDatabase _fotoDatabase;
    private Foto _foto;


    public DetallePage(int fotoId)
	{
		InitializeComponent();


        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "fotos.db3");
        _fotoDatabase = new FotoDatabase(dbPath);

        CargarFoto(fotoId);
    }


    private async void CargarFoto(int fotoId)
    {
        _foto = await _fotoDatabase.GetFotosAsync().ContinueWith(task => task.Result.Find(f => f.Id == fotoId));
        if (_foto != null)
        {
            DetalleImagen.Source = _foto.RutaImagen;
            DetalleNombre.Text = _foto.Nombre;
            DetalleFecha.Text = _foto.Fecha.ToString("d");
        }
    }

    private async void OnEliminarFotoClicked(object sender, EventArgs e)
    {
        
        if (_foto != null)
        {
            bool confirmar = await DisplayAlert("Eliminar Foto", "¿Está seguro que desea eliminar esta foto?", "OK", "cancel");
          

            if (confirmar)
            {
                await _fotoDatabase.DeleteFotoAsync(_foto);
                await Navigation.PopAsync();

            }
        }
       
    }

}