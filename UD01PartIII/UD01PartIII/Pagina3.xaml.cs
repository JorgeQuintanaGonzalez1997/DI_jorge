namespace UD01PartIII;

public partial class Pagina3 : ContentPage
{
	
	string producto;
	/*public Pagina3()
	{
		InitializeComponent();
	}
	*/
	public Pagina3( string producto)
	{
        InitializeComponent();
		
		this.producto = producto;
    }
	private async void BtnVolver_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
	private async void BtnPagina4_Clicked(object sender, EventArgs e)
	{
		int cantidad =int.Parse(eValor.Text);
		await Navigation.PushAsync(new Pagina4(producto, cantidad));
	}
}