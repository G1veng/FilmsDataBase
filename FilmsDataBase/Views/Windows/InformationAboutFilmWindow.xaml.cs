using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FilmsDataBase.Views.Windows
{
  /// <summary>
  /// Interaction logic for InformationAboutFilm  .xaml
  /// </summary>
  public partial class InformationAboutFilmWindow : Window
  {
    private bool isPlaying = false;
    public InformationAboutFilmWindow()
    {
      InitializeComponent();
      Video.Play();
      Video.Pause();
    }
    private void Video_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (isPlaying)
        Video.Stop();
      else
        Video.Play();
      isPlaying = isPlaying == false ? isPlaying = true : isPlaying = false;
    }
  }
}
