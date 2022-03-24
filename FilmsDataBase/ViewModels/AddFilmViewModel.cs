using FilmsDataBase.ViewModels.Base;
using System.Windows.Input;
using FilmsDataBase.Infrastructure.Commands;
using System.Windows;
using System.Collections.Generic;
using FilmsDataBase.Models;
using System;
using FilmsDataBase.ViewModels.Base;

namespace FilmsDataBase.ViewModels
{
  
  internal class AddFilmViewModel : ViewModel
  {
    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
      if (p is Window window)
        window.Close();
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion
    public AddFilmViewModel()
    {
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
    }
  }
}
