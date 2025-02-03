using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desafio_WPFAPP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient _httpClient;
        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            CarregarDados();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                var dados = new
                {
                    Nome = boxnome.Text, 
                    Sobrenome = boxsobrenome.Text,
                    Telefone = boxtelefone.Text
                };

                string jsonContent = JsonConvert.SerializeObject(dados);
                
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                string url = "https://desafio.azurewebsites.net/api/Cadastro"; 

                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dados salvos com sucesso!");
                    CarregarDados();
                    boxnome.Clear();
                    boxsobrenome.Clear();
                    boxtelefone.Clear();
                }
                else
                {
                    MessageBox.Show("Erro ao salvar os dados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }

        private async void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var resultado = MessageBox.Show("Você tem certeza que deseja excluir este item?",
                                            "Confirmar Exclusão",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Warning);

            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    int id = int.Parse(box1.Text); 

                    string url = $"https://desafio.azurewebsites.net/api/Cadastro/{id}"; 

                    HttpResponseMessage response = await _httpClient.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Item excluído com sucesso!");
                        CarregarDados();

                    }
                    else
                    {
                        MessageBox.Show("Erro ao excluir o item.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocorreu um erro: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Exclusão cancelada.");
            }
        }

        private async void btnAlterar_Click(object sender, RoutedEventArgs e)
        {

            string nome = boxnome.Text; 
            string sobrenome = boxsobrenome.Text; 
            string telefone = boxtelefone.Text; 

            int id = int.Parse(box1.Text); 

            var dados = new
            {
                Nome = nome,
                Sobrenome = sobrenome,
                Telefone = telefone
            };

            string jsonContent = JsonConvert.SerializeObject(dados);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            string url = $"https://desafio.azurewebsites.net/api/Cadastro/{id}"; 

            try
            {
                HttpResponseMessage response = await _httpClient.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dados alterados com sucesso!");
                    CarregarDados();
                    boxnome.Clear();
                    boxsobrenome.Clear();
                    boxtelefone.Clear();
                    box1.Clear();
                }
                else
                {
                    MessageBox.Show("Erro ao alterar os dados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }

        private async void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string url = "https://desafio.azurewebsites.net/api/Cadastro"; 

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    List<Usuario> itens = JsonConvert.DeserializeObject<List<Usuario>>(jsonResponse);

                    dtDados.ItemsSource = itens;
                }
                else
                {
                    MessageBox.Show("Erro ao buscar os dados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }
        private async void CarregarDados()
        {
            try
            {
            
                string url = "https://desafio.azurewebsites.net/api/Cadastro"; 

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    List<Usuario> itens = JsonConvert.DeserializeObject<List<Usuario>>(jsonResponse);

                    dtDados.ItemsSource = itens;
                }
                else
                {
                    MessageBox.Show("Erro ao buscar os dados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }

        public class Usuario
        {
            public int Id { get; set; }
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
            public string Telefone { get; set; }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string id = box1.Text; 

            string url = $"https://desafio.azurewebsites.net/api/Cadastro/{id}"; 

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    Usuario usuario = JsonConvert.DeserializeObject<Usuario>(jsonResponse);

                    boxnome.Text = usuario.Nome;
                    boxsobrenome.Text = usuario.Sobrenome;
                    boxtelefone.Text = usuario.Telefone;
                }
                else
                {
                    MessageBox.Show("Erro ao buscar os dados.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro: {ex.Message}");
            }
        }

    }
}
