using DAN_XLVI_Milica_Karetic.Model;
using DAN_XLVI_Milica_Karetic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DAN_XLVI_Milica_Karetic.View
{
    /// <summary>
    /// Interaction logic for AddWorker.xaml
    /// </summary>
    public partial class AddWorker : Window
    {
        /// <summary>
        /// Window constructor for editing workers
        /// </summary>
        /// <param name="workerEdit">worker that is bing edited</param>
        public AddWorker(vwUser userEdit)
        {
            InitializeComponent();
            this.DataContext = new AddWorkerViewModel(this, userEdit);
        }

        /// <summary>
        /// Window constructor for creating workers
        /// </summary>
        public AddWorker()
        {
            InitializeComponent();
            this.DataContext = new AddWorkerViewModel(this);
        }

        /// <summary>
        /// User can only imput numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        /// <summary>
        /// Calcualtes the birth date and places it in the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            InputCalculator iv = new InputCalculator();
            string text = "";
            if (txtJMBG.Text.Length >= 7)
            {
                text = iv.CountDateOfBirth(txtJMBG.Text).ToString("dd.MM.yyy.");
            }
            else
            {
                text = "";
            }

            txtDateOfBirth.Text = text;
        }
    }
}
