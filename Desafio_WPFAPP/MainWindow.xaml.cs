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
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Criar um objeto com os dados a serem enviados
                var dados = new
                {
                    Nome = boxnome.Text, // Aqui você pode pegar os valores dos campos de texto, por exemplo
                    Sobrenome = boxsobrenome.Text,
                    Telefone = boxtelefone.Text
                };

                // Converter os dados em JSON
                string jsonContent = JsonConvert.SerializeObject(dados);

                // Criar o conteúdo da requisição HTTP
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Defina a URL da sua API
                string url = "https://desafio.azurewebsites.net/api/Cadastro"; // Altere para a URL correta

                // Enviar os dados para a API via POST
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dados salvos com sucesso!");
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
            // Perguntar ao usuário se ele tem certeza
            var resultado = MessageBox.Show("Você tem certeza que deseja excluir este item?",
                                            "Confirmar Exclusão",
                                            MessageBoxButton.YesNo,
                                            MessageBoxImage.Warning);

            // Se o usuário clicar em "Yes", prosseguir com a exclusão
            if (resultado == MessageBoxResult.Yes)
            {
                try
                {
                    // Supondo que o ID seja obtido de um campo de entrada ou outra fonte
                    int id = int.Parse(box1.Text); // Aqui você vai pegar o ID que deseja excluir

                    // Defina a URL da sua API, incluindo o ID do item
                    string url = $"https://desafio.azurewebsites.net/api/Cadastro/{id}"; // Altere para o formato correto da sua API

                    // Realiza a requisição DELETE para excluir o item
                    HttpResponseMessage response = await _httpClient.DeleteAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Item excluído com sucesso!");
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
                // Caso o usuário clique em "No", a exclusão não ocorre
                MessageBox.Show("Exclusão cancelada.");
            }
        }
    }
}