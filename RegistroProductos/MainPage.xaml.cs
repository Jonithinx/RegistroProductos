using RegistroProductos.Data;
using RegistroProductos.Models;

namespace RegistroProductos
{
    public partial class MainPage : ContentPage
    {
        DatabaseService _dbService;

        public MainPage()
        {
            InitializeComponent();
            _dbService = new DatabaseService();
        }

        private async void OnGuardarClicked(object sender, EventArgs e)
        {
            // Validación para que no te metan basura
            if (string.IsNullOrWhiteSpace(NombreEntry.Text) ||
                string.IsNullOrWhiteSpace(DescripcionEntry.Text) ||
                string.IsNullOrWhiteSpace(PrecioEntry.Text) ||
                string.IsNullOrWhiteSpace(CantidadEntry.Text))
            {
                await DisplayAlert("Error", "No seás maje, llená todos los campos.", "OK");
                return;
            }

            if (!decimal.TryParse(PrecioEntry.Text, out decimal precio) ||
                !int.TryParse(CantidadEntry.Text, out int cantidad))
            {
                await DisplayAlert("Error", "Precio y Cantidad deben ser números válidos.", "OK");
                return;
            }

            var nuevoProducto = new Producto
            {
                Nombre = NombreEntry.Text,
                Descripcion = DescripcionEntry.Text,
                Precio = precio,
                Cantidad = cantidad,
                FechaRegistro = DateTime.Now // Captura la fecha actual solita
            };

            await _dbService.GuardarProductoAsync(nuevoProducto);

            await DisplayAlert("Éxito", "Producto guardado cheque en SQLite.", "OK");

            // Limpiar campos después de guardar
            NombreEntry.Text = string.Empty;
            DescripcionEntry.Text = string.Empty;
            PrecioEntry.Text = string.Empty;
            CantidadEntry.Text = string.Empty;
        }
    }
}