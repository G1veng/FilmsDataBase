﻿using System.Windows;
using FilmsDataBase.ViewModels;
using FilmsDataBase.Views.Windows;
using FilmsDataBase.Infrastructure;
using FilmsDataBase.Services;
using Autofac;
using FilmsDataBase.Data;

namespace FilmsDataBase
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    public DisplayRootRegistry displayRootRegistry = new DisplayRootRegistry();
    private static IContainer _mainWindowService;

    public App()
    {
      displayRootRegistry.RegisterWindowType<AddFilmViewModel, AddFilmWindow>();
      displayRootRegistry.RegisterWindowType<InformationAboutFilmWindowViewModel, InformationAboutFilmWindow>();
      displayRootRegistry.RegisterWindowType<GreetingsWindowViewModel, GreetingsWindow>();
    }
    private static void RegisterTypes()
    {
      var mainWindowBuilder = new ContainerBuilder();
      mainWindowBuilder.RegisterType<FilmService>().As<IFilmService>();
      mainWindowBuilder.RegisterType<MainWindowViewModel>();
      _mainWindowService = mainWindowBuilder.Build();
    }

    private void Application_Startup(object sender, StartupEventArgs e)
    {
      RegisterTypes();
      _mainWindowService.Resolve<MainWindowViewModel>();
    }
  }
}
