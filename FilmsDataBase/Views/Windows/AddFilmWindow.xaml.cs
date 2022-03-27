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
  /// Interaction logic for AddFilmWindow.xaml
  /// </summary>
  public partial class AddFilmWindow : Window
  {
    private bool isPlaying =  false;
    public AddFilmWindow()
    {
      InitializeComponent();
      Video.Play();
      Video.Stop();
    }

    private void MediaElement_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (isPlaying)
        Video.Pause();
      if (!isPlaying)
        Video.Play();
      isPlaying = isPlaying == false ? true : false;
    }
  }
}
