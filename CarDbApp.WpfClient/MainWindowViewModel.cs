using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace I8SSYF_HFT_2021221.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public ICommand CarsCommand { get; set; }
        public ICommand EnginesCommand { get; set; }
        public ICommand ModelsCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                CarsCommand = new RelayCommand(() => new BrandEditor.BrandWindow().ShowDialog());
                PhonesCommand = new RelayCommand(() => new PhonesEditor.PhonesWindow().ShowDialog());
                EmployeesCommand = new RelayCommand(() => new EmployeesEditor.EmployeeWindow().ShowDialog());
            }

        }
    }
}
