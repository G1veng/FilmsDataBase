using FilmsDataBase.ViewModels.Base;
using System.Windows.Input;
using FilmsDataBase.Infrastructure.Commands;
using System.Windows;

namespace FilmsDataBase.ViewModels
{
  internal class MainWindowViewModel : ViewModel
  {
    private int _id = 0;
    private string _Title = "Человек-паук: Нет пути домой";
    private string _Description = "Жизнь и репутация Питера Паркера оказываются под угрозой, поскольку Мистерио раскрыл всему миру тайну личности Человека-паука. Пытаясь исправить ситуацию, Питер обращается за помощью к Стивену Стрэнджу, но вскоре всё становится намного опаснее.";
    private string _Icon = @"D:\4 семестр\РПС\FilmsDataBase\Spider Man.jpg";
    private string _Trailer = @"D:\4 семестр\РПС\FilmsDataBase\SPIDER-MAN_ NO WAY HOME - Official Trailer (HD).mp4";
    private int _Year = 2021;

    #region Commands
    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get;}
    private void OnCloseApplicationCommandExecuted(object p)
    {
      Application.Current.Shutdown();
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion
    #endregion
    public MainWindowViewModel()
    {
      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      #endregion
    }
    /// <summary>
    /// Title of film
    /// </summary>
    public string Title
    {
      get => _Title;
      set => Set(ref _Title, value);
    }
    /// <summary>
    /// Description of film
    /// </summary>
    public string Description
    {
      get => _Description;
      set => Set(ref _Description, value);
    }
    /// <summary>
    /// Icon of film
    /// </summary>
    public string Icon
    {
      get => _Icon;
      set => Set(ref _Icon, value);
    }
    /// <summary>
    /// Trailer of film
    /// </summary>
    public string Trailer
    {
      get => _Trailer;
      set => Set(ref _Trailer, value);
    }
    /// <summary>
    /// Year of film came out
    /// </summary>
    public int Year
    {
      get => _Year;
      set => Set(ref _Year, value);
    }
    /// <summary>
    /// Id of film data base
    /// </summary>
    public int Id
    {
      get => _id;
      set => Set(ref _id, value);
    }
  }
}
