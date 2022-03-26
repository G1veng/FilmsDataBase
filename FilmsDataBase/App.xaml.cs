using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using FilmsDataBase.ViewModels;
using FilmsDataBase.Views.Windows;
using FilmsDataBase;
using FilmsDataBase.Data;


namespace FilmsDataBase
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {

    public DisplayRootRegistry displayRootRegistry = new DisplayRootRegistry();

    public App()
    {
      displayRootRegistry.RegisterWindowType<AddFilmViewModel, AddFilmWindow>();
      BaseOfFilms baseOfFilms = new BaseOfFilms();
    }
  }
}
