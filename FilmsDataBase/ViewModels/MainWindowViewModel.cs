using FilmsDataBase.ViewModels.Base;
using System.Windows.Input;
using FilmsDataBase.Infrastructure.Commands;
using System.Windows;
using System.Collections.Generic;
using FilmsDataBase.Models;
using FilmsDataBase;

namespace FilmsDataBase.ViewModels
{
  internal class MainWindowViewModel : ViewModel
  {
    #region SelectedWindow
    private DisplayRootRegistry displayRootRegistry;
    private AddFilmViewModel addFilmViewModel;
    #endregion
    public List<Film> Films { get;}
    public bool isOpened { get; set; }
    #region Commands

    #region OpenInnerWindowComman 
    public ICommand OpenInnerWindowCommand { get; }
    private bool CanOpenInnerWindowCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(addFilmViewModel);
    private void OnOpenInnerWindowCommandExecuted(object p)
    {
      addFilmViewModel = new AddFilmViewModel();
      addFilmViewModel.DisplayRootRegistry = displayRootRegistry;
      displayRootRegistry.ShowPresentation(addFilmViewModel);
    }
    #endregion

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get;}
    private void OnCloseApplicationCommandExecuted(object p)
    {
      if (p is Window window)
        window.Close();
    }
    private bool CanCloseApplicationCommandExecute(object p) => true;
    #endregion


    #endregion
    public MainWindowViewModel()
    {
      displayRootRegistry = (Application.Current as App).displayRootRegistry;
      Films = new List<Film>() 
      { new Film{
          Title = "Человек-паук: Нет пути домой",
          Description = "Жизнь и репутация Питера Паркера оказываются под угрозой, поскольку Мистерио раскрыл всему миру тайну личности Человека-паука. Пытаясь исправить ситуацию, Питер обращается за помощью к Стивену Стрэнджу, но вскоре всё становится намного опаснее.",
          Icon = @"D:\4 семестр\РПС\FilmsDataBase\Spider Man.jpg",
          Trailer = @"D:\4 семестр\РПС\FilmsDataBase\SPIDER-MAN_ NO WAY HOME - Official Trailer (HD).mp4",
          Year = 2021,
        }
      };
      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      OpenInnerWindowCommand = new LambdaCommand(OnOpenInnerWindowCommandExecuted, CanOpenInnerWindowCommandExecute);
      #endregion
    }
  }
}
