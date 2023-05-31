using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using I8SSYF_HFT_2021221.WpfClient.Editors.CarEditor;
using I8SSYF_HFT_2021221.WpfClient.Editors.EngineEdior;
using I8SSYF_HFT_2021221.WpfClient.Editors.ModelEditor;
using I8SSYF_HFT_2021221.WpfClient.Editors.NonCrudEditor;

namespace I8SSYF_HFT_2021221.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        public ICommand CarCommand { get; set; }
        public ICommand EngineCommand { get; set; }
        public ICommand ModelCommand { get; set; }
        public ICommand MethodCommand { get; set; }
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
                CarCommand = new RelayCommand(() => new CarEditor().ShowDialog());
                EngineCommand = new RelayCommand(() => new EngineEditor().ShowDialog());
                ModelCommand = new RelayCommand(() => new ModelEditor().ShowDialog());
                MethodCommand = new RelayCommand(() => new NonCrudWindow().ShowDialog());
            }
        }
    }
}
