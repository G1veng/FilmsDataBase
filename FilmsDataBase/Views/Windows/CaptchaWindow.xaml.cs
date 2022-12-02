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
using FilmsDataBase.ViewModels;

namespace FilmsDataBase.Views.Windows
{
  /// <summary>
  /// Interaction logic for Captcha.xaml
  /// </summary>
  public partial class CaptchaWindow : Window
  {
    public CaptchaWindow()
    {
      InitializeComponent();
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (!((CapthcaViewModel)this.DataContext).IsRegistrated)
      {
        Environment.Exit(0);
      }
    }
  }
}
