using System.Windows.Input;
using FilmsDataBase.ViewModels.Base;
using FilmsDataBase.Infrastructure.Commands;

namespace FilmsDataBase.ViewModels
{
  public class InformationAboutFilmWindowViewModel : ViewModel
  {
    #region Properties
    private DisplayRootRegistry _displayRootRegistry;
    public DisplayRootRegistry DisplayRootRegistry
    {
      set { _displayRootRegistry = value; }
      get { return _displayRootRegistry; }
    }
    private string _title;
    public string Title { get => _title; set => Set(ref _title, value); }

    private string _description;
    public string Description { get => _description; set => Set(ref _description, value); }

    private string _icon;
    public string Icon { get => _icon; set => Set(ref _icon, value); }

    private string _trailer;
    public string Trailer { get => _trailer; set => Set(ref _trailer, value); }

    private System.DateTime _year;
    public System.DateTime Year { get => _year; set => Set(ref _year, value); }
    //private int _intYear;
    /*public int IntYear { get => _intYear; set => Set(ref _intYear, value); }*/
    private string _oldTitle;
    public string OldTitle { get => _oldTitle; set => Set(ref _oldTitle, value); }
    #endregion

    #region Commands

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
      DisplayRootRegistry.HidePresentation(this);
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion

    #endregion

    public InformationAboutFilmWindowViewModel()
    {
      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      #endregion
    }
  }
}
