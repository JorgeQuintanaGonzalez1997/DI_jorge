using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace UD01PartV;
public partial class PagePago : ContentPage
{
    private MainPage mainPage;

    public PagePago(MainPage page)
    {
        InitializeComponent();
        mainPage = page;
    }

    private async void OnEfectivoClicked(object sender, EventArgs e)
    {
        mainPage.ActualizarFormaPago("Al contado");
        await Navigation.PopAsync();
    }

    private async void OnTarjetaClicked(object sender, EventArgs e)
    {
        mainPage.ActualizarFormaPago("Con tarjeta");
        await Navigation.PopAsync();
    }
}