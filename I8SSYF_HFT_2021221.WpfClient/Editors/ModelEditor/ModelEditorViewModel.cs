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

namespace I8SSYF_HFT_2021221.WpfClient.Editors.ModelEditor
{
    public class ModelEditorViewModel : ObservableRecipient
    {
        public RestCollection<Model> Models { get; set; }
        private Model selectedModel;

        public Model SelectedModel
        {
            get { return selectedModel; }
            set
            {
                if (value != null)
                {
                    selectedModel = new Model()
                    {
                        Shape = value.Shape,
                        ModelId = value.ModelId
                    };
                }
                OnPropertyChanged();
                (DeleteModelCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CreateModelCommand { get; set; }
        public ICommand DeleteModelCommand { get; set; }
        public ICommand UpdateModelCommand { get; set; }
        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public ModelEditorViewModel()
        {
            if (!IsInDesignMode)
            {
                Models = new RestCollection<Model>("http://localhost:64139/", "Model", "hub");
                CreateModelCommand = new RelayCommand(() =>
                {
                    Models.Add(new Model()
                    {
                        Shape = SelectedModel.Shape,
                    });
                });

                UpdateModelCommand = new RelayCommand(() =>
                {
                    Models.Update(SelectedModel);
                });

                DeleteModelCommand = new RelayCommand(() =>
                {
                    Models.Delete(SelectedModel.ModelId);
                },
                () =>
                {
                    return SelectedModel != null;
                });
                SelectedModel = new Model();
            }
        }
    }
}
