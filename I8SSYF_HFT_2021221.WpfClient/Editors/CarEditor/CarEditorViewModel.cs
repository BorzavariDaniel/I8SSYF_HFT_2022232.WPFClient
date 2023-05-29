using I8SSYF_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace I8SSYF_HFT_2021221.WpfClient.Editors.CarEditor
{
    public class CarEditorViewModel : ObservableRecipient
    {
        public RestCollection<Car> Cars { get; set; }
        private Car selectedCar;

        public Car SelectedCar
        {
            get { return selectedCar; }
            set
            {
                if (value != null)
                {
                    selectedCar = new Car()
                    {
                        Name = value.Name,
                        Price = value.Price,
                        Id = value.Id,
                    };
                }
                OnPropertyChanged();
                (DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateCarCommand { get; set; }
        public ICommand DeleteCarCommand { get; set; }
        public ICommand UpdateCarCommand { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public CarEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Cars = new RestCollection<Car>("http://localhost:64139/", "Car");
                CreateCarCommand = new RelayCommand(() =>
                {
                    Cars.Add(new Car()
                    {
                        Name = SelectedCar.Name,                       
                    });
                });

                UpdateCarCommand = new RelayCommand(() =>
                {
                    Cars.Update(SelectedCar);
                });

                DeleteCarCommand = new RelayCommand(() =>
                {
                    Cars.Delete(SelectedCar.Id);
                },
                () =>
                {
                    return SelectedCar != null;
                });
                SelectedCar = new Car();
            }
        }
    }
}
