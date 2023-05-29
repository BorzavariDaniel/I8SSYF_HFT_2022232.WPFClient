using I8SSYF_HFT_2021221.Models;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace I8SSYF_HFT_2021221.WpfClient.Editors.EngineEdior
{
    public class EngineEditorViewModel : ObservableRecipient
    {
        public RestCollection<Engine> Engines { get; set; }
        private Engine selectedEngine;

        public Engine SelectedEngine
        {
            get { return selectedEngine; }
            set
            {
                if (value != null)
                {
                    selectedEngine = new Engine()
                    {
                        Fuel = value.Fuel,
                        NumOfCylinders = value.NumOfCylinders,
                        EngineId = value.EngineId,
                    };
                }
                OnPropertyChanged();
                (DeleteEngineCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateEngineCommand { get; set; }
        public ICommand DeleteEngineCommand { get; set; }
        public ICommand UpdateEngineCommand { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public EngineEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Engines = new RestCollection<Engine>("http://localhost:64139/", "Engine");
                CreateEngineCommand = new RelayCommand(() =>
                {
                    Engines.Add(new Engine()
                    {
                        Fuel = SelectedEngine.Fuel,
                        NumOfCylinders= SelectedEngine.NumOfCylinders
                    });
                });

                UpdateEngineCommand = new RelayCommand(() =>
                {
                    Engines.Update(SelectedEngine);
                });

                DeleteEngineCommand = new RelayCommand(() =>
                {
                    Engines.Delete(SelectedEngine.EngineId);
                },
                () =>
                {
                    return SelectedEngine != null;
                });
                SelectedEngine = new Engine();
            }
        }
    }
}
