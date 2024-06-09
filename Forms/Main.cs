using System;
using System.Windows.Forms;

namespace search.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Обработчик нажатия кнопки "Приступить к поиску"
        /// </summary>
        private void SearchButton_Click(object sender, EventArgs e)
        {
            var searchForm = new Search();
            searchForm.Show();
            this.Hide();
        }
    }

}
