using FilmsDataBase.ViewModels.Base;
using System.Windows.Input;
using FilmsDataBase.Infrastructure.Commands;
using System.Windows;

namespace FilmsDataBase.ViewModels
{
  
  internal class AddFilmViewModel : ViewModel
  {
    private DisplayRootRegistry _displayRootRegistry;
    public DisplayRootRegistry DisplayRootRegistry
    {
      set { _displayRootRegistry = value; }
      get { return _displayRootRegistry; }
    }
    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
      DisplayRootRegistry.HidePresentation(this);
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion
    public AddFilmViewModel()
    {
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
    }
  }
}
