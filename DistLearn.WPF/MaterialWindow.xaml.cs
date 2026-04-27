using System.Windows;
using DistLearn;

namespace DistLearn.WPF
{
    public partial class MaterialWindow : Window
    {
        private Material material;

        public MaterialWindow(Material selectedMaterial)
        {
            InitializeComponent();
            material = selectedMaterial;
            LoadMaterial();
        }

        private void LoadMaterial()
        {
            if (material == null)
            {
                MessageBox.Show("Матеріал не знайдено.");
                Close();
                return;
            }

            TitleText.Text = material.Title;
            DescText.Text = material.Description;
            TypeText.Text = material.MaterialType;
            UrlText.Text = material.Url;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}