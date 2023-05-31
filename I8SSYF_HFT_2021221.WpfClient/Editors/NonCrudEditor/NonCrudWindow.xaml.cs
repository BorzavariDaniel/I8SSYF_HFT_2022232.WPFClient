using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace I8SSYF_HFT_2021221.WpfClient.Editors.NonCrudEditor
{
    /// <summary>
    /// Interaction logic for NonCrudWindow.xaml
    /// </summary>
    public partial class NonCrudWindow : Window
    {
        public NonCrudWindow()
        {
            InitializeComponent();

            var result = new RestService("http://localhost:64139/").Get<KeyValuePair<string, string>>("Method/AveragePriceByModels");
            foreach (var item in result)
            {
                listbox.Items.Add(item);
            }

            var result2 = new RestService("http://localhost:64139/").Get<KeyValuePair<string, string>>("Method/CylindersByDescending");
            foreach (var item in result2)
            {
                listbox2.Items.Add(item);
            }

            var result3 = new RestService("http://localhost:64139/").Get<KeyValuePair<string, string>>("Method/AverageNumberOfCylindersByModels");
            foreach (var item in result3)
            {
                listbox3.Items.Add(item);
            }

            var result4 = new RestService("http://localhost:64139/").Get<KeyValuePair<string, string>>("Method/SumPriceByModels");
            foreach (var item in result4)
            {
                listbox4.Items.Add(item);
            }

            var result5 = new RestService("http://localhost:64139/").Get<KeyValuePair<string, string>>("Method/CarCountByModels");
            foreach (var item in result5)
            {
                listbox5.Items.Add(item);
            }
        }
    }
}
