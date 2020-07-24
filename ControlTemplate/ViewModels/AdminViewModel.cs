using ControlTemplate.Models;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;

namespace ControlTemplate.ViewModels
{
    public class AdminViewModel:ViewModelBase
    {
        public AdminViewModel()
        {
            _usersList.Connect().Bind(out _users).Subscribe();
            DeleteCommand = ReactiveCommand.Create(DeleteUser);
            CloseCommand = ReactiveCommand.Create(() => Unit.Default);
        }

      

        private ReadOnlyObservableCollection<User> _users;

        public ReadOnlyObservableCollection<User> Users
        {
            get
            {
                return _users;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _users, value);
            }
        }

        private SourceList<User> _usersList = new SourceList<User>();

        public ReactiveCommand<Unit,Unit> CloseCommand { get; }

        public ReactiveCommand<Unit,Unit> DeleteCommand { get; }

        private void DeleteUser()
        {
            throw new NotImplementedException();
        }
    }
}
