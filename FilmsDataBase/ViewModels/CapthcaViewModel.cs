using FilmsDataBase.ViewModels.Base;
using System.Windows.Input;
using FilmsDataBase.Infrastructure.Commands;
using System.Windows;
using FilmsDataBase.Data;
using FilmsDataBase.Models;

namespace FilmsDataBase.ViewModels
{
  public class CapthcaViewModel : ViewModel
  {
    #region Properies
    private int _wrongCounter = 0;
    private Captcha _captcha;
    private string _answer;
    private DisplayRootRegistry _displayRootRegistry;

    public bool IsRegistrated = false;
    public DisplayRootRegistry DisplayRootRegistry
    {
      set { _displayRootRegistry = value; }
      get { return _displayRootRegistry; }
    }
    public string Answer { get => _answer; set => Set(ref _answer, value); }
    public string Path { get; }
    #endregion
    private MessageBoxResult Alarm(string message, string caption, MessageBoxButton button, MessageBoxImage icon) =>
      System.Windows.MessageBox.Show(message, caption, button, icon);

    #region Commands

    #region SubmitInputCommand
    public ICommand SubmitInputCommand { get; }
    private bool CanSubmitInputCommandExecute(object p) => true;
    private void OnSubmitInputCommandExecuted(object p)
    {
      if(Answer == _captcha.Answer)
      {
        IsRegistrated = true;
        OnCloseApplicationCommandExecuted(p);
      }
      else
      {
        if(_wrongCounter == 3)
        {
          Alarm("You are out of attempts, bye bye", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Warning);
          OnTerminateApllicationCommandExecuted(p);
        }
        else
        {
          _wrongCounter++;
        }
        Alarm("Wrong", "Word Processor", MessageBoxButton.OK, MessageBoxImage.Warning);
      }
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

    #region TerminateApllicationCommand
    public ICommand TerminateApllicationCommand { get; }
    private bool CanTerminateApllicationCommandExecute(object p) => true;
    private void OnTerminateApllicationCommandExecuted(object p)
    {
      System.Environment.Exit(0);
    }
    #endregion

    #endregion

    public CapthcaViewModel()
    {
      _captcha = CaptchaRepository.GetRandomCaptcha();
      Path = "D:\\4 семестр\\РПС\\FilmsDataBase\\FilmsDataBase\\Saved\\Capthca\\" + _captcha.Path;

      #region Commands
      SubmitInputCommand = new LambdaCommand(OnSubmitInputCommandExecuted, CanSubmitInputCommandExecute);
      TerminateApllicationCommand = new LambdaCommand(OnTerminateApllicationCommandExecuted, CanTerminateApllicationCommandExecute);
      #endregion
    }
  }
}
