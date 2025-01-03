using System;

namespace UD01PartV;
public partial class MainPage : ContentPage
{
    private string curso;
    private decimal precio;
    private string formaPago;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnSeleccionarCursoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PageCurso(this));
    }

    private async void OnSeleccionarFormaPagoClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new PagePago(this));
    }

    public void ActualizarCurso(string nombreCurso, decimal precioCurso)
    {
        curso = nombreCurso;
        precio = precioCurso;
        lblCurso.Text = curso;
        lblPrecio.Text = precio.ToString("C");
        VerificarCamposCompletos();
    }

    public void ActualizarFormaPago(string formaDePago)
    {
        formaPago = formaDePago;
        lblFormaPago.Text = formaPago;
        VerificarCamposCompletos();
    }

    private void VerificarCamposCompletos()
    {
        btnCalcularPrecio.IsEnabled = !string.IsNullOrWhiteSpace(curso) && precio > 0 && !string.IsNullOrWhiteSpace(formaPago);
    }

    private void OnCalcularPrecioClicked(object sender, EventArgs e)
    {
        decimal precioFinal = formaPago == "Con tarjeta" ? precio * 0.9m : precio;
        lblResultado.Text = $"El precio final es {precioFinal:C}";
    }
}
