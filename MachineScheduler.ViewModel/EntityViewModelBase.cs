using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MachineScheduler.DAL.Models;
using MachineScheduler.DAL.Parsers;

namespace MachineScheduler.ViewModel
{
    public abstract class EntityViewModelBase<T> : IEntityViewModel<T> where T : class, IEntity, new()
    {
        private string _filename;
        private protected ICommand _openFileCommand;
        private ObservableCollection<T> _items;
        private bool _filenameChanged;
        private bool _inputDataChanged;
        private bool _inputDataModifiedManually;
        protected readonly MainWindowViewModel ParentViewModel;
        protected bool UpdateDataInProgress { get; set; }
        protected Func<object, bool> CanExecute => obj =>
        {
            if (obj is not string filename) return false;
            if (!IsCorrectFilename(filename)) return false;
            if (!EntityParserFactory<T>.IsSupportedFiletype(Path.GetExtension(filename))) return false;
            Filename = filename;
            return true;
        };
        
        protected EntityViewModelBase(MainWindowViewModel parentViewModel)
        {
            ParentViewModel = parentViewModel;
            ParentViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName.Equals(nameof(ParentViewModel.Schedules)))
                {
                    _inputDataChanged = false;
                }
            };
            _filenameChanged = false;
            Filename = "";
            Items = new ObservableCollection<T>();
            Items.CollectionChanged += OnCollectionChanged; //TODO: Prob. memleak. Detect changes in grids via Loaded/Unloaded event. Or make grid read-only 
        }

        public string Filename
        {
            get => _filename;
            set
            {
                if (value == _filename) return;
                _filename = value;
                _filenameChanged = true;
                OnPropertyChanged(nameof(Filename));
            }
        }

        public ObservableCollection<T> Items
        {
            get => _items;
            set
            {
                if(value == _items) return;
                _items = value;
                _inputDataChanged = true;
                OnPropertyChanged(nameof(Items));
            }
        }

        private bool IsCorrectFilename(string filename)
        {
            return !string.IsNullOrWhiteSpace(filename) && File.Exists(filename);
        }
        
        public abstract ICommand OpenFileCommand { get; }

        protected bool WillExecute()
        {
            if (!_filenameChanged && !_inputDataChanged && !_inputDataModifiedManually) return false;
            if (Items.Count > 0)
                Items.Clear(); //TODO: Add ability to choose how to open file: Replace exists data or add to exists data
            _filenameChanged = false;
            return true;

        }
        public event PropertyChangedEventHandler PropertyChanged;
        [DAL.NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            // if (propertyName.Equals(nameof(Items)))
            //     _inputDataChanged = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        
        protected void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (!UpdateDataInProgress)
            {
                _inputDataChanged = true;
                _inputDataModifiedManually = true;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Items)));
            }
            else
            {
                _inputDataModifiedManually = false;
            }
            
           // CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(args.Action));
        }
    }
}
