using BVallejoT5.Models;
using Xamarin.Google.Crypto.Tink.Mac;

namespace BVallejoT5.Views;

public partial class VInicio : ContentPage
{
	public VInicio()
	{
		InitializeComponent();
	}

    private void btnInsert_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(lblId.Text))
        {
            updatePerson();
        }
        else
        {
            insertPerson();
        }

    }

    private void btnObtener_Clicked(object sender, EventArgs e)
    {

        lblStatus.Text = "";
        List<Persona> personas = App.personRepo.GetAllPersonas();
        ListarPersona.ItemsSource = personas;
        lblStatus.Text = App.personRepo.StatusMsg;
    }

    private void onChangeItem(object sender, SelectionChangedEventArgs e)
    {
        txtName.Text = (e.CurrentSelection.FirstOrDefault() as Persona)?.Name;
        lblId.Text = (e.CurrentSelection.FirstOrDefault() as Persona)?.Id.ToString();
        

    }
    private void insertPerson()
    {
        lblStatus.Text = "";
        App.personRepo.AddPerson(txtName.Text);
        lblStatus.Text = App.personRepo.StatusMsg;
        resetForm();
    }
    private void updatePerson()
    {
        lblStatus.Text = "";
        int id = int.Parse(lblId.Text);
        App.personRepo.EditPerson(id, txtName.Text);
        lblStatus.Text = App.personRepo.StatusMsg;
        resetForm();
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        SwipeItem swipeItem = sender as SwipeItem;
        var item = swipeItem.CommandParameter as Persona;

        App.personRepo.DeletePerson(item.Id);
        lblStatus.Text = App.personRepo.StatusMsg;
        resetForm();
    }
    private void resetForm()
    {
        lblId.Text = "";
        txtName.Text = "";
    }

}