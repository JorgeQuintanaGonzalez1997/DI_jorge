namespace UD01PartIII;

public partial class Pagina5 : ContentPage
{
    string producto;
    int cantidad;
    string direccion;
    
    public Pagina5(string producto, int cantidad,string direccion)
    {
        InitializeComponent();

        this.producto = producto;
        this.cantidad = cantidad;
        this.direccion = direccion;
        sResumen.Text = "Producto: " + producto + "\n" +
            "Cantidad: " + cantidad + "\n" +
            "Dirección: " + direccion;
       
    }
    private async void BtnVolver_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

}