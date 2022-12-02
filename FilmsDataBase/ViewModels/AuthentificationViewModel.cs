using FilmsDataBase.ViewModels.Base;
using FilmsDataBase.Services;
using FilmsDataBase.Infrastructure.Commands;
using System.Windows.Input;
using System.Windows;

namespace FilmsDataBase.ViewModels
{
  public class AuthentificationViewModel : ViewModel 
  {
    #region Properies
    private int _counter = 0;
    private string _token;
    private string _login;
    private string _password;
    private DisplayRootRegistry _displayRootRegistry;

    public bool IsRegistrated = false;
    public DisplayRootRegistry DisplayRootRegistry
    {
      set { _displayRootRegistry = value; }
      get { return _displayRootRegistry; }
    }
    public string Login { get => _login; set => Set(ref _login, value); }
    public string Password { get => _password; set => Set(ref _password, value); }
    //public string Token { get => _token; set => Set(ref _token, value); }
    #endregion
    private MessageBoxResult Alarm(string message, string caption, MessageBoxButton button, MessageBoxImage icon) =>
      System.Windows.MessageBox.Show(message, caption, button, icon);

    #region Commands

    #region TerminateApllicationCommand
    public ICommand TerminateApllicationCommand { get; }
    private bool CanTerminateApllicationCommandExecute(object p) => true;
    private void OnTerminateApllicationCommandExecuted(object p)
    {
      System.Environment.Exit(0);
    }
    #endregion

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get; }
    private void OnCloseApplicationCommandExecuted(object p)
    {
      DisplayRootRegistry.HidePresentation(this);
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion

    #region VerifyAuthentificationCommand
    public ICommand VerifyAuthentificationCommand { get; }
    private bool CanVerifyAuthentificationCommandExecute(object p)
    {
      if (Login == string.Empty || Login == null) return false;
      if (Password == string.Empty || Password == null) return false;
      return true;
    }
    private void OnVerifyAuthentificationCommandExecuted(object p)
    {
      if (_counter == 3)
      {
        CapthcaViewModel capthcaViewModel = new CapthcaViewModel();
        capthcaViewModel.DisplayRootRegistry = DisplayRootRegistry;
        DisplayRootRegistry.ShowDialogPresentation(capthcaViewModel);
        _counter = 0;
      }
      _counter++;
      var token = CheckUserDataService.CheckUserData(Password, Login);
      if (!token.Contains("Not found"))
      {
        IsRegistrated = true;
        Properties.Settings.Default.token = token;
        OnCloseApplicationCommandExecuted(p);
      }
      else
      {
        if (Alarm("Wrong email or password", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Warning) == MessageBoxResult.OK)
          Properties.Settings.Default.token = string.Empty;
      }
    }
    #endregion

    #endregion
    public AuthentificationViewModel()
    {
      #region Commands
      VerifyAuthentificationCommand = new LambdaCommand(OnVerifyAuthentificationCommandExecuted, CanVerifyAuthentificationCommandExecute);
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      TerminateApllicationCommand = new LambdaCommand(OnTerminateApllicationCommandExecuted, CanTerminateApllicationCommandExecute);
      #endregion
    }
  }
}
