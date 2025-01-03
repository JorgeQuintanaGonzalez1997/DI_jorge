using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace UD01PartV;
public partial class PageCurso : ContentPage
{
    private MainPage mainPage;

    public PageCurso(MainPage page)
    {
        InitializeComponent();
        mainPage = page;
    }

    private async void OnCurso1Clicked(object sender, EventArgs e)
    {
        mainPage.ActualizarCurso("Electricista", 100);
        await Navigation.PopAsync();
    }

    private async void OnCurso2Clicked(object sender, EventArgs e)
    {
        mainPage.ActualizarCurso("Jardinería", 150);
        await Navigation.PopAsync();
    }
}