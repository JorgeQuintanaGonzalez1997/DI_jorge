namespace UD01PartIII;

public partial class Pagina4 : ContentPage
{
    
    string producto;
    int cantidad;
    public Pagina4()
    {
        InitializeComponent();
    }
    public Pagina4(string producto, int cantidad)
    {
        InitializeComponent();
        
        this.producto = producto;
        this.cantidad= cantidad;
    }
    private async void BtnVolver_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    private async void BtnPagina5_Clicked(object sender, EventArgs e)
    {
        string direccion = eValor.Text;
        await Navigation.PushAsync(new Pagina5(producto, cantidad,direccion));
    }
}