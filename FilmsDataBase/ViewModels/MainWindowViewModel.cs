using FilmsDataBase.ViewModels.Base;
using System.Windows.Input;
using FilmsDataBase.Infrastructure.Commands;
using System.Windows;
using FilmsDataBase.Models;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.IO;
using System;
using FilmsDataBase.Infrastructure;
using System.Collections.Generic;
using Autofac;
using FilmsDataBase.Data;
using FilmsDataBase.Services;

namespace FilmsDataBase.ViewModels
{
  public class MainWindowViewModel : ViewModel
  {
    #region Autofac
    private static IFilmService _concentrationService = null;
    public MainWindowViewModel(IFilmService concentrationService)
    {
      _concentrationService = concentrationService ?? throw new ArgumentNullException(nameof(concentrationService));
    }
    #endregion
    private void NotificationFileUpdated()
    {
      System.Windows.MessageBox.Show(
        "File updated",
        "Notification",
        (MessageBoxButton)MessageBoxButtons.OK,
        (MessageBoxImage)MessageBoxIcon.Information,
        (MessageBoxResult)MessageBoxDefaultButton.Button1,
        System.Windows.MessageBoxOptions.DefaultDesktopOnly);
    }

    #region SelectedWindow
    private DisplayRootRegistry displayRootRegistry;
    private AddFilmViewModel addFilmViewModel;
    private InformationAboutFilmWindowViewModel informationAboutFilmWindowViewModel;
    private GreetingsWindowViewModel greetingsWindowViewModel;
    #endregion

    #region Properties

    #region Film
    private ObservableCollection<Film> _films;
    public ObservableCollection<Film> Films { get => _films; set => Set(ref _films, value); }
    #endregion

    #region WhichFilm
    private Film _whichFilm;
    public Film WhichFilm { get => _whichFilm; set => Set(ref _whichFilm, value); }
    #endregion

    #endregion

    #region Commands

    #region InformationAboutFilmCommand
    public ICommand InformationAboutFilmCommand { get; }
    private bool CanInformationAboutFilmCommandExecute(object p) => WhichFilm != null && !_concentrationService.IsEmpty() &&
      !displayRootRegistry.CheckForExistingWindows(informationAboutFilmWindowViewModel);
    private void OnInformationAboutFilmCommandExecuted(object p)
    {
      if (informationAboutFilmWindowViewModel == null)
        informationAboutFilmWindowViewModel = new InformationAboutFilmWindowViewModel();
      informationAboutFilmWindowViewModel.Title = WhichFilm.Title;
      informationAboutFilmWindowViewModel.Description = WhichFilm.Description;
      informationAboutFilmWindowViewModel.Icon = WhichFilm.Icon;
      informationAboutFilmWindowViewModel.Trailer = WhichFilm.Trailer;
      informationAboutFilmWindowViewModel.Year = WhichFilm.Year;
      informationAboutFilmWindowViewModel.DisplayRootRegistry = displayRootRegistry;
      displayRootRegistry.ShowPresentation(informationAboutFilmWindowViewModel);
    }
    #endregion

    #region OpenInnerWindowCommand
    public ICommand OpenInnerWindowCommand { get; }
    private bool CanOpenInnerWindowCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(addFilmViewModel);
    private void OnOpenInnerWindowCommandExecuted(object p)
    {
      if (addFilmViewModel == null)
        addFilmViewModel = new AddFilmViewModel();
      addFilmViewModel.OldTitle = "";
      addFilmViewModel.Title = "";
      addFilmViewModel.Description = "";
      addFilmViewModel.Icon = "";
      addFilmViewModel.Trailer = "";
      addFilmViewModel.Year = new DateTime();
      addFilmViewModel.DisplayRootRegistry = displayRootRegistry;
      displayRootRegistry.ShowPresentation(addFilmViewModel);
    }
    #endregion

    #region OpenGreetingWindowCommand
    public ICommand OpenGreetingWindowCommand { get; }
    private bool CanOpenGreetingWindowCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(greetingsWindowViewModel);
    public void OnOpenGreetingWindowCommandExxecuted(object p)
    {
      bool Agreement;
      FileStream createFile = null;
      StreamReader file = null;
      try
      {
        file = new StreamReader("Agreement.txt");
      }
      catch
      {
        createFile = File.Create("Agreement.txt");
        createFile.Close();
        file = new StreamReader("Agreement.txt");
      }
      Agreement = false;
      if (file != null)
      {
        string yesOrNo = file.ReadLine();
        if (yesOrNo != null)
          Agreement = bool.Parse(yesOrNo);
        file.Close();
      }
      if(greetingsWindowViewModel == null)
        greetingsWindowViewModel = new GreetingsWindowViewModel();
      if (Agreement)
        greetingsWindowViewModel.IsChecked = true;
      else
        greetingsWindowViewModel.IsChecked = false;
      if (Convert.ToBoolean(p))
      {
        greetingsWindowViewModel.DisplayRootRegistry = displayRootRegistry;
        displayRootRegistry.ShowPresentation(greetingsWindowViewModel);
      }
      else
      {
        if (!Agreement)
        {
          greetingsWindowViewModel.DisplayRootRegistry = displayRootRegistry;
          displayRootRegistry.ShowPresentation(greetingsWindowViewModel);
        }
      }
    }
    #endregion

    #region EditFilmCommand
    public ICommand EditFilmCommand { get; }
    private bool CanEditFilmCommandExecute(object p) => WhichFilm != null && !_concentrationService.IsEmpty() &&
      !displayRootRegistry.CheckForExistingWindows(addFilmViewModel);
    private void OnEditFilmCommandExecuted(object p)
    {
      if(addFilmViewModel == null)
        addFilmViewModel = new AddFilmViewModel();
      addFilmViewModel.OldTitle = WhichFilm.Title;
      addFilmViewModel.Title = WhichFilm.Title;
      addFilmViewModel.Description = WhichFilm.Description;
      addFilmViewModel.Icon = WhichFilm.Icon;
      addFilmViewModel.Trailer = WhichFilm.Trailer;
      addFilmViewModel.Year = WhichFilm.Year;
      addFilmViewModel.DisplayRootRegistry = displayRootRegistry;
      displayRootRegistry.ShowPresentation(addFilmViewModel);
    }
    #endregion

    #region CloseApplicationCommand
    public ICommand CloseApplicationCommand { get;}
    private void OnCloseApplicationCommandExecuted(object p)
    {
      System.Windows.Application.Current.Shutdown();
    }
    private bool CanCloseApplicationCommandExecute(object p) => !displayRootRegistry.CheckForExistingWindows(addFilmViewModel) &&
      !displayRootRegistry.CheckForExistingWindows(informationAboutFilmWindowViewModel) && 
      !displayRootRegistry.CheckForExistingWindows(greetingsWindowViewModel);
    #endregion

    #region RefreshDataBaseCommand
    public ICommand RefreshDataBaseCommand { get; }
    private bool CanRefreshDataBaseCommandExecute(object p) => !_concentrationService.IsEmpty();
    private void OnRefreshDataBaseCommandExecuted(object p)
    {
      Films.Clear();
      List<Film> someFilms = _concentrationService.GetData();
      for (int i = 0; i < someFilms.Count; i++)
      {
        Films.Add((Film)someFilms[i].Clone());
      }
    }
    #endregion

    #region DeleteDataFromDataBaseCommand
    public ICommand DeleteDataFromDataBaseCommand { get; }
    private bool CanDeleteDataFromDataBaseCommandExecute(object p) => WhichFilm != null;
    private void OnDeleteDataFromDataBaseCommandExecuted(object p)
    {
      _concentrationService.DeleteData(WhichFilm.Title);
      NotificationFileUpdated();
    }
    #endregion

    #endregion
    
    public MainWindowViewModel()
    {
      Films = new ObservableCollection<Film>();
      displayRootRegistry = (System.Windows.Application.Current as App).displayRootRegistry;
      if (!_concentrationService.IsEmpty())
      {
        List<Film> someFilms = _concentrationService.GetData();
        for(int i = 0; i < someFilms.Count; i++)
        {
          Films.Add((Film)someFilms[i].Clone());
        }
      }

      OnOpenGreetingWindowCommandExxecuted(false);
      #region Commands
      CloseApplicationCommand = new LambdaCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
      OpenInnerWindowCommand = new LambdaCommand(OnOpenInnerWindowCommandExecuted, CanOpenInnerWindowCommandExecute);
      RefreshDataBaseCommand = new LambdaCommand(OnRefreshDataBaseCommandExecuted, CanRefreshDataBaseCommandExecute);
      DeleteDataFromDataBaseCommand = new LambdaCommand(OnDeleteDataFromDataBaseCommandExecuted, CanDeleteDataFromDataBaseCommandExecute);
      EditFilmCommand = new LambdaCommand(OnEditFilmCommandExecuted, CanEditFilmCommandExecute);
      InformationAboutFilmCommand = new LambdaCommand(OnInformationAboutFilmCommandExecuted, CanInformationAboutFilmCommandExecute);
      OpenGreetingWindowCommand = new LambdaCommand(OnOpenGreetingWindowCommandExxecuted, CanOpenGreetingWindowCommandExecute);
      #endregion
    }
  }
}
