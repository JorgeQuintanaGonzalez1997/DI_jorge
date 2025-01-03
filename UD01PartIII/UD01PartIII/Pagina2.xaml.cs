namespace UD01PartIII;

public partial class Pagina2 : ContentPage
{
    string nombre;

   
    public Pagina2()
    {
        InitializeComponent();
    }

    private async void BtnVolver_Clicked(object sender, EventArgs e)
    {
        // Volver a la página principal
        await Navigation.PopAsync();
    }

    private async void BtnPagina3_Clicked(object sender, EventArgs e)
    {
        string producto;
        producto = eValor.Text;
        await Navigation.PushAsync(new Pagina3(producto));
    }
}