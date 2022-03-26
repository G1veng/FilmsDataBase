using FilmsDataBase.ViewModels.Base;
using System.Windows.Input;
using FilmsDataBase.Infrastructure.Commands;
using FilmsDataBase.Services;
using System.Windows;

namespace FilmsDataBase.ViewModels
{
  
  internal class AddFilmViewModel : ViewModel
  {
    #region Properties
    private DisplayRootRegistry _displayRootRegistry;
    public DisplayRootRegistry DisplayRootRegistry
    {
      set { _displayRootRegistry = value; }
      get { return _displayRootRegistry; }
    }
    private string _title;
    public string Title { get { return _title; } set { _title = value; } }

    private string _description;
    public string Description { get { return _description; } set { _description = value; } }

    private string _icon;
    public string Icon { get { return _icon; } set { _icon = value; } }

    private string _trailer;
    public string Trailer { get { return _trailer; } set { _trailer = value; } }

    private string _year;

    public string Year { get { return _year; } set { _year = value; } }
    #endregion

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
      DisplayRootRegistry.HidePresentation(this);
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion

    #region SaveDataCommand
    public ICommand SaveDataCommand { get; }
    private void OnSaveDataCommandExecuted(object p)
    {
      
    }
    private bool CanSaveDataCommandExecute(object p) 
    {
      if (Title == string.Empty || Title == null) return false;
      if (Description == string.Empty || Description == null) return false;
      if (Icon == string.Empty || Icon == null) return false;
      if (Trailer == string.Empty || Trailer == null) return false;
      if (!int.TryParse(Year, out int year) || year <= 0) return false;
      return true;
    }
    #endregion


    public AddFilmViewModel()
    {
      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      SaveDataCommand = new LambdaCommand(OnSaveDataCommandExecuted, CanSaveDataCommandExecute);
      #endregion
    }
  }
}
